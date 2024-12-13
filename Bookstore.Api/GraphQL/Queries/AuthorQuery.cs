using Bookstore.Api.DTOs;
using Bookstore.Api.Services;

namespace Bookstore.Api.GraphQL.Queries
{
    public class AuthorQuery
    {
        private readonly IAuthorService _authorService;

        public AuthorQuery(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<List<AuthorDto>> GetAuthorsAsync() => await _authorService.GetAllAuthorsAsync();

        public async Task<AuthorDto> GetAuthorByIdAsync(int id) => await _authorService.GetAuthorByIdAsync(id);
    }
}
