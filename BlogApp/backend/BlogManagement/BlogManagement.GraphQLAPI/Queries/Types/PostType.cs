using BlogManagement.Core.Models;
using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Queries.Types
{
    public class PostType : ObjectGraphType<Post>
    {
        public PostType()
        {
            Field(x => x.Id).Description("Post ID");
            Field(x => x.Title).Description("Post Title");
            Field(x => x.Description, nullable: true).Description("Post Description or Details");
            Field<StringGraphType>("date", resolve: context => context.Source.Date.ToShortDateString(), description: "Date when the Post was published");
            Field(x => x.Url, nullable:true).Description("Post URL, if any");
            Field<AuthorType>("author", description: "Author Details");
            Field<CategoryType>("category", description: "Category Details");
        }
    }
}
