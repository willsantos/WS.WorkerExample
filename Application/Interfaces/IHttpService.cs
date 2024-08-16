using System.Net;
using WS.WorkerExample.Data.Dto;

namespace WS.WorkerExample.Application.Interfaces
{
    public interface IHttpService
    {
        Task<HttpStatusCode> CheckStatusSite(string url);

        Task<Pokemon?> GetPokemon(string url);
    }
}