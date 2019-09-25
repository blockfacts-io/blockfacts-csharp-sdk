using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsNormalizationIncludedExchangesModel
    {
        public string asset { get; set; }
        public string blockfactsPair { get; set; }
        public string blockfactsTicker { get; set; }
        public string blockfactsDenominator { get; set; }
        public List<string> exchanges { get; set; }
    }
}
