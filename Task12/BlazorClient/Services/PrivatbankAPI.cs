using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClient.Models;

namespace BlazorClient.Services
{
    public class PrivatbankAPI : IPrivatbankAPI
    {
        public async Task<ExchangeRates> GetExchangeRatesAsync(DateTime date)
        {
            HttpClient _httpClient = new HttpClient();

            var apiUrl = $"https://api.privatbank.ua/p24api/exchange_rates?json&date={date:dd.MM.yyyy}";
            var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(responseBody);
            return exchangeRates;
        }
    }
}
