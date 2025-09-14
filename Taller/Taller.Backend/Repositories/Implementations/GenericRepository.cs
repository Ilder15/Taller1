using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResponse<T>> AddAsync(T entity)
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

    public async Task<ActionResponse<T>> DeleteAsync(int id)
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

    public async Task<ActionResponse<T>> GetAsync(int id)
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

    public async Task<ActionResponse<IEnumerable<T>>> GetAsync() => new ActionResponse<IEnumerable<T>>
    {
        WasSuccess = true,
        Result = await _entity.ToListAsync()
    };

    public async Task<ActionResponse<T>> UpdateAsync(T entity)
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


}
