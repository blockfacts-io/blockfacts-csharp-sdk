using blockfacts_csharp_sdk.v1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blockfacts_csharp_sdk.v1.REST
{
    public class ExchangeEndpoints : Endpoints, IExchangeEndpoints
    {
        public RestClient restClient;
        public RestRequest restRequest;

        public ExchangeEndpoints(string key, string secret) : base(key, secret)
        {
            this.restClient = new RestClient();
            this.restRequest = new RestRequest(Method.GET);
            this.restRequest.AddHeader("Content-Type", this.headers["Content-Type"].Value<string>());
            this.restRequest.AddHeader("X-API-KEY", this.headers["X-API-KEY"].Value<string>());
            this.restRequest.AddHeader("X-API-SECRET", this.headers["X-API-SECRET"].Value<string>());
        }
        
        /// <summary>
        /// Lists all exchanges that we support.
        /// Reference: https://docs.blockfacts.io/?csharp#all-exchanges
        /// </summary>
        /// <returns>List<BlockfactsExchangeDataModel></returns>
        public async Task<List<BlockfactsExchangeDataModel>> ListAllExchanges()
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges");
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<List<BlockfactsExchangeDataModel>>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets information about a specific exchange by its name. Returns information such as which assets are supported, asset ticker info, etc.
        /// Reference: https://docs.blockfacts.io/?csharp#specific-exchange-data
        /// </summary>
        /// <param name="exchange">Name of the exchange (e.g. KRAKEN)</param>
        /// <returns>BlockfactsExchangeDataModel</returns>
        public async Task<BlockfactsExchangeDataModel> GetSpecificExchangeData(string exchange)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/" + exchange);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<BlockfactsExchangeDataModel>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets the Blockfacts pair representation of the provided exchange pair.
        /// Reference: https://docs.blockfacts.io/?csharp#pair-info
        /// </summary>
        /// <param name="exchange">Name of the exchange (e.g. KRAKEN)</param>
        /// <param name="pair">Pair name query on the provided exchange (e.g. BTCUSD or XBTUSD)</param>
        /// <returns>JObject</returns>
        public async Task<JObject> GetPairInfo(string exchange, string pair)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/pair-info?exchange=" + exchange + "&pair=" + pair);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<JObject>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets current trade data for specific asset-denominator pair, from specific exchange(s).
        /// Reference: https://docs.blockfacts.io/?csharp#current-trade-data
        /// </summary>
        /// <param name="assets">Asset tickers (e.g. BTC, ETH)</param>
        /// <param name="denominators">Denominator tickers (e.g. USD, EUR)</param>
        /// <param name="exchanges">Exchange names (e.g. KRAKEN, COINBASE)</param>
        /// <returns>JObject</returns>
        public async Task<JObject> GetCurrentTradeData(string assets, string denominators, string exchanges)
        {
            assets = assets.Trim().Replace(" ", "");
            denominators = denominators.Trim().Replace(" ", "");
            exchanges = exchanges.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades?asset=" + assets + "&denominator=" + denominators + "&exchange=" + exchanges);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);
            return data;
        }

        /// <summary>
        /// Gets 600 latest trades that happened on the requested exchanges and pairs.
        /// Reference: https://docs.blockfacts.io/?csharp#snapshot-trade-data
        /// </summary>
        /// <param name="assets">Asset tickers (e.g. BTC, ETH)</param>
        /// <param name="denominators">Denominator tickers (e.g. USD, EUR)</param>
        /// <param name="exchanges">Exchange names (e.g. KRAKEN, COINBASE)</param>
        /// <returns></returns>
        public async Task<JObject> GetSnapshotTradeData(string assets, string denominators, string exchanges)
        {
            assets = assets.Trim().Replace(" ", "");
            denominators = denominators.Trim().Replace(" ", "");
            exchanges = exchanges.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/snapshot?asset=" + assets + "&denominator=" + denominators + "&exchange=" + exchanges);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);
            return data;
        }

        /// <summary>
        /// Gets the snapshot of provided exchange(s) OHLCV data for provided asset-denominator pairs and intervals.
        /// Reference: https://docs.blockfacts.io/?csharp#data-snapshot-ohlcv-exchange
        /// </summary>
        /// <param name="assets">Asset tickers (e.g. BTC, ETH)</param>
        /// <param name="denominators">Denominator tickers (e.g. USD, EUR)</param>
        /// <param name="exchanges">Exchange names (e.g. KRAKEN, COINBASE)</param>
        /// <param name="intervals">Intervals (e.g. 1m, 3m, 1h)</param>
        /// <returns></returns>
        public async Task<JObject> GetOHLCVSnapshotData(string assets, string denominators, string exchanges, string intervals)
        {
            assets = assets.Trim().Replace(" ", "");
            denominators = denominators.Trim().Replace(" ", "");
            exchanges = exchanges.Trim().Replace(" ", "");
            intervals = intervals.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/ohlcv-snapshot?asset=" + assets + "&denominator=" + denominators + "&exchange=" + exchanges + "&interval=" + intervals);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JObject.Parse(response.Content);
            return data;
        }

        /// <summary>
        /// Gets exchange historical price by asset-denominator, exchange, date, time and interval.
        /// Reference: https://docs.blockfacts.io/?csharp#historical-trade-data
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="exchanges">Exchange names (e.g. KRAKEN, COINBASE)</param>
        /// <param name="date">Specific date (e.g. 2.9.2019)</param>
        /// <param name="time">Specific time (e.g. 14:00:00)</param>
        /// <param name="interval">Historical interval to cover (e.g. 20 = 14:00:00 - 14:20:00) (Min 0, Max 240)</param>
        /// <param name="page">Optional, our API is always showing 100 results per page in order to improve the performance. You can provide the page parameter in order to query a specific page</param>
        /// <returns>BlockfactsHistoricalExchangeTradesModel</returns>
        public async Task<BlockfactsHistoricalExchangeTradesModel> GetHistoricalTradeData(string asset, string denominator, string exchanges, string date, string time, int interval, int page = 1)
        {
            exchanges = exchanges.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/historical?asset=" + asset + "&denominator=" + denominator + "&exchange=" + exchanges + "&date=" + date + "&time=" + time + "&interval=" + interval.ToString() + "&page=" + page.ToString());
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<BlockfactsHistoricalExchangeTradesModel>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets historical OHLCV data by asset-denominator, exchange, date, time and interval.
        /// Reference: https://docs.blockfacts.io/?csharp#ohlcv-historical-data-2
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="exchanges">Exchange names (e.g. KRAKEN, COINBASE)</param>
        /// <param name="interval">OHLCV Interval (30s, 1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 12h, 1d, 1w, 1mo)</param>
        /// <param name="dateStart">Specific date to start from (e.g. 5.8.2020)</param>
        /// <param name="timeStart">Specific time to start from (in UTC) (e.g. 14:00:00)</param>
        /// <param name="dateEnd">Specific end date (e.g. 5.8.2020)</param>
        /// <param name="timeEnd">Specific end time (in UTC) (e.g. 14:00:00)</param>
        /// <param name="page">Optional, our API is always showing 100 results per page in order to improve the performance. You can provide the page parameter in order to query a specific page</param>
        /// <returns>JObject</returns>
        public async Task<JObject> GetHistoricalOHLCVData(string asset, string denominator, string exchanges, string interval, string dateStart, string timeStart, string dateEnd, string timeEnd, int page = 1)
        {
            exchanges = exchanges.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/ohlcv?asset=" + asset + "&denominator=" + denominator + "&exchange=" + exchanges + "&interval=" + interval + "&dateStart=" + dateStart + "&timeStart=" + timeStart + "&dateEnd=" + dateEnd + "&timeEnd=" + timeEnd + "&page=" + page.ToString());
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<JObject>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets historical exchange trades in specific second.
        /// Reference: https://docs.blockfacts.io/?csharp#specific-trade-data
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="exchanges">Exchange name (e.g. KRAKEN, COINBASE)</param>
        /// <param name="date">Specific date (e.g. 2.9.2019)</param>
        /// <param name="time">Specific time (e.g. 14:00:00)</param>
        /// <returns>List<BlockfactsTradeModel></returns>
        public async Task<List<BlockfactsTradeModel>> GetSpecificTradeData(string asset, string denominator, string exchanges, string date, string time)
        {
            exchanges = exchanges.Trim().Replace(" ", "");

            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/specific?asset=" + asset + "&denominator=" + denominator + "&exchange=" + exchanges + "&date=" + date + "&time=" + time);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<List<BlockfactsTradeModel>>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets the total traded volume on all exchanges by asset-denominator and interval.
        /// Reference: https://docs.blockfacts.io/?csharp#total-trade-volume
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="interval">Interval (1d, 30d, 60d, 90d)</param>
        /// <returns>JObject</returns>
        public async Task<JObject> GetTotalTradeVolume(string asset, string denominator, string interval)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/total-volume?asset=" + asset + "&denominator=" + denominator + "&interval=" + interval);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<JObject>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets the moving percentage, and difference in price over a certain time period.
        /// Reference: https://docs.blockfacts.io/?csharp#period-movers-2
        /// </summary>
        /// <param name="exchange">Exchange name (e.g. KRAKEN)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="date">Specific date (e.g. 11.8.2020)</param>
        /// <param name="interval">Interval (oneDay, sevenDay, thirtyDay, ninetyDay, oneYear, twoYear, threeYear, fiveYear)</param>
        /// <param name="sort">1 - Losers first, -1 - Winners first</param>
        /// <returns>JArray</returns>
        public async Task<JArray> GetPeriodMovers(string exchange, string denominator, string date, string interval, int sort)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/period-movers?exchange=" + exchange + "&denominator=" + denominator + "&date=" + date + "&interval=" + interval + "&sort=" + sort);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<JArray>(response.Content);
            return data;
        }
    }
}
