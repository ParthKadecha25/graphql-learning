using BlogManagement.Core.Models;
using BlogManagement.GraphQLAPI.Helpers;
using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Queries.Types
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Id).Description("Id of an author");
            Field(x => x.Name).Description("Name of an author");
            Field(x => x.Bio, nullable: true).Description("Just a little bio of an author");
            //Field(x => x.ProfileImage).Description("Byte array of the user's profile image");
            
            // For getting all categories of the author
            Field<ListGraphType<CategoryType>>("categories",
                resolve: context => contextServiceLocator.CategoryRepository.GetByAuthor(context.Source.Id), 
                description: "All Categories created by the user");


            // For getting all posts of the author
            Field<ListGraphType<PostType>>("posts",
                resolve: context => contextServiceLocator.PostRepository.GetByAuthor(context.Source.Id),
                description: "All posts published by the user");
        }
    }
}
