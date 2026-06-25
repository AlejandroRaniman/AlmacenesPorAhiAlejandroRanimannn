using AlmacenesPorAhi.Data;
using AlmacenesPorAhi.Services;
using AlmacenesPorAhi.ViewModels;
using AlmacenesPorAhi.Views;

namespace AlmacenesPorAhi;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateMauiAppBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Configuración de persistencia (Base de datos local)
        builder.Services.AddDbContext<AppDbContext>();

        // Registro de Capa de Servicios
        builder.Services.AddScoped<IProductoService, ProductoService>();
        builder.Services.AddScoped<IClienteService, ClienteService>();

        // Registro de ViewModels
        builder.Services.AddTransient<MainMenuViewModel>();
        builder.Services.AddTransient<ProductoListViewModel>();
        builder.Services.AddTransient<ProductoFormViewModel>();
        builder.Services.AddTransient<ClienteListViewModel>();
        builder.Services.AddTransient<ClienteFormViewModel>();

        // Registro de Vistas (Views)
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<ProductoListPage>();
        builder.Services.AddTransient<ProductoFormPage>();
        builder.Services.AddTransient<ClienteListPage>();
        builder.Services.AddTransient<ClienteFormPage>();

        return builder.Build();
    }
}