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
        /// Gets exchange end of day data for specific asset-denominator and exchange.
        /// Reference: https://docs.blockfacts.io/?csharp#end-of-day-data-2
        /// </summary>
        /// <param name="asset">Asset ticker (e.g. BTC)</param>
        /// <param name="denominator">Denominator ticker (e.g. USD)</param>
        /// <param name="exchange">Exchange name (e.g. KRAKEN)</param>
        /// <param name="length">Length (representing how many days back from the current day, Min = 0, Max = 20)</param>
        /// <returns>List<BlockfactsEndOfDayModel></returns>
        public async Task<List<BlockfactsOHLCModel>> GetEndOfDayData(string asset, string denominator, string exchange, int length)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/exchanges/trades/endOfDay?asset=" + asset + "&denominator=" + denominator + "&exchange=" + exchange + "&length=" + length.ToString());
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<List<BlockfactsOHLCModel>>(response.Content);
            return data;
        }
    }
}
