using Bookstore.Api.GraphQL.Queries;

namespace Bookstore.Api.GraphQL
{
    public class Query
    {
        public BookQuery BookQuery { get; }
        public AuthorQuery AuthorQuery { get; }

        public Query(BookQuery bookQuery, AuthorQuery authorQuery)
        {
            BookQuery = bookQuery;
            AuthorQuery = authorQuery;
        }
    }
}
