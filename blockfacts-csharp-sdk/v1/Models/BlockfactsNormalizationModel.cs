using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsNormalizationModel
    {
        public string exchange { get; set; }
        public string pair { get; set; }
        public double price { get; set; }
        public List<BlockfactsTradeModel> included { get; set; }
        public List<BlockfactsTradeModel> excluded { get; set; }
        public long timestamp { get; set; }
        public long normalizationTimestamp { get; set; }
        public string algorithm { get; set; }
    }
}
