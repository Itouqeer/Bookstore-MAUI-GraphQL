namespace Bookstore.Maui.Services
{
    public interface IAlertService
    {
        Task ShowAlert(string title, string message);
    }

    public class AlertService : IAlertService
    {
        public async Task ShowAlert(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");
        }
    }
}
