using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Mutations.Types
{
    public class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType()
        {
            Name = "CategoryInput";
            Description = "Data required for creating and updating the category";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<StringGraphType>("description");
        }
    }
}
