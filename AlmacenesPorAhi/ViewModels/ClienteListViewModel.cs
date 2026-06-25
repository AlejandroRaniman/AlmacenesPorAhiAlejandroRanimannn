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
        foreach (var c in lista)
        {
            Clientes.Add(c);
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
        await Shell.Current.GoToAsync($"{nameof(ClienteFormPage)}?Id={cliente.Id}");
    }

    [RelayCommand]
    private async Task DeleteAsync(Cliente cliente)
    {
        if (cliente == null) return;

        bool confirm = await Shell.Current.DisplayAlert("Confirmar", $"¿Desea eliminar a {cliente.Nombre} {cliente.ApellidoPaterno}?", "Sí", "No");
        if (confirm)
        {
            await _clienteService.DeleteClienteAsync(cliente.Id);
            await LoadClientesAsync();
        }
    }
}