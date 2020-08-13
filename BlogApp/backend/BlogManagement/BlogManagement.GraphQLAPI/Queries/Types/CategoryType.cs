using BlogManagement.Core.Models;
using BlogManagement.GraphQLAPI.Helpers;
using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Queries.Types
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Id).Description("Category ID");
            Field(x => x.Name).Description("Category Name");
            Field(x => x.Description).Description("Category Description");

            Field<AuthorType>("author",
                resolve: context => contextServiceLocator.AuthorRepository.GetByID(context.Source.Author.Id),
                description: "Author details");

            Field<ListGraphType<PostType>>("posts",
                resolve: context => contextServiceLocator.PostRepository.GetByCategory(context.Source.Id),
                description: "All posts of this category");
        }
    }
}
