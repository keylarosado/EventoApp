using System.Globalization;

namespace EventoApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        // O logger de depuração é opcional. Foi removido para evitar
        // dependência de Microsoft.Extensions.Logging.Debug no projeto.
        return builder.Build();
    }
}
