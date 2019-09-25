using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsHistoricalExchangeTradesModel
    {
        public int page { get; set; }
        public int totalPages { get; set; }
        public List<BlockfactsTradeModel> results { get; set; }
    }
}
