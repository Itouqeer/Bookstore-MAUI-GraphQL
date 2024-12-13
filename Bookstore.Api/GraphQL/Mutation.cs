using Bookstore.Api.GraphQL.Mutations;

namespace Bookstore.Api.GraphQL
{
    public class Mutation
    {
        public BookMutation BookMutation { get; }
        public AuthorMutation AuthorMutation { get; }

        public Mutation(BookMutation bookMutation, AuthorMutation authorMutation)
        {
            BookMutation = bookMutation;
            AuthorMutation = authorMutation;
        }
    }
}
