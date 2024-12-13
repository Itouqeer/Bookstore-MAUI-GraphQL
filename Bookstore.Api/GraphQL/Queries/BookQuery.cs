using Bookstore.Api.DTOs;
using Bookstore.Api.Services;

namespace Bookstore.Api.GraphQL.Queries
{
    public class BookQuery
    {
        private readonly IBookService _bookService;

        public BookQuery(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<List<BookDto>> GetBooksAsync() => await _bookService.GetAllBooksAsync();

        public async Task<BookDto> GetBookByIdAsync(int id) => await _bookService.GetBookByIdAsync(id);
    }
}
