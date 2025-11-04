using Microsoft.AspNetCore.Mvc;
using Taller.Backend.Controllers;
using Taller.Backend.UnitOfWork.Interfaces;
using Taller.Shared.Entities;

namespace Taller.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : GenericController<City>
{
    public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
    {
    }
}
