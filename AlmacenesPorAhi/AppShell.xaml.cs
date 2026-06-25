using AlmacenesPorAhi.Views;

namespace AlmacenesPorAhi;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registro de rutas explícitas para navegación jerárquica
        Routing.RegisterRoute(nameof(ProductoListPage), typeof(ProductoListPage));
        Routing.RegisterRoute(nameof(ProductoFormPage), typeof(ProductoFormPage));

        // Rutas del módulo de Clientes
        Routing.RegisterRoute(nameof(ClienteListPage), typeof(ClienteListPage));
        Routing.RegisterRoute(nameof(ClienteFormPage), typeof(ClienteFormPage));
    }
}