namespace Bookstore.Maui.GraphQL.Queries
{
    public static class BookQueries
    {
        public const string GetBooksQuery = @"
            query {
                bookQuery {  
                    books {
                        id,
                        title,
                        author {
                            id,
                            name,
                            bio
                        }
                    }
                }
            }";

        public const string GetBookById = @"
            query ($id: Int!) {
                bookQuery {  
                    bookById(id: $id) {
                        id,
                        title,
                        author {
                            id,
                            name,
                            bio
                        }
                    }
                }
            }";
    }
}
