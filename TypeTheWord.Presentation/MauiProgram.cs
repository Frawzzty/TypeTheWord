using Microsoft.Extensions.Logging;

using Domain.Entities.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypeTheWord.App.Interfaces;
using TypeTheWord.App.Services;
using TypeTheWord.Infrastructure.Connections;
using TypeTheWord.Infrastructure.Repositories;
using TypeTheWord.Presentation.ViewModels;

namespace TypeTheWord.Presentation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //Usersecrets //Does not run on android emulator it seems
            builder.Configuration.AddUserSecrets<App>();

            //DB
            builder.Services.AddScoped<MongoDbConnection>();
            builder.Services.AddScoped<SqlLiteDbContext>();
            

            builder.Services.AddScoped<IWordGameService, WordGameService>();
            builder.Services.AddScoped<IwordSetRepository, WordSetRepositoryMongoDb>(); //Switch between MongoRepo and SQLiteRepo
            builder.Services.AddScoped<IWordSetService, WordSetService>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();

            //Play pages
            builder.Services.AddTransient<Views.PlayPages.WordGamePlayPage>();
            builder.Services.AddTransient<ViewModels.PlayPages.WordGamePlayPageViewModel>();

            //Admin pages
            builder.Services.AddTransient<Views.AdminPages.AdminSelectWordSetPage>();
            builder.Services.AddTransient<ViewModels.AdminPages.AdminSelectWordSetPageViewModel>();

            builder.Services.AddTransient<Views.AdminPages.AdminEditWordSetPage>();
            builder.Services.AddTransient<ViewModels.AdminPages.AdminEditWordSetPageViewModel>();

            return builder.Build();
        }
    }
}
