using Bookstore.Api.DTOs;
using Bookstore.Api.Services;

namespace Bookstore.Api.GraphQL.Mutations
{
    public class AuthorMutation
    {
        private readonly IAuthorService _authorService;

        public AuthorMutation(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorInputDto input) => await _authorService.AddAuthorAsync(input);

        public async Task<AuthorDto> UpdateAuthorAsync(int id, AuthorInputDto input) => await _authorService.UpdateAuthorAsync(id, input);

        public async Task<bool> DeleteAuthorAsync(int id) => await _authorService.DeleteAuthorAsync(id);
    }
}
