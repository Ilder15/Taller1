using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Backend.Data;
using Taller.Backend.UnitOfWork.Interfaces;
using Taller.Shared.Entities;
namespace Taller.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeesController : GenericController<Employee>

{
    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork) : base(unitOfWork)
    {
    }
}