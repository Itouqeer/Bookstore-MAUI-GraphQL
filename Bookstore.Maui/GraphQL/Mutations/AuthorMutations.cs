namespace Bookstore.Maui.GraphQL.Mutations
{
    public class AuthorMutations
    {
        public const string AddAuthorMutation = @"
            mutation($name: String!, $bio: String!) {
                authorMutation {
                    addAuthor(input: { name: $name, bio: $bio }) {
                        id,
                        name,
                        bio
                    }
                }
            }";

        public const string UpdateAuthor = @"
            mutation ($id: Int!, $name: String!, $bio: String!) {
                authorMutation {
                    updateAuthor(id: $id, input: { name: $name, bio: $bio }) {
                        id,
                        name,
                        bio
                    }
                }
            }";
    }
}
