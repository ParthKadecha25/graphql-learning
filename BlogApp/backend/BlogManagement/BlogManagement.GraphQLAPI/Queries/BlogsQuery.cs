using BlogManagement.GraphQLAPI.Helpers;
using BlogManagement.GraphQLAPI.Queries.Types;
using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Queries
{
    public class BlogsQuery : ObjectGraphType
    {
        public BlogsQuery(ContextServiceLocator serviceLocator)
        {
            Name = "Blogs - Query";
            Description = "Get or Search Author/Category/Post details";

            #region "Authors"

            // Getting particular author by ID
            Field<AuthorType>(
                name: "author",
                description: "Get Author details by ID",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorID" }),
                resolve: context =>
                {
                    var auhorID = context.GetArgument<int>("authorID");
                    return serviceLocator.AuthorRepository.GetByID(auhorID);
                }
            );

            // Getting all author details, if name provided, then the authors matching the name will be provided
            Field<ListGraphType<AuthorType>>(
                name: "authors",
                description: "Get all authors' details that matches the name",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "authorName", DefaultValue = "", Description = "Author Name. Keep this empty to get all authors" }),
                resolve: context =>
                {
                    var authorName = context.GetArgument<string>("authorName");
                    if (string.IsNullOrEmpty(authorName))
                    {
                        return serviceLocator.AuthorRepository.GetAll();
                    }
                    else
                    {
                        return serviceLocator.AuthorRepository.SearchByName(authorName);
                    }
                    
                }
            );
          
            #endregion

            #region "Categories"

            // Getting category details by ID
            Field<CategoryType>(
                name: "category",
                description: "Get particular category details",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryID" }),
                resolve: context =>
                {
                    var categoryID = context.GetArgument<int>("categoryID");
                    return serviceLocator.CategoryRepository.GetByID(categoryID);
                }
            );
            
            // Getting all categories
            Field<ListGraphType<CategoryType>>(
                name: "categories",
                description: "Get all categories",
                resolve: context =>
                {
                    return serviceLocator.CategoryRepository.GetAll();
                }
            );

            #endregion

            // Getting particular post details
            Field<PostType>(
                name: "post",
                description: "Get post details by ID",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var postID = context.GetArgument<int>("id");
                    return serviceLocator.PostRepository.GetByID(postID);
                }
            );

            // Searching post by title for the particular author. If title not provided then all posts of the author will be returned
            Field<ListGraphType<PostType>>(
                name: "searchPosts",
                description: "Search post by title",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorID"},
                    new QueryArgument<StringGraphType> { Name = "title", DefaultValue = "", Description = "Keep it empty to get all posts of the author"}
                ),
                resolve: context =>
                {
                    var authorID = context.GetArgument<int>("authorID");
                    var postTitle = context.GetArgument<string>("title");
                    return serviceLocator.PostRepository.SearchByTitle(postTitle, authorID);
                }
            );
        }
    }
}
