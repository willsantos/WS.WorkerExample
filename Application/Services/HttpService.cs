using System.Net;
using System.Text.Json;
using WS.WorkerExample.Application.Interfaces;
using WS.WorkerExample.Data.Dto;

namespace WS.WorkerExample.Application.Services
{
    public class HttpService : IHttpService
    {
        public async Task<HttpStatusCode> CheckStatusSite(string url)
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);
                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.NotFound;
            }
        }

        public async Task<Pokemon?> GetPokemon(string url)
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Pokemon>(content);
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}