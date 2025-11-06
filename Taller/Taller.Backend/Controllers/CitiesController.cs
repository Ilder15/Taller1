using Microsoft.AspNetCore.Mvc;
using Taller.Backend.Controllers;
using Taller.Backend.UnitOfWork.Interfaces;
using Taller.Backend.UnitsOfWork.Interfaces;
using Taller.Shared.DTOs;
using Taller.Shared.Entities;

namespace Taller.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : GenericController<City>
{
    private readonly ICitiesUnitOfWork _citiesUnitOfWork;

    public CitiesController(IGenericUnitOfWork<City> unitOfWork, ICitiesUnitOfWork citiesUnitOfWork) : base(unitOfWork)
    {
        _citiesUnitOfWork = citiesUnitOfWork;
    }

}