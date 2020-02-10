using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsSubscribeMessage
    {
        public string type { get; set; }
        public bool snapshot { get; set; }
        public string id { get; set; }
        public string X_API_KEY { get; set; }
        public string X_API_SECRET { get; set; }
        public List<BlockfactsChannelObject> channels { get; set; }
    }

    public class BlockfactsChannelObject
    {
        public string exchangeName { get; set; }
        public List<string> pairs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName">Name of the exchange</param>
        /// <param name="pairs">List of pairs for subscription</param>
        public BlockfactsChannelObject(string exchangeName, List<string> pairs)
        {
            this.exchangeName = exchangeName;
            this.pairs = pairs;
        }

        public BlockfactsChannelObject() { }
    }
}
