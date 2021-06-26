using System;
using SQLite;

namespace PubTrends
{
    public class Metrics {

        [PrimaryKey]
        public Guid Id {get; set;} = Guid.NewGuid();

        [Indexed]
        public string PackageName {get; set;}

        [Indexed]
        public string Version {get; set;}

        [Indexed]
        public int Points {get; set;}

        [Indexed]
        public int Likes {get; set;}
        
        [Indexed]
        public double Popularity {get; set;}

        [Indexed]
        public DateTime ReadAt {get; set;} = DateTime.Now;

        public DateTime LastUpdatedPub {get; set;}

        public string OriginalJson {get; set;}
    }
}
