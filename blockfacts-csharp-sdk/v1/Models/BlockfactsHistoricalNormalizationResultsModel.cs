using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsHistoricalNormalizationResultsModel
    {
        public int page { get; set; }
        public int totalPages { get; set; }
        public List<BlockfactsNormalizationModel> results { get; set; }
    }
}
