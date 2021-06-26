using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace PubTrends
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var packagesTxt = args[0]; // "/Users/jonas/Documents/projects/pub-trends.fyi/packages.txt"
            var pubDb = args[1]; // Path.Combine("/Users/jonas/Documents/projects/pub-trends.fyi/docs", "pub.db")
            Console.WriteLine($"Starting");
            var pubClient = new PubClient();
            var db = CreateDatabase(pubDb);
            var packages = File.ReadLines(packagesTxt);
            Console.WriteLine($"Starting to load package information");
            foreach (var package in packages)
            {
                if(string.IsNullOrWhiteSpace(package)){
                    continue;
                }
                try
                {
                    var metric = await pubClient.LoadPackageMetrics(package);
                    db.Insert(metric);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Could not load package {package}");
                }
            }
            Console.WriteLine($"Finished loading of package information");
            Console.WriteLine($"Optimizing database");
            db.Query<Object>("vacuum");
            db.Close();
            Console.WriteLine($"Finished");
        }

        static SQLiteConnection CreateDatabase(string path)
        {
            var db = new SQLiteConnection(path, storeDateTimeAsTicks: false);

            db.Query<Object>("pragma journal_mode = delete"); // to be able to actually set page size
            db.Query<Object>("pragma page_size = 1024"); // trade off of number of requests that need to be made vs overhead. 
            // db.Query<Object>("insert into ftstable(ftstable) values ('optimize')");// for every FTS table you have (if you have any)
            db.Query<Object>("vacuum");

            db.CreateTable<Metrics>();
            return db;
        }
    }
}
