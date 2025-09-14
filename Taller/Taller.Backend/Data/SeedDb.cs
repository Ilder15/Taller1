
using Taller.Shared.Entities;

namespace Taller.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedDbAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
    }

    private async Task CheckEmployeesAsync()
    {
        if(!_context.Employees.Any())
        {
            _context.Employees.Add(new Employee
            {
                FirstName = "Ilder",
                LastName = "Lopez",
                FechaHora = DateTime.Now,
                IsActive = true,
                Salary = 20000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Ana",
                LastName = "Gomez",
                FechaHora = DateTime.Now,
                IsActive = true,
                Salary = 15000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Antonio",
                LastName = "Aspilla",
                FechaHora = new DateTime(2025, 9, 14, 10, 30, 0),
                IsActive = true,
                Salary = 12000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Mariana",
                LastName = "Gómez",
                FechaHora = new DateTime(2025, 9, 13, 9, 15, 0),
                IsActive = true,
                Salary = 15000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Carlos",
                LastName = "Pérez",
                FechaHora = new DateTime(2025, 9, 12, 11, 45, 0),
                IsActive = false,
                Salary = 10500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Sofía",
                LastName = "López",
                FechaHora = new DateTime(2025, 9, 11, 14, 20, 0),
                IsActive = true,
                Salary = 13500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Javier",
                LastName = "Rodríguez",
                FechaHora = new DateTime(2025, 9, 10, 8, 0, 0),
                IsActive = true,
                Salary = 11000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Valeria",
                LastName = "Fernández",
                FechaHora = new DateTime(2025, 9, 9, 16, 50, 0),
                IsActive = false,
                Salary = 9800000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "David",
                LastName = "Sánchez",
                FechaHora = new DateTime(2025, 9, 8, 12, 10, 0),
                IsActive = true,
                Salary = 16000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Laura",
                LastName = "Martínez",
                FechaHora = new DateTime(2025, 9, 7, 7, 30, 0),
                IsActive = true,
                Salary = 14200000
            });
            await _context.SaveChangesAsync();
        }
    }
}
