using AlmacenesPorAhi.Models;
using AlmacenesPorAhi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Documents;

namespace AlmacenesPorAhi.ViewModels;

[QueryProperty(nameof(ClienteId), "Id")]
public partial class ClienteFormViewModel : ObservableObject
{
    private readonly IClienteService _clienteService;

    [ObservableProperty] private int clienteId;
    [ObservableProperty] private string rut = string.Empty;
    [ObservableProperty] private string nombre = string.Empty;
    [ObservableProperty] private string apellidoPaterno = string.Empty;
    [ObservableProperty] private string apellidoMaterno = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string telefono = string.Empty;
    [ObservableProperty] private string direccion = string.Empty;
    [ObservableProperty] private string estado = "Activo";

    public ClienteFormViewModel(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    partial void OnClienteIdChanged(int value)
    {
        if (value > 0)
        {
            LoadClienteAsync(value);
        }
    }

    private async void LoadClienteAsync(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente != null)
        {
            Rut = cliente.Rut;
            Nombre = cliente.Nombre;
            ApellidoPaterno = cliente.ApellidoPaterno;
            ApellidoMaterno = cliente.ApellidoMaterno;
            Email = cliente.Email;
            Telefono = cliente.Telefono;
            Direccion = cliente.Direccion;
            Estado = cliente.Estado;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Rut) || string.IsNullOrWhiteSpace(Nombre))
        {
            await Shell.Current.DisplayAlert("Error", "El RUT y el Nombre son obligatorios.", "OK");
            return;
        }

        var cliente = new Cliente
        {
            Id = ClienteId,
            Rut = Rut,
            Nombre = Nombre,
            ApellidoPaterno = ApellidoPaterno,
            ApellidoMaterno = ApellidoMaterno,
            Email = Email,
            Telefono = Telefono,
            Direccion = Direccion,
            Estado = Estado,
            FechaRegistro = DateTime.Now
        };

        bool result;
        if (ClienteId == 0)
        {
            result = await _clienteService.CreateClienteAsync(cliente);
        }
        else
        {
            result = await _clienteService.UpdateClienteAsync(cliente);
        }

        if (result)
        {
            await Shell.Current.DisplayAlert("Éxito", "Cliente guardado correctamente.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Ocurrió un problema al guardar.", "OK");
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}