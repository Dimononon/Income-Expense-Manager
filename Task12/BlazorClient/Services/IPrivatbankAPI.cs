using BlazorClient.Models;

namespace BlazorClient.Services
{
    public interface IPrivatbankAPI
    {
        Task<ExchangeRates> GetExchangeRatesAsync(DateTime date);
    }
}