using AlmacenesPorAhi.Data;
using AlmacenesPorAhi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlmacenesPorAhi.Services;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;

    public ClienteService(AppDbContext context)
    {
        _context = context;
        // Asegura que la base de datos y tablas estén creadas de forma local
        _context.Database.EnsureCreated();
    }

    public async Task<List<Cliente>> GetClientesAsync()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    public async Task<Cliente?> GetClienteByIdAsync(int id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> CreateClienteAsync(Cliente cliente)
    {
        try
        {
            await _context.Clientes.AddAsync(cliente);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateClienteAsync(Cliente cliente)
    {
        try
        {
            _context.Clientes.Update(cliente);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteClienteAsync(int id)
    {
        try
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }
}