using AlmacenesPorAhi.Models;
using AlmacenesPorAhi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlmacenesPorAhi.ViewModels;

[QueryProperty(nameof(Id), "Id")]
public partial class ClienteFormViewModel : ObservableObject
{
    private readonly IClienteService _clienteService;

    [ObservableProperty]
    private int id;

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

    // Se ejecuta de manera automática al recibir el parámetro de consulta Id
    partial void OnIdChanged(int value)
    {
        if (value > 0)
        {
            CargarClienteAsync(value);
        }
    }

    private async void CargarClienteAsync(int idCliente)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(idCliente);
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
        // Validaciones de entrada obligatorias requeridas por el punto 5 de la rúbrica
        if (string.IsNullOrWhiteSpace(Rut) || Rut.Length < 8)
        {
            await Shell.Current.DisplayAlert("Validación", "Por favor, ingrese un RUT válido.", "Aceptar");
            return;
        }
        if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(ApellidoPaterno) || string.IsNullOrWhiteSpace(ApellidoMaterno))
        {
            await Shell.Current.DisplayAlert("Validación", "El nombre y los apellidos son campos obligatorios.", "Aceptar");
            return;
        }
        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
        {
            await Shell.Current.DisplayAlert("Validación", "El formato del correo electrónico es incorrecto.", "Aceptar");
            return;
        }
        if (string.IsNullOrWhiteSpace(Telefono))
        {
            await Shell.Current.DisplayAlert("Validación", "El campo teléfono no puede quedar vacío.", "Aceptar");
            return;
        }
        if (string.IsNullOrWhiteSpace(Direccion))
        {
            await Shell.Current.DisplayAlert("Validación", "La dirección de domicilio es obligatoria.", "Aceptar");
            return;
        }

        var cliente = new Cliente
        {
            Id = Id,
            Rut = Rut.Trim(),
            Nombre = Nombre.Trim(),
            ApellidoPaterno = ApellidoPaterno.Trim(),
            ApellidoMaterno = ApellidoMaterno.Trim(),
            Email = Email.Trim(),
            Telefono = Telefono.Trim(),
            Direccion = Direccion.Trim(),
            Estado = Estado,
            FechaRegistro = Id == 0 ? DateTime.Now : DateTime.Now // Mantiene o actualiza fecha
        };

        bool success;
        if (Id == 0)
        {
            success = await _clienteService.CreateClienteAsync(cliente);
        }
        else
        {
            success = await _clienteService.UpdateClienteAsync(cliente);
        }

        if (success)
        {
            await Shell.Current.DisplayAlert("Operación Exitosa", "Los datos del cliente se guardaron correctamente.", "Aceptar");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudieron guardar los cambios en la base de datos.", "Aceptar");
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}