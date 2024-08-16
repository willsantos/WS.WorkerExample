using Microsoft.EntityFrameworkCore;
using WS.WorkerExample.Application.Interfaces;
using WS.WorkerExample.Data;
using WS.WorkerExample.Data.Dto;
using WS.WorkerExample.Data.Entities;

namespace WS.WorkerExample.Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IHttpService _httpService;
        private readonly AppDbContext _context;
        private readonly ILogger<WorkerService> _logger;

        public WorkerService(IHttpService httpService, AppDbContext context, ILogger<WorkerService> logger)
        {
            _httpService = httpService;
            _context = context;
            _logger = logger;
        }

        public Task<List<Pokemon>> GetFuncionarioList(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Funcionario?> GetFuncionarioByName(string name, CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(x => x.Nome.Equals(name), cancellationToken);
        }

        public async Task<Pokemon?> GetPokemonApi()
        {
            var random = new Random();
            var id = random.Next(1, 20);
            return await _httpService.GetPokemon($"https://pokeapi.co/api/v2/pokemon/{id}");
        }

        public async Task<bool> HealthCheck()
        {
            var statusSite = await _httpService.CheckStatusSite("https://pokeapi.co/api/v2");

            if (statusSite != System.Net.HttpStatusCode.OK)
            {
                _logger.LogError("Site is down at: {time}", DateTimeOffset.Now);
                return false;
            }

            return true;
        }

        public async Task SaveFuncionario(Funcionario funcionario, CancellationToken cancellationToken)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Pokemon: {name} cadastrado como funcionario com sucesso!", funcionario.Nome);
        }
    }
}