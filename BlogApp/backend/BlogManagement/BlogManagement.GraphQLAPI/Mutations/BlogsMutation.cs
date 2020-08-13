using BlogManagement.Core.Models;
using BlogManagement.GraphQLAPI.Helpers;
using BlogManagement.GraphQLAPI.Mutations.Types;
using BlogManagement.GraphQLAPI.Queries.Types;
using GraphQL;
using GraphQL.Types;

namespace BlogManagement.GraphQLAPI.Mutations
{
    public class BlogsMutation : ObjectGraphType
    {
        public BlogsMutation(ContextServiceLocator contextServiceLocator)
        {
            Name = "Blogs - Mutation";
            Description = "Add, Update or Delete Author/Category/Post details";

            #region "Author"
            
            // For Adding new author
            Field<AuthorType>(  // AuthorType is Return Type
                "addAuthor", // Name of the function
                description: "Create new author with provided data",
                arguments: new QueryArguments( // Arguments and type
                    new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "author" }
                ),
                resolve: context => // Function Body
                {
                    Author author = context.GetArgument<Author>("author");
                    return contextServiceLocator.AuthorRepository.Add(author);
                }
            );

            Field<AuthorType>(
                "updateAuthor",
                description: "Update the author details",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorID" },
                    new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "author" }
                ),
                resolve: context =>
                {
                    Author author = context.GetArgument<Author>("author");
                    int authorID = context.GetArgument<int>("authorID");
                    author.Id = authorID;
                    return contextServiceLocator.AuthorRepository.Update(author);
                });

            Field<BooleanGraphType>(
                "deleteAuthor",
                description: "Delete the author and all associated data",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorID" }
                ),
                resolve: context =>
                {
                    int authorID = context.GetArgument<int>("authorID");
                    return contextServiceLocator.AuthorRepository.Delete(authorID);
                });

            #endregion

            #region "Category"

            // For Adding new Category
            Field<CategoryType>(
                "addCategory",
                description: "Create new category for the provided author",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorID" }
                ),
                resolve: context =>
                {
                    Category category = context.GetArgument<Category>("category");
                    int authorID = context.GetArgument<int>("authorID");
                    return contextServiceLocator.CategoryRepository.Add(category, authorID);
                }
            );

            // For updating the Category
            Field<CategoryType>(
                "updateCategory",
                description: "Update the category details",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryID" },
                    new QueryArgument<CategoryInputType> { Name = "category" },
                    new QueryArgument<IntGraphType> { Name = "authorID", DefaultValue = null }
                ),
                resolve: context =>
                {
                    Category category = context.GetArgument<Category>("category");
                    int categoryID = context.GetArgument<int>("categoryID");
                    int? authorID = context.GetArgument<int?>("authorID");
                    Category dbCategory = contextServiceLocator.CategoryRepository.GetByID(categoryID);
                    if (dbCategory == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find category with provided ID in DB."));
                        return null;
                    }
                    return contextServiceLocator.CategoryRepository.Update(category, dbCategory, authorID);
                }
            );

            // For deleting the particular category
            Field<BooleanGraphType>(
                "deleteCategory",
                description: "Delete the category",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryID" }
                ),
                resolve: context =>
                {
                    int categoryID = context.GetArgument<int>("categoryID");
                    return contextServiceLocator.CategoryRepository.Delete(categoryID);
                });

            #endregion

            #region "Posts"

            // For Adding new Post
            Field<PostType>(
                "addPost",
                description: "Create new Post",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PostInputType>> { Name = "post" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorID" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryID" }
                ),
                resolve: context =>
                {
                    Post post = context.GetArgument<Post>("post");
                    int authorID = context.GetArgument<int>("authorID");
                    int categoryID = context.GetArgument<int>("categoryID");
                    return contextServiceLocator.PostRepository.Add(post, authorID, categoryID);
                }
            );

            // For updating the Post
            Field<PostType>(
                "updatePost",
                description: "Update the post details",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "postID" },
                    new QueryArgument<PostInputType> { Name = "post", DefaultValue =  null },
                    new QueryArgument<IntGraphType> { Name = "categoryID", DefaultValue = null }
                ),
                resolve: context =>
                {
                    int postID = context.GetArgument<int>("postID");
                    Post post = context.GetArgument<Post>("post");
                    int? categoryID = context.GetArgument<int>("categoryID");
                    Post dbPost = contextServiceLocator.PostRepository.GetByID(postID);
                    if (dbPost == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find post with provided ID in DB."));
                        return null;
                    }
                    return contextServiceLocator.PostRepository.Update(post, dbPost, categoryID);
                }
            );

            // For deleting the particular post
            Field<BooleanGraphType>(
                "deletePost",
                description: "Delete the post",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "postID" }
                ),
                resolve: context =>
                {
                    int postID = context.GetArgument<int>("postID");
                    return contextServiceLocator.PostRepository.Delete(postID);
                });
            
            #endregion
        }

    }
}
