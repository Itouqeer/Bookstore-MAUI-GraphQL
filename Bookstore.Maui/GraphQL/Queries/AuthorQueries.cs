namespace Bookstore.Maui.GraphQL.Queries
{
    public class AuthorQueries
    {
        public const string GetAuthorsQuery = @"
            query {
                authorQuery {  
                    authors {
                        id,
                        name,
                        bio
                    }
                }
            }";

        public const string GetAuthorById = @"
            query ($id: Int!) {
                authorQuery {  
                    authorById(id: $id) {
                        id,
                        name,
                        bio
                    }
                }
            }";
    }
}
