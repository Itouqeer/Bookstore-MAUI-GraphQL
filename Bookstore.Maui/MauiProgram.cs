using Bookstore.Maui.GraphQL;
using Bookstore.Maui.Services;
using Bookstore.Maui.ViewModels;
using Bookstore.Maui.Views;
using Microsoft.Extensions.Logging;

namespace Bookstore.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Register fonts
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<AuthorService>();
            builder.Services.AddSingleton<BookService>();

            // Register GraphQL client with required dependencies
            builder.Services.AddSingleton(serviceProvider =>
                new GraphQLClientService(
                    "http://192.168.10.8:5004/graphql",
                    serviceProvider.GetRequiredService<IAlertService>())
            );

            // Register ViewModels and Pages
            builder.Services.AddSingleton<AuthorsViewModel>();
            builder.Services.AddSingleton<BooksViewModel>();
            builder.Services.AddSingleton<AuthorsPage>();
            builder.Services.AddSingleton<BooksPage>();
            builder.Services.AddTransient<AuthorDetailViewModel>();
            builder.Services.AddTransient<BookDetailViewModel>();
            builder.Services.AddTransient<AuthorDetailPage>();
            builder.Services.AddTransient<BookDetailPage>();

            // Configure logging only for DEBUG
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
