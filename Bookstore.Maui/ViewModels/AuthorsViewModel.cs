using Bookstore.Maui.Models;
using Bookstore.Maui.Services;
using Bookstore.Maui.Views;
using System.Collections.ObjectModel;

namespace Bookstore.Maui.ViewModels
{
    public class AuthorsViewModel : BaseViewModel
    {
        private readonly AuthorService _authorService;

        public ObservableCollection<Author> Authors { get; } = new();

        public Command LoadAuthorsCommand { get; }
        public Command AddAuthorCommand { get; }
        public Command<Author> EditAuthorCommand { get; }

        public AuthorsViewModel(AuthorService authorService)
        {
            _authorService = authorService;

            LoadAuthorsCommand = new Command(async () => await LoadAuthorsAsync());
            AddAuthorCommand = new Command(async () => await AddAuthorAsync());
            EditAuthorCommand = new Command<Author>(async (author) => await EditAuthorAsync(author));

            LoadAuthorsCommand.Execute(null);
        }

        public async Task LoadAuthorsAsync()
        {
            try
            {
                IsBusy = true;
                var authors = await _authorService.GetAuthorsAsync();
                Authors.Clear();
                foreach (var author in authors)
                    Authors.Add(author);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddAuthorAsync()
        {
            await Shell.Current.GoToAsync(nameof(AuthorDetailPage));
        }

        private async Task EditAuthorAsync(Author author)
        {
            if (author == null) return;

            await Shell.Current.GoToAsync($"{nameof(AuthorDetailPage)}?Id={author.Id}");
        }

        public void UpdateAuthorInList(Author updatedAuthor)
        {
            var index = Authors.IndexOf(Authors.FirstOrDefault(b => b.Id == updatedAuthor.Id));
            if (index >= 0)
            {
                Authors[index] = updatedAuthor;
            }
            else
            {
                Authors.Add(updatedAuthor);
            }
        }
    }
}
