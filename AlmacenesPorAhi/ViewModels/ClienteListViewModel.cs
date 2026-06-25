using System.Collections.ObjectModel;
using AlmacenesPorAhi.Models;
using AlmacenesPorAhi.Services;
using AlmacenesPorAhi.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlmacenesPorAhi.ViewModels;

public partial class ClienteListViewModel : ObservableObject
{
    private readonly IClienteService _clienteService;

    [ObservableProperty]
    private ObservableCollection<Cliente> clientes = new();

    public ClienteListViewModel(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [RelayCommand]
    public async Task LoadClientesAsync()
    {
        var lista = await _clienteService.GetClientesAsync();
        Clientes.Clear();
        foreach (var cliente in lista)
        {
            Clientes.Add(cliente);
        }
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await Shell.Current.GoToAsync(nameof(ClienteFormPage));
    }

    [RelayCommand]
    private async Task GoToEditAsync(Cliente cliente)
    {
        if (cliente == null) return;
        // Pasa el ID como parámetro de consulta
        await Shell.Current.GoToAsync($"{nameof(ClienteFormPage)}?Id={cliente.Id}");
    }

    [RelayCommand]
    private async Task DeleteAsync(Cliente cliente)
    {
        if (cliente == null) return;

        bool confirm = await Shell.Current.DisplayAlert("Confirmar Eliminación",
            $"¿Está seguro de eliminar al cliente {cliente.Nombre} {cliente.ApellidoPaterno}?", "Eliminar", "Cancelar");

        if (confirm)
        {
            bool success = await _clienteService.DeleteClienteAsync(cliente.Id);
            if (success)
            {
                await LoadClientesAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el registro.", "OK");
            }
        }
    }
}