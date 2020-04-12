using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsPairModel
    {
        public string collection { get; set; }
        public string query { get; set; }
        public string tickerId { get; set; }
        public string blockfactsPair { get; set; }
        public string assetName { get; set; }
        public bool active { get; set; }
        public string type { get; set; }
        public string denominatorName { get; set; }
        public string blockfactsDenominator { get; set; }
    }
}
