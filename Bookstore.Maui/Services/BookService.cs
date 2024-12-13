using Bookstore.Maui.GraphQL.Mutations;
using Bookstore.Maui.GraphQL.Queries;
using Bookstore.Maui.Models;
using System.Collections.ObjectModel;

namespace Bookstore.Maui.Services
{
    public class BookService
    {
        private readonly GraphQLClientService _graphqlClientService;

        public BookService(GraphQLClientService graphqlClientService)
        {
            _graphqlClientService = graphqlClientService;
        }

        public async Task<ObservableCollection<Book>> GetBooksAsync()
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(BookQueries.GetBooksQuery);
            var books = response?.bookQuery?.books?.ToObject<ObservableCollection<Book>>() ?? new ObservableCollection<Book>();
            return books;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(BookQueries.GetBookById, new { id });
            var book = response?.bookQuery?.bookById;
            if (book == null) return null;

            return new Book
            {
                Id = book.id,
                Title = book.title,
                Author = new Author
                {
                    Id = book.author.id,
                    Name = book.author.name,
                    Bio = book.author.bio
                }
            };
        }

        public async Task<Book> AddBookAsync(string title, int authorId)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(BookMutations.AddBookMutation, new { title, authorId });
            return response?.bookMutation?.addBook?.ToObject<Book>();
        }

        public async Task<Book> UpdateBookAsync(int id, string title, int authorId)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(BookMutations.UpdateBook, new { id, title, authorId });
            return response?.bookMutation?.updateBook?.ToObject<Book>();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(BookMutations.DeleteBook, new { id });
            return response?.bookMutation?.deleteBook == true;
        }
    }
}
