
using Taller.Shared.DTOs;
using Taller.Shared.Entities;
using Taller.Shared.Responses;

namespace Taller.Backend.UnitsOfWork.Interfaces;

public interface IStatesUnitOfWork
{
    Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<State>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<State>>> GetAsync();
}