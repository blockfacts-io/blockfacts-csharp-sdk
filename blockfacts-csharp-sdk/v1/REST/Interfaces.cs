using blockfacts_csharp_sdk.v1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blockfacts_csharp_sdk.v1.REST
{
    public interface IAssetEndpoints
    {
        Task<List<BlockfactsAssetModel>> ListAllAssets();
        Task<BlockfactsAssetModel> GetSpecificAsset(string tickerId);
    }

    public interface IBlockfactsEndpoints
    {
        Task<JObject> GetExchangesInNormalization(string pairs);
        Task<JObject> GetCurrentData(string assets, string denominators);
        Task<JObject> GetSnapshotData(string assets, string denominators);
        Task<BlockfactsHistoricalNormalizationResultsModel> GetHistoricalData(string asset, string denominator, string date, string time, int interval, int page);
        Task<BlockfactsNormalizationModel> GetSpecificHistoricalData(string asset, string denominator, string date, string time);
        Task<List<BlockfactsRunningNormalizationPairsTradesModel>> GetNormalizationPairs();
        Task<JObject> GetHistoricalOHLCVData(string asset, string denominator, string interval, string dateStart, string timeStart, string dateEnd, string timeEnd, int page);
        Task<JArray> GetPeriodMovers(string denominator, string date, string interval, int sort);
    }

    public interface IExchangeEndpoints
    {
        Task<List<BlockfactsExchangeDataModel>> ListAllExchanges();
        Task<BlockfactsExchangeDataModel> GetSpecificExchangeData(string exchange);
        Task<JObject> GetCurrentTradeData(string assets, string denominators, string exchanges);
        Task<JObject> GetSnapshotTradeData(string assets, string denominators, string exchanges);
        Task<BlockfactsHistoricalExchangeTradesModel> GetHistoricalTradeData(string asset, string denominator, string exchanges, string date, string time, int interval, int page);
        Task<List<BlockfactsTradeModel>> GetSpecificTradeData(string asset, string denominator, string exchanges, string date, string time);
        Task<JObject> GetPairInfo(string exchange, string pair);
        Task<JObject> GetHistoricalOHLCVData(string asset, string denominator, string exchanges, string interval, string dateStart, string timeStart, string dateEnd, string timeEnd, int page);
        Task<JObject> GetTotalTradeVolume(string asset, string denominator, string interval);
        Task<JArray> GetPeriodMovers(string exchange, string denominator, string date, string interval, int sort);
    }
}
