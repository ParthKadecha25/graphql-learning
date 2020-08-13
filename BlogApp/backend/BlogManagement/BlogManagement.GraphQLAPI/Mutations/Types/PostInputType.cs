using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Mutations.Types
{
    public class PostInputType : InputObjectGraphType
    {
        public PostInputType()
        {
            Name = "PostInput";
            Description = "Data required for creating/updating the post";
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("description");
            Field<StringGraphType>("url");
        }
    }
}
