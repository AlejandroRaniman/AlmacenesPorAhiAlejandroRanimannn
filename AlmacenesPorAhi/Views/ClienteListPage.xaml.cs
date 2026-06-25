using AlmacenesPorAhi.ViewModels;

namespace AlmacenesPorAhi.Views;

public partial class ClienteListPage : ContentPage
{
    public ClienteListPage(ClienteListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Recarga automática de datos al volver a la pantalla
        if (BindingContext is ClienteListViewModel vm)
        {
            vm.LoadClientesCommand.Execute(null);
        }
    }
}