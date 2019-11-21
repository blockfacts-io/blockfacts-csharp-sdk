using System;
using System.Collections.Generic;
using System.Text;

namespace blockfacts_csharp_sdk.v1.Models
{
    public class BlockfactsEndOfDayModel
    {
        public string exchange { get; set; }
        public string pair { get; set; }
        public double volume { get; set; }
        public double baseVolume { get; set; }
        public double low { get; set; }
        public double high { get; set; }
        public double open { get; set; }
        public double close { get; set; }
        public long tradesCount { get; set; }
        public long timestamp { get; set; }
        public DateTime date { get; set; }
    }
}
