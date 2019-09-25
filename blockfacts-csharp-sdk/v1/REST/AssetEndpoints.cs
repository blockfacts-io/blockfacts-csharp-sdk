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
    public class AssetEndpoints : Endpoints, IAssetEndpoints
    {
        public RestClient restClient;
        public RestRequest restRequest;

        public AssetEndpoints(string key, string secret) : base(key, secret)
        {
            this.restClient = new RestClient();
            this.restRequest = new RestRequest(Method.GET);
            this.restRequest.AddHeader("Content-Type", this.headers["Content-Type"].Value<string>());
            this.restRequest.AddHeader("X-API-KEY", this.headers["X-API-KEY"].Value<string>());
            this.restRequest.AddHeader("X-API-SECRET", this.headers["X-API-SECRET"].Value<string>());
        }

        /// <summary>
        /// Lists all assets that we support.
        /// Reference: https://docs.blockfacts.io/?csharp#list-all-assets
        /// </summary>
        /// <returns>List<BlockfactsAssetModel></returns>
        public async Task<List<BlockfactsAssetModel>> ListAllAssets()
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/assets");
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<List<BlockfactsAssetModel>>(response.Content);
            return data;
        }

        /// <summary>
        /// Gets specific asset by ticker ID.
        /// Reference: https://docs.blockfacts.io/?csharp#specific-asset
        /// </summary>
        /// <param name="tickerId">BlockFacts asset ticker (e.g. BTC)</param>
        /// <returns>BlockfactsAssetModel</returns>
        public async Task<BlockfactsAssetModel> GetSpecificAsset(string tickerId)
        {
            restClient.BaseUrl = new Uri(this.blockfactsApiUrl + "/api/v1/assets/"+tickerId);
            var response = await restClient.ExecuteTaskAsync(restRequest);
            var data = JsonConvert.DeserializeObject<BlockfactsAssetModel>(response.Content);
            return data;
        }
    }
}
