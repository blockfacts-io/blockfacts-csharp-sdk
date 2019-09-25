using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsExchangeDataModel
    {
        public string exchange { get; set; }
        public List<BlockfactsPairModel> pairs { get; set; }
    }
}
