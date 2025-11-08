using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Backend.Data;
using Taller.Backend.UnitOfWork.Interfaces;
using Taller.Shared.Entities;
namespace Taller.Backend.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class EmployeesController : GenericController<Employee>
{
    private readonly IGenericUnitOfWork<Employee> _unitOfWork;

    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("Name")]
    public async Task<IActionResult> SearchByNameOrLastName([FromQuery] string query)
    {
        var result = await _unitOfWork.SearchByNameOrLastNameAsync(query);
        if (result.WasSuccess)
            return Ok(result.Result);

        return BadRequest(result.Message);
    }
}