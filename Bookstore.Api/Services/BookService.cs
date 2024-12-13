using Bookstore.Api.DTOs;
using Bookstore.Api.Models;
using Bookstore.Api.Repositories;

namespace Bookstore.Api.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto?> AddBookAsync(BookInputDto bookDto);
        Task<BookDto?> UpdateBookAsync(int id, BookInputDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }

    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Author> authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync(b => b.Author);
            return books.Select(book => MapToBookDto(book)).ToList();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id, b => b.Author);
            return book == null ? null : MapToBookDto(book);
        }

        public async Task<BookDto?> AddBookAsync(BookInputDto input)
        {
            var author = await _authorRepository.GetByIdAsync(input.AuthorId);
            if (author == null) return null;

            var book = MapToBook(input);

            await _bookRepository.AddAsync(book);
            return MapToBookDto(book, author);
        }

        public async Task<BookDto?> UpdateBookAsync(int id, BookInputDto input)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            var author = await _authorRepository.GetByIdAsync(input.AuthorId);
            if (author == null) return null;

            book.Title = input.Title;
            book.AuthorId = input.AuthorId;

            await _bookRepository.UpdateAsync(book);
            return MapToBookDto(book, author);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            await _bookRepository.DeleteAsync(id);
            return true;
        }

        private BookDto MapToBookDto(Book book, Author? author = null)
        {
            author ??= book.Author;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = author == null
                    ? null
                    : new AuthorDto
                    {
                        Id = author.Id,
                        Name = author.Name,
                        Bio = author.Bio
                    }
            };
        }

        private Book MapToBook(BookInputDto input)
        {
            return new Book
            {
                Title = input.Title,
                AuthorId = input.AuthorId
            };
        }
    }
}
