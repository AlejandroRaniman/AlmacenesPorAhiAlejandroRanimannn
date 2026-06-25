using AlmacenesPorAhi.Models;

namespace AlmacenesPorAhi.Services;

public interface IClienteService
{
    Task<List<Cliente>> GetClientesAsync();
    Task<Cliente?> GetClienteByIdAsync(int id);
    Task<bool> CreateClienteAsync(Cliente cliente);
    Task<bool> UpdateClienteAsync(Cliente cliente);
    Task<bool> DeleteClienteAsync(int id);
}