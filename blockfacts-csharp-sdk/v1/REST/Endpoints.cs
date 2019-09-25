using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.REST
{
    public class Endpoints
    {
        public string apiKey;
        public string apiSecret;
        public JObject headers;
        public string blockfactsApiUrl;

        public Endpoints(string key, string secret)
        {
            this.blockfactsApiUrl = "https://api.blockfacts.io";
            this.apiKey = key;
            this.apiSecret = secret;
            this.headers = new JObject();
            this.headers["Content-Type"] = "application/json";
            this.headers["X-API-KEY"] = key;
            this.headers["X-API-SECRET"] = secret;
        }
    }
}
