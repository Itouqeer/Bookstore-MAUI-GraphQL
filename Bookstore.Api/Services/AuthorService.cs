using Bookstore.Api.DTOs;
using Bookstore.Api.Models;
using Bookstore.Api.Repositories;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace Bookstore.Api.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> AddAuthorAsync(AuthorInputDto authorDto);
        Task<AuthorDto?> UpdateAuthorAsync(int id, AuthorInputDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(MapToAuthorDto).ToList();
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author == null ? null : MapToAuthorDto(author);
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorInputDto input)
        {
            var author = MapToAuthor(input);
            await _authorRepository.AddAsync(author);
            return MapToAuthorDto(author);
        }

        public async Task<AuthorDto?> UpdateAuthorAsync(int id, AuthorInputDto input)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;

            author.Name = input.Name;
            author.Bio = input.Bio;

            await _authorRepository.UpdateAsync(author);
            return MapToAuthorDto(author);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            try
            {
                await _authorRepository.DeleteAsync(id);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private AuthorDto MapToAuthorDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio
            };
        }

        private Author MapToAuthor(AuthorInputDto input)
        {
            return new Author
            {
                Name = input.Name,
                Bio = input.Bio
            };
        }
    }
}
