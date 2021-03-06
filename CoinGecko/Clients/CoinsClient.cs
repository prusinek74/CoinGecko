using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinGecko.ApiEndPoints;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Interfaces;
using CoinGecko.Parameters;
using CoinGecko.Services;

namespace CoinGecko.Clients
{
    public class CoinsClient:BaseApiClient,ICoinsClient
    {
        public CoinsClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<IReadOnlyList<CoinFullData>> GetAllCoinsData()
        {
            return await GetAllCoinsData(OrderField.GeckoDesc, null, null, "", null);
        }

        public async Task<IReadOnlyList<CoinFullData>> GetAllCoinsData(string order,int? perPage,int? page,string localization,bool? sparkline)
        {
            return await GetAsync<IReadOnlyList<CoinFullData>>(QueryStringService.AppendQueryString(
                CoinsApiEndPoints.Coins, new Dictionary<string, object>
                {
                    {"order", order},
                    {"per_page", perPage},
                    {"page",page},
                    {"localization",localization},
                    {"sparkline",sparkline}
                }));
        }

        public async Task<IReadOnlyList<CoinList>> GetCoinList()
        {
            return await GetAsync<IReadOnlyList<CoinList>>(QueryStringService.AppendQueryString(CoinsApiEndPoints.CoinList));
        }

        public async Task<CoinMarkets> GetCoinMarkets(string vsCurrency)
        {
            return await GetCoinMarkets(vsCurrency, new string[] { }, null, null, null, false);
        }

        public async Task<CoinMarkets> GetCoinMarkets(string vsCurrency,string[] ids,string order,int? perPage,int? page,bool sparkline)
        {
            return await GetAsync<CoinMarkets>(QueryStringService.AppendQueryString(CoinsApiEndPoints.CoinMarkets,new Dictionary<string, object>
            {
                {"vs_currency",vsCurrency},
                {"ids",string.Join(",",ids)},
                {"per_page",perPage},
                {"page",page},
                {"sparkline",sparkline}
            }));
        }

        public async Task<CoinFullDataById> GetAllCoinDataWithId(string id)
        {
            return await GetAllCoinDataWithId(id, "true", true, true, true, true, false);
        }

        public async Task<CoinFullDataById> GetAllCoinDataWithId(string id, string localization, bool tickers,
            bool marketData, bool communityData, bool developerData, bool sparkline)
        {
            return await GetAsync<CoinFullDataById>(QueryStringService.AppendQueryString(
                CoinsApiEndPoints.AllDataByCoinId(id), new Dictionary<string, object>
                {
                    {"localization", localization},
                    {"tickers", tickers},
                    {"market_data", marketData},
                    {"community_data", communityData},
                    {"developer_data", developerData},
                    {"sparkline", sparkline}
                }));
        }

        public async Task<TickerById> GetTickerByCoinId(string id)
        {
            return await GetTickerByCoinId(id, null);
        }

        public async Task<TickerById> GetTickerByCoinId(string id, int? page)
        {
            return await GetAsync<TickerById>(QueryStringService.AppendQueryString(
                CoinsApiEndPoints.TickerByCoinId(id), new Dictionary<string, object>
                {
                    {"page", page}
                }));
        }

        public async Task<CoinFullData> GetHistoryByCoinId(string id, string date, string localization)
        {
            return await GetAsync<CoinFullData>(QueryStringService.AppendQueryString(
                CoinsApiEndPoints.HistoryByCoinId(id), new Dictionary<string, object>
                {
                    {"date",date},
                    {"localization",localization}
                }));
        }

        public async Task<MarketChartById> GetMarketChartsByCoinId(string id, string[] vsCurrency, string days)
        {
            return await GetAsync<MarketChartById>(QueryStringService.AppendQueryString(
                CoinsApiEndPoints.MarketChartByCoinId(id), new Dictionary<string, object>
                {
                    {"vs_currency", string.Join(",",vsCurrency)},
                    {"days", days}
                }));
        }
        
        
    }
    
}