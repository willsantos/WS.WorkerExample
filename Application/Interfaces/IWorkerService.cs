using WS.WorkerExample.Data.Dto;
using WS.WorkerExample.Data.Entities;

namespace WS.WorkerExample.Application.Interfaces
{
    public interface IWorkerService
    {
        Task<Pokemon?> GetPokemonApi();

        Task<Funcionario?> GetFuncionarioByName(string id, CancellationToken cancellationToken);

        Task<List<Pokemon>> GetFuncionarioList(CancellationToken cancellationToken);

        Task SaveFuncionario(Funcionario funcionario, CancellationToken cancellationToken);

        Task<bool> HealthCheck();
    }
}