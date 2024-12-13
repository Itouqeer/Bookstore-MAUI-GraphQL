using Bookstore.Api.DTOs;
using Bookstore.Api.Services;

namespace Bookstore.Api.GraphQL.Mutations
{
    public class BookMutation
    {
        private readonly IBookService _bookService;

        public BookMutation(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookDto> AddBookAsync(BookInputDto input) => await _bookService.AddBookAsync(input);

        public async Task<BookDto> UpdateBookAsync(int id, BookInputDto input) => await _bookService.UpdateBookAsync(id, input);

        public async Task<bool> DeleteBookAsync(int id) => await _bookService.DeleteBookAsync(id);
    }
}
