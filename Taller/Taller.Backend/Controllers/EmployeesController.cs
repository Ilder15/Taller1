using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Backend.Data;
using Taller.Shared.Entities;
namespace Taller.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeesController : ControllerBase
{  
    public DataContext _Context { get; }
    public EmployeesController(DataContext context)
    {
        _Context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _Context.Employees.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Post(Employee employee)
    {
        _Context.Employees.Add(employee);
        await _Context.SaveChangesAsync(); 
        return Ok(employee);
    }


}
