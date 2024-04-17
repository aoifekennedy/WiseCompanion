using Microsoft.Extensions.Logging;

namespace WiseCompanion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSans");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Arial.ttf", "Arial");
                    fonts.AddFont("Times New Roman.ttf", "Times New Roman");
                    fonts.AddFont("helvetica.ttf", "Helvetica");
                    fonts.AddFont("georgia.ttf", "Georgia");
                    fonts.AddFont("ProximaNovaReg.ttf", "Proxima Nova");
                    fonts.AddFont("calibri.ttf", "Calibri");
                });


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
