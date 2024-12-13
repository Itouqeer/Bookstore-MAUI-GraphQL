using Bookstore.Maui.Services;

namespace Bookstore.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<BookService>();
            DependencyService.Register<GraphQLClientService>();

            MainPage = new AppShell();
        }
    }
}
