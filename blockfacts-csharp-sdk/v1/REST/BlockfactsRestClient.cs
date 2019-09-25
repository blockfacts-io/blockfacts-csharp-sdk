using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.REST
{
    public class BlockfactsRestClient
    {
        public AssetEndpoints Assets;
        public BlockfactsEndpoints Blockfacts;
        public ExchangeEndpoints Exchanges;

        public BlockfactsRestClient(string key = "api-key-not-specified", string secret = "api-secret-not-specified")
        {
            this.Assets = new AssetEndpoints(key, secret);
            this.Blockfacts = new BlockfactsEndpoints(key, secret);
            this.Exchanges = new ExchangeEndpoints(key, secret);
        }

        /// <summary>
        /// Sets an API Key.
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(string key)
        {
            this.Assets.apiKey = key;
            this.Blockfacts.apiKey = key;
            this.Exchanges.apiKey = key;
        }

        /// <summary>
        /// Sets an API Secret.
        /// </summary>
        /// <param name="key"></param>
        public void SetSecret(string secret)
        {
            this.Assets.apiSecret = secret;
            this.Blockfacts.apiSecret = secret;
            this.Exchanges.apiSecret = secret;
        }
    }
}
