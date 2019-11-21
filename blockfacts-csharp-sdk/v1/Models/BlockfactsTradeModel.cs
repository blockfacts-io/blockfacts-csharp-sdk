using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsTradeModel
    {
        public string exchange { get; set; }
        public string pair { get; set; }
        public double price { get; set; }
        public double tradeSize { get; set; }
        public double denominatorSize { get; set; }
        public string makerTaker { get; set; }
        public string tradeId { get; set; }
        public string exchangeTime { get; set; }
        public long blockfactsTime { get; set; }
        public long epochExchangeTime { get; set; }
    }
}
