using AlmacenesPorAhi.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlmacenesPorAhi.ViewModels;

public partial class MainMenuViewModel : ObservableObject
{
    [RelayCommand]
    private async Task IrAInventarioAsync()
    {
        await Shell.Current.GoToAsync(nameof(ProductoListPage));
    }

    [RelayCommand]
    private async Task IrAClientesAsync()
    {
        // Navega de forma fluida hacia la interfaz del listado de clientes
        await Shell.Current.GoToAsync(nameof(ClienteListPage));
    }
}