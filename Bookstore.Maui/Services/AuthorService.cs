using Bookstore.Maui.GraphQL.Mutations;
using Bookstore.Maui.GraphQL.Queries;
using Bookstore.Maui.Models;
using System.Collections.ObjectModel;

namespace Bookstore.Maui.Services
{
    public class AuthorService
    {
        private readonly GraphQLClientService _graphqlClientService;

        public AuthorService(GraphQLClientService graphqlClientService)
        {
            _graphqlClientService = graphqlClientService;
        }

        public async Task<ObservableCollection<Author>> GetAuthorsAsync()
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(AuthorQueries.GetAuthorsQuery);
            var authors = response?.authorQuery?.authors?.ToObject<ObservableCollection<Author>>() ?? new ObservableCollection<Author>();
            return authors;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(AuthorQueries.GetAuthorById, new { id });
            var author = response?.authorQuery?.authorById;
            if (author == null) return null;

            return new Author
            {
                Id = author.id,
                Name = author.name,
                Bio = author.bio
            };
        }

        public async Task<Author> AddAuthorAsync(string name, string bio)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(AuthorMutations.AddAuthorMutation, new { name, bio });
            return response?.authorMutation?.addAuthor?.ToObject<Author>();
        }

        public async Task<Author> UpdateAuthorAsync(int id, string name, string bio)
        {
            var response = await _graphqlClientService.SendRequestAsync<dynamic>(AuthorMutations.UpdateAuthor, new { id, name, bio });
            return response?.authorMutation?.updateAuthor?.ToObject<Author>();
        }
    }
}
