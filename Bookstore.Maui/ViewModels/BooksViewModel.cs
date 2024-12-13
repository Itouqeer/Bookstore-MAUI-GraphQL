using System.Collections.ObjectModel;
using Bookstore.Maui.Models;
using Bookstore.Maui.Services;
using Bookstore.Maui.Views;

namespace Bookstore.Maui.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        private readonly BookService _bookService;

        public ObservableCollection<Book> Books { get; } = new();

        public Command LoadBooksCommand { get; }
        public Command AddBookCommand { get; }
        public Command<Book> EditBookCommand { get; }
        public Command<Book> DeleteBookCommand { get; }

        public BooksViewModel(BookService bookService)
        {
            _bookService = bookService;

            LoadBooksCommand = new Command(async () => await LoadBooksAsync());
            AddBookCommand = new Command(async () => await AddBookAsync());
            EditBookCommand = new Command<Book>(async (book) => await EditBookAsync(book));
            DeleteBookCommand = new Command<Book>(async (book) => await DeleteBookAsync(book));

            LoadBooksCommand.Execute(null);
        }

        public async Task LoadBooksAsync()
        {
            try
            {
                IsBusy = true;
                var books = await _bookService.GetBooksAsync();
                Books.Clear();
                foreach (var book in books)
                    Books.Add(book);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddBookAsync()
        {
            await Shell.Current.GoToAsync(nameof(BookDetailPage));
        }

        private async Task EditBookAsync(Book book)
        {
            if (book == null) return;

            await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?Id={book.Id}");
        }

        private async Task DeleteBookAsync(Book book)
        {
            if (book == null) return;

            bool isDeleted = await _bookService.DeleteBookAsync(book.Id);
            if (isDeleted)
                Books.Remove(book);
        }

        public void UpdateBookInList(Book updatedBook)
        {
            var index = Books.IndexOf(Books.FirstOrDefault(b => b.Id == updatedBook.Id));
            if (index >= 0)
            {
                Books[index] = updatedBook;
            }
            else
            {
                Books.Add(updatedBook);
            }
        }
    }
}
