using Taller.Shared.DTOs;
using Taller.Shared.Entities;
using Taller.Shared.Responses;

namespace Taller.Backend.UnitOfWork.Interfaces
{
    public interface ICountriesUnitOfWork
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    }
}

