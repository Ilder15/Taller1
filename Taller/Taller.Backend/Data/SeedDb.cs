using Microsoft.EntityFrameworkCore;
using Taller.Backend.UnitOfWork.Interfaces;
using Taller.Shared.Entities;
using Taller.Shared.Enums;

namespace Taller.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task SeedDbAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesFullAsync();
        await CheckEmployeesAsync();
        //await CheckCountriesAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010", "Ilder", "Lopez", "ilder@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);
        await CheckUserAsync("1010", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);
    }

    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                City = _context.Cities.FirstOrDefault(),
                UserType = userType,
            };

            await _usersUnitOfWork.AddUserAsync(user, "123456");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }


    private async Task CheckCountriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }


    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                States = [
                    new State()
                {
                    Name = "Antioquia",
                    Cities = [
                        new City() { Name = "Medellín" },
                        new City() { Name = "Itagüí" },
                        new City() { Name = "Envigado" },
                        new City() { Name = "Bello" },
                        new City() { Name = "Rionegro" },
                    ]
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = [
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Useme" },
                        new City() { Name = "Bosa" },
                    ]
                },
            ]
            });
            _context.Countries.Add(new Country
            {
                Name = "Estados Unidos",
                States = [
                    new State()
                {
                    Name = "Florida",
                    Cities = [
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    ]
                },
                new State()
                    {
                        Name = "Texas",
                        Cities = [
                            new City() { Name = "Houston" },
                            new City() { Name = "San Antonio" },
                            new City() { Name = "Dallas" },
                            new City() { Name = "Austin" },
                            new City() { Name = "El Paso" },
                        ]
                    },
                ]
            });
        }
        await _context.SaveChangesAsync();
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

       
        _context.Employees.Add(new Employee { FirstName = "Pedro", LastName = "Ramírez", FechaHora = new DateTime(2025, 9, 6, 8, 0, 0), IsActive = true, Salary = 12000000 });
        _context.Employees.Add(new Employee { FirstName = "Lucía", LastName = "Morales", FechaHora = new DateTime(2025, 9, 5, 9, 15, 0), IsActive = true, Salary = 13000000 });
        _context.Employees.Add(new Employee { FirstName = "Miguel", LastName = "Castro", FechaHora = new DateTime(2025, 9, 4, 10, 30, 0), IsActive = false, Salary = 11000000 });
        _context.Employees.Add(new Employee { FirstName = "Camila", LastName = "Vargas", FechaHora = new DateTime(2025, 9, 3, 11, 45, 0), IsActive = true, Salary = 12500000 });
        _context.Employees.Add(new Employee { FirstName = "Andrés", LastName = "Jiménez", FechaHora = new DateTime(2025, 9, 2, 12, 10, 0), IsActive = true, Salary = 14000000 });
        _context.Employees.Add(new Employee { FirstName = "Paula", LastName = "Herrera", FechaHora = new DateTime(2025, 9, 1, 13, 20, 0), IsActive = false, Salary = 9500000 });
        _context.Employees.Add(new Employee { FirstName = "Santiago", LastName = "Mendoza", FechaHora = new DateTime(2025, 8, 31, 14, 35, 0), IsActive = true, Salary = 15000000 });
        _context.Employees.Add(new Employee { FirstName = "Isabella", LastName = "Ruiz", FechaHora = new DateTime(2025, 8, 30, 15, 50, 0), IsActive = true, Salary = 15500000 });
        _context.Employees.Add(new Employee { FirstName = "Tomás", LastName = "Silva", FechaHora = new DateTime(2025, 8, 29, 16, 5, 0), IsActive = false, Salary = 10200000 });
        _context.Employees.Add(new Employee { FirstName = "Gabriela", LastName = "Ortega", FechaHora = new DateTime(2025, 8, 28, 17, 20, 0), IsActive = true, Salary = 11800000 });
        _context.Employees.Add(new Employee { FirstName = "Matías", LastName = "Cruz", FechaHora = new DateTime(2025, 8, 27, 8, 30, 0), IsActive = true, Salary = 12300000 });
        _context.Employees.Add(new Employee { FirstName = "Daniela", LastName = "Ríos", FechaHora = new DateTime(2025, 8, 26, 9, 45, 0), IsActive = true, Salary = 12700000 });
        _context.Employees.Add(new Employee { FirstName = "Samuel", LastName = "Peña", FechaHora = new DateTime(2025, 8, 25, 10, 0, 0), IsActive = false, Salary = 9900000 });
        _context.Employees.Add(new Employee { FirstName = "Martina", LastName = "Guerrero", FechaHora = new DateTime(2025, 8, 24, 11, 15, 0), IsActive = true, Salary = 13400000 });
        _context.Employees.Add(new Employee { FirstName = "Emilio", LastName = "Flores", FechaHora = new DateTime(2025, 8, 23, 12, 30, 0), IsActive = true, Salary = 11900000 });
        _context.Employees.Add(new Employee { FirstName = "Renata", LastName = "Navarro", FechaHora = new DateTime(2025, 8, 22, 13, 45, 0), IsActive = false, Salary = 9700000 });
        _context.Employees.Add(new Employee { FirstName = "Diego", LastName = "Reyes", FechaHora = new DateTime(2025, 8, 21, 14, 0, 0), IsActive = true, Salary = 14500000 });
        _context.Employees.Add(new Employee { FirstName = "Julieta", LastName = "Campos", FechaHora = new DateTime(2025, 8, 20, 15, 15, 0), IsActive = true, Salary = 13200000 });
        _context.Employees.Add(new Employee { FirstName = "Alejandro", LastName = "Mora", FechaHora = new DateTime(2025, 8, 19, 16, 30, 0), IsActive = false, Salary = 10100000 });
        _context.Employees.Add(new Employee { FirstName = "Valentina", LastName = "Paredes", FechaHora = new DateTime(2025, 8, 18, 17, 45, 0), IsActive = true, Salary = 13700000 });
        _context.Employees.Add(new Employee { FirstName = "Sebastián", LastName = "Salazar", FechaHora = new DateTime(2025, 8, 17, 8, 0, 0), IsActive = true, Salary = 12100000 });
        _context.Employees.Add(new Employee { FirstName = "Mía", LastName = "Arias", FechaHora = new DateTime(2025, 8, 16, 9, 15, 0), IsActive = true, Salary = 12900000 });
        _context.Employees.Add(new Employee { FirstName = "Nicolás", LastName = "Vega", FechaHora = new DateTime(2025, 8, 15, 10, 30, 0), IsActive = false, Salary = 11200000 });
        _context.Employees.Add(new Employee { FirstName = "Sara", LastName = "Delgado", FechaHora = new DateTime(2025, 8, 14, 11, 45, 0), IsActive = true, Salary = 12600000 });
        _context.Employees.Add(new Employee { FirstName = "Felipe", LastName = "Ramos", FechaHora = new DateTime(2025, 8, 13, 12, 10, 0), IsActive = true, Salary = 14100000 });
        _context.Employees.Add(new Employee { FirstName = "Elena", LastName = "Cortés", FechaHora = new DateTime(2025, 8, 12, 13, 20, 0), IsActive = false, Salary = 9600000 });
        _context.Employees.Add(new Employee { FirstName = "Juan", LastName = "Luna", FechaHora = new DateTime(2025, 8, 11, 14, 35, 0), IsActive = true, Salary = 15300000 });
        _context.Employees.Add(new Employee { FirstName = "Victoria", LastName = "Soto", FechaHora = new DateTime(2025, 8, 10, 15, 50, 0), IsActive = true, Salary = 14900000 });
        _context.Employees.Add(new Employee { FirstName = "Maximiliano", LastName = "Ibarra", FechaHora = new DateTime(2025, 8, 9, 16, 5, 0), IsActive = false, Salary = 10300000 });
        _context.Employees.Add(new Employee { FirstName = "Ariana", LastName = "Mejía", FechaHora = new DateTime(2025, 8, 8, 17, 20, 0), IsActive = true, Salary = 11700000 });
        _context.Employees.Add(new Employee { FirstName = "Bruno", LastName = "Palacios", FechaHora = new DateTime(2025, 8, 7, 8, 30, 0), IsActive = true, Salary = 12400000 });
        _context.Employees.Add(new Employee { FirstName = "Florencia", LastName = "Montoya", FechaHora = new DateTime(2025, 8, 6, 9, 45, 0), IsActive = true, Salary = 12800000 });
        _context.Employees.Add(new Employee { FirstName = "Agustín", LastName = "Escobar", FechaHora = new DateTime(2025, 8, 5, 10, 0, 0), IsActive = false, Salary = 9800000 });
        _context.Employees.Add(new Employee { FirstName = "Carolina", LastName = "Ponce", FechaHora = new DateTime(2025, 8, 4, 11, 15, 0), IsActive = true, Salary = 13300000 });
        _context.Employees.Add(new Employee { FirstName = "Franco", LastName = "Suárez", FechaHora = new DateTime(2025, 8, 3, 12, 30, 0), IsActive = true, Salary = 12000000 });
        _context.Employees.Add(new Employee { FirstName = "Josefina", LastName = "Aguilar", FechaHora = new DateTime(2025, 8, 2, 13, 45, 0), IsActive = false, Salary = 9500000 });
        _context.Employees.Add(new Employee { FirstName = "Benjamín", LastName = "León", FechaHora = new DateTime(2025, 8, 1, 14, 0, 0), IsActive = true, Salary = 14600000 });
        _context.Employees.Add(new Employee { FirstName = "Constanza", LastName = "Maldonado", FechaHora = new DateTime(2025, 7, 31, 15, 15, 0), IsActive = true, Salary = 13100000 });
        _context.Employees.Add(new Employee { FirstName = "Simón", LastName = "Figueroa", FechaHora = new DateTime(2025, 7, 30, 16, 30, 0), IsActive = false, Salary = 10000000 });
        _context.Employees.Add(new Employee { FirstName = "Antonia", LastName = "Carrillo", FechaHora = new DateTime(2025, 7, 29, 17, 45, 0), IsActive = true, Salary = 13800000 });

            await _context.SaveChangesAsync();
        }
    }
}
