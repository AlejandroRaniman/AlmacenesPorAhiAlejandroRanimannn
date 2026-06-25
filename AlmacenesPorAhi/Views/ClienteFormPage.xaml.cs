using AlmacenesPorAhi.ViewModels;

namespace AlmacenesPorAhi.Views;

public partial class ClienteFormPage : ContentPage
{
    public ClienteFormPage(ClienteFormViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}