using Bookstore.Maui.Models;
using Bookstore.Maui.Services;
using System.Collections.ObjectModel;

namespace Bookstore.Maui.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {
        private readonly AuthorService _authorService;
        private readonly BookService _bookService;
        private readonly IAlertService _alertService;

        public ObservableCollection<Author> Authors { get; } = new ObservableCollection<Author>();

        private Book _book = new();
        public Book Book
        {
            get => _book;
            private set => SetProperty(ref _book, value);
        }

        private Author _selectedAuthor;
        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set => SetProperty(ref _selectedAuthor, value);
        }

        public Command SaveCommand { get; }

        public BookDetailViewModel(AuthorService authorService, BookService bookService, IAlertService alertService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _alertService = alertService;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public async Task LoadAuthorsAsync()
        {
            var authors = await _authorService.GetAuthorsAsync();
            Authors.Clear();
            foreach (var author in authors)
            {
                Authors.Add(author);
            }

            SelectedAuthor = Authors.FirstOrDefault(a => a.Id == Book.Author?.Id);
            IsBusy = false;
        }

        public async void LoadBook(int? bookId)
        {
            IsBusy = true;
            Title = bookId.HasValue ? "Edit Book Details" : "Add New Book Details";
            if (bookId.HasValue)
            {
                var book = await _bookService.GetBookByIdAsync(bookId.Value);
                if (book != null)
                {
                    Book = book;
                }
            }

            await LoadAuthorsAsync();
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Book.Title))
            {
                await _alertService.ShowAlert("Required", "Title is required.");
                return;
            }

            if (SelectedAuthor == null)
            {
                await _alertService.ShowAlert("Required", "Author is required.");
                return;
            }

            IsBusy = true;
            Book updatedBook;
            if (Book.Id == 0)
            {
                updatedBook = await _bookService.AddBookAsync(Book.Title, SelectedAuthor.Id);
            }
            else
            {
                updatedBook = await _bookService.UpdateBookAsync(Book.Id, Book.Title, SelectedAuthor.Id);
            }
            IsBusy = false;

            if (updatedBook != null)
            {
                await Shell.Current.GoToAsync($"..?Id={updatedBook.Id}&Title={Uri.EscapeDataString(updatedBook.Title)}&AuthorId={updatedBook.Author.Id}&AuthorName={Uri.EscapeDataString(updatedBook.Author.Name)}&AuthorBio={Uri.EscapeDataString(updatedBook.Author.Bio)}");
            }
        }
    }
}
