using Bookstore.Maui.Models;
using Bookstore.Maui.Services;

namespace Bookstore.Maui.ViewModels
{
    public class AuthorDetailViewModel : BaseViewModel
    {
        private readonly AuthorService _authorService;
        private readonly IAlertService _alertService;

        private Author _author = new();
        public Author Author
        {
            get => _author;
            private set => SetProperty(ref _author, value);
        }

        public Command SaveCommand { get; }

        public AuthorDetailViewModel(AuthorService authorService, IAlertService alertService)
        {
            _authorService = authorService;
            _alertService = alertService;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public async void LoadAuthor(int? authorId)
        {
            IsBusy = true;
            Title = authorId.HasValue ? "Edit Author Details" : "Add New Author Details";
            if (authorId.HasValue)
            {
                var author = await _authorService.GetAuthorByIdAsync(authorId.Value);
                if (author != null)
                {
                    Author = author;
                }
            }
            IsBusy = false;
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Author.Name))
            {
                await _alertService.ShowAlert("Required", "Name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Author.Bio))
            {
                await _alertService.ShowAlert("Required", "Bio is required.");
                return;
            }

            IsBusy = true;
            Author updatedAuthor;
            if (Author.Id == 0)
            {
                updatedAuthor = await _authorService.AddAuthorAsync(Author.Name, Author.Bio);
            }
            else
            {
                updatedAuthor = await _authorService.UpdateAuthorAsync(Author.Id, Author.Name, Author.Bio);
            }
            IsBusy = false;

            if (updatedAuthor != null)
            {
                await Shell.Current.GoToAsync($"..?Id={updatedAuthor.Id}&Name={Uri.EscapeDataString(updatedAuthor.Name)}&Bio={updatedAuthor.Bio}");
            }
        }
    }
}
