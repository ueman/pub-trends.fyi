using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PubTrends
{
    public class PubClient {
        private const string _version = "0.0.1";
        private const string _site = "https://pub-trends.fyi";

        private HttpClient _client;

        public PubClient() {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", $"pub-trends.fyi/{_version} (+{_site})");
        }


        public async Task<Metrics> LoadPackageMetrics(string packageName) {
            var url = $"https://pub.dev/api/packages/{packageName}/metrics";
            var response = await _client.GetAsync(url);
            var stringContent = await response.Content.ReadAsStringAsync();
            var pubMetricsRoot = JsonConvert.DeserializeObject<PubRoot>(stringContent); 
            var metrics = pubMetricsRoot.ToMetric();
            metrics.OriginalJson = stringContent;
            return metrics;
        }
    }
}