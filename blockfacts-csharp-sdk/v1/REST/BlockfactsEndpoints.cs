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
        /// Gets last 600 BLOCKFACTS normalized prices for provided asset-denominator pairs.
        /// Reference: https://docs.blockfacts.io/?csharp#data-snapshot
        /// </summary>
        /// <param name="assets">Asset tickers (e.g. BTC, ETH)</param>
        /// <param name="denominators">Denominator tickers (e.g. USD, EUR)</param>
        /// <returns></returns>
        public async Task<JObject> GetSnapshotData(string assets, string denominators)
        {
            assets = assets.Trim().Replace(" ", "");
            denominators = denominators.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/price/snapshot?asset=" + assets + "&denominator=" + denominators);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);
            return data;
        }

        /// <summary>
        /// Gets the snapshot of Blockfacts OHLCV data for provided asset-denominator pairs and intervals.
        /// Reference: https://docs.blockfacts.io/?csharp#data-snapshot-ohlcv-blockfacts
        /// </summary>
        /// <param name="assets">Asset tickers (e.g. BTC, ETH)</param>
        /// <param name="denominators">Denominator tickers (e.g. USD, EUR)</param>
        /// <param name="intervals">Intervals (e.g. 1m, 3m, 1h)</param>
        /// <returns></returns>
        public async Task<JObject> GetOHLCVSnapshotData(string assets, string denominators, string intervals)
        {
            assets = assets.Trim().Replace(" ", "");
            denominators = denominators.Trim().Replace(" ", "");
            intervals = intervals.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/price/ohlcv-snapshot?asset=" + assets + "&denominator=" + denominators + "&interval=" + intervals);
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
        /// Gets historical OHLCV data by asset-denominator, date, time and interval.
        /// Reference: https://docs.blockfacts.io/?csharp#ohlcv-historical-data
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="interval">OHLCV Interval (30s, 1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 12h, 1d, 1w, 1mo)</param>
        /// <param name="dateStart">Specific date to start from (e.g. 5.8.2020)</param>
        /// <param name="timeStart">Specific time to start from (in UTC) (e.g. 14:00:00)</param>
        /// <param name="dateEnd">Specific end date (e.g. 5.8.2020)</param>
        /// <param name="timeEnd">Specific end time (in UTC) (e.g. 14:00:00)</param>
        /// <param name="page">Optional, our API is always showing 100 results per page in order to improve the performance. You can provide the page parameter in order to query a specific page</param>
        /// <returns>Jobject</returns>
        public async Task<JObject> GetHistoricalOHLCVData(string asset, string denominator, string interval, string dateStart, string timeStart, string dateEnd, string timeEnd, int page = 1)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/ohlcv?asset=" + asset + "&denominator=" + denominator + "&interval=" + interval + "&dateStart=" + dateStart + "&timeStart=" + timeStart + "&dateEnd=" + dateEnd + "&timeEnd=" + timeEnd + "&page=" + page.ToString());
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<JObject>(response.Content);
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
        /// Gets the moving percentage, and difference in price over a certain time period.
        /// Reference: https://docs.blockfacts.io/?csharp#period-movers
        /// </summary>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="date">Specific date (e.g. 11.8.2020)</param>
        /// <param name="interval">Interval (oneDay, sevenDay, thirtyDay, ninetyDay, oneYear, twoYear, threeYear, fiveYear)</param>
        /// <param name="sort">1 - Losers first, -1 - Winners first</param>
        /// <returns>JArray</returns>
        public async Task<JArray> GetPeriodMovers(string denominator, string date, string interval, int sort)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/blockfacts/period-movers?denominator=" + denominator + "&date=" + date + "&interval=" + interval + "&sort=" + sort );
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<JArray>(response.Content);
            return data;
        }
    }
}
