using Microsoft.EntityFrameworkCore;
using Orders.Backend.Helpers;
using Orders.Shared.DTOs;
using System.Security.Cryptography.Xml;
using Taller.Backend.Data;
using Taller.Backend.Repositories.Interfaces;
using Taller.Shared.Responses;

namespace Taller.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;
    public GenericRepository(DataContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }
    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<T>
            {
                Message = exception.Message 

            };
        }
        
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "No se encontró el registro"
            };
        }
        _entity.Remove(row);
        try 
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true
            };
        }
        catch
        {
            return new ActionResponse<T>
            {
                Message = "No se pudo eliminar el registro, puede que esté siendo usado por otra entidad"
            };
        }
    }

    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
       var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "No se encontró el registro"
            };
        }
        return new ActionResponse<T>
        {
            WasSuccess = true,
            Result = row
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => new ActionResponse<IEnumerable<T>>
    {
        WasSuccess = true,
        Result = await _entity.ToListAsync()
    };

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    private ActionResponse<T> ExceptionActionResponse(Exception exception)
    {
        return new ActionResponse<T>
        {
            WasSuccess = false,
            Message = exception.Message
        };
    }

    public async Task<ActionResponse<IEnumerable<T>>> SearchByNameOrLastNameAsync(string query)
    {
        var result = new List<T>();
        if (typeof(T).Name == "Employee")
        {
            // Assuming you have a DbSet<Employee> named Employees in your context
            var employees = _context.Set<T>().AsQueryable();
            result = employees
                .Where(e =>
                    EF.Property<string>(e, "FirstName").Contains(query) ||
                    EF.Property<string>(e, "LastName").Contains(query))
                .ToList();
        }

        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = result
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            if (typeof(T).Name == "Employee")
            {
                queryable = queryable.Where(x =>
                    EF.Property<string>(x, "FirstName").ToLower().Contains(pagination.Filter.ToLower()) ||
                    EF.Property<string>(x, "LastName").ToLower().Contains(pagination.Filter.ToLower())
                );
            }
            else
            {
            }
        }

        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

}
