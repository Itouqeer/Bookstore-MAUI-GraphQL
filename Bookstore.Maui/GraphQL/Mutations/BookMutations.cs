namespace Bookstore.Maui.GraphQL.Mutations
{
    public static class BookMutations
    {
        public const string AddBookMutation = @"
            mutation($title: String!, $authorId: Int!) {
                bookMutation {
                    addBook(input: { title: $title, authorId: $authorId }) {
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

        public const string UpdateBook = @"
            mutation ($id: Int!, $title: String!, $authorId: Int!) {
                bookMutation {
                    updateBook(id: $id, input: { title: $title, authorId: $authorId }) {
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

        public const string DeleteBook = @"
            mutation ($id: Int!) {
                bookMutation {
                    deleteBook(id: $id)
                }
            }";
    }
}
