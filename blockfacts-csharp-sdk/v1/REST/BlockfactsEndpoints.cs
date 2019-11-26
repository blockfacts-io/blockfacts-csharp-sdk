using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using blockfacts_csharp_sdk.v1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace blockfacts_csharp_sdk.v1.REST
{
    public class BlockfactsEndpoints : Endpoints, IBlockfactsEndpoints
    {
        public RestClient restClient;
        public RestRequest restRequest;

        public BlockfactsEndpoints(string key, string secret) : base(key, secret)
        {
            this.restClient = new RestClient();
            this.restRequest = new RestRequest(Method.GET);
            this.restRequest.AddHeader("Content-Type", this.headers["Content-Type"].Value<string>());
            this.restRequest.AddHeader("X-API-KEY", this.headers["X-API-KEY"].Value<string>());
            this.restRequest.AddHeader("X-API-SECRET", this.headers["X-API-SECRET"].Value<string>());
        }

        /// <summary>
        /// Lists all exchanges that go into the normalization for specific asset-denominator pair.
        /// Reference: https://docs.blockfacts.io/?csharp#exchanges-in-normalization
        /// </summary>
        /// <param name="pairs">Asset-denominator pairs (e.g. BTC-USD, BTC-EUR)</param>
        /// <returns>JObject</returns>
        public async Task<JObject> GetExchangesInNormalization(string pairs)
        {
            pairs = pairs.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/normalization/whitelist/" + pairs);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);
            return data;
        }

        /// <summary>
        /// Gets current normalization data for specific asset-denominator pair.
        /// Reference: https://docs.blockfacts.io/?csharp#current-data
        /// </summary>
        /// <param name="assets">Asset tickers (e.g. BTC, ETH)</param>
        /// <param name="denominators">Denominator tickers (e.g. USD, EUR)</param>
        /// <returns>JObject</returns>
        public async Task<JObject> GetCurrentData(string assets, string denominators)
        {
            assets = assets.Trim().Replace(" ", "");
            denominators = denominators.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/price?asset=" + assets + "&denominator=" + denominators);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);
            return data;
        }

        /// <summary>
        /// Gets historical normalization data by asset-denominator, date, time and interval.
        /// Reference: https://docs.blockfacts.io/?csharp#historical-data
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="date">Specific date (e.g. 2.8.2019)</param>
        /// <param name="time">Specific time (e.g. 14:01:00)</param>
        /// <param name="interval">Historical interval to cover (e.g. 20 = 14:01:00 - 14:21:00) (Min 0, Max 240)</param>
        /// <param name="page">Optional, our API is always showing 100 results per page in order to improve the performance. You can provide the page parameter in order to query a specific page</param>
        /// <returns>BlockfactsHistoricalNormalizationResultsModel</returns>
        public async Task<BlockfactsHistoricalNormalizationResultsModel> GetHistoricalData(string asset, string denominator, string date, string time, int interval, int page = 1)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/price/historical?asset=" + asset + "&denominator=" + denominator + "&date=" + date + "&time=" + time + "&interval=" + interval.ToString() + "&page=" + page.ToString());
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<BlockfactsHistoricalNormalizationResultsModel>(response.Content);
            return data;
        }

        /// <summary>
        /// Get historical normalized price by specific point in time.
        /// Reference: https://docs.blockfacts.io/?csharp#specific-historical-data
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="date">Specific date (e.g. 2.9.2019)</param>
        /// <param name="time">Specific time (e.g. 14:00:00)</param>
        /// <returns>BlockfactsNormalizationModel</returns>
        public async Task<BlockfactsNormalizationModel> GetSpecificHistoricalData(string asset, string denominator, string date, string time)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/price/specific?asset=" + asset + "&denominator=" + denominator + "&date=" + date + "&time=" + time);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<BlockfactsNormalizationModel>(response.Content);
            return data;
        }

        /// <summary>
        /// Get all running normalization pairs. Resulting in which asset-denominator pairs are currently being normalized inside our internal system.
        /// Reference: https://docs.blockfacts.io/?csharp#normalization-pairs
        /// </summary>
        /// <returns>List<BlockfactsRunningNormalizationPairsTradesModel></returns>
        public async Task<List<BlockfactsRunningNormalizationPairsTradesModel>> GetNormalizationPairs()
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/normalization/trades");
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<List<BlockfactsRunningNormalizationPairsTradesModel>>(response.Content);
            return data;
        }

        /// <summary>
        /// Get normalized end of day data for specific asset-denominator.
        /// Reference: https://docs.blockfacts.io/?csharp#end-of-day-data
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="length">Length (representing how many days back from the current day, Min = 0, Max = 20)</param>
        /// <returns>List<BlockfactsEndOfDayModel></returns>
        public async Task<List<BlockfactsOHLCModel>> GetEndOfDayData(string asset, string denominator, int length)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/price/endOfDay?asset=" + asset + "&denominator=" + denominator + "&length=" + length.ToString());
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<List<BlockfactsOHLCModel>>(response.Content);
            return data;
        }
    }
}
