using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Mutations.Types
{
    public class AuthorInputType : InputObjectGraphType
    {
        public AuthorInputType()
        {
            Name = "AuthorInput";
            Description = "Data required for creating and updating the author details";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<StringGraphType>("Bio");
        }
    }
}
