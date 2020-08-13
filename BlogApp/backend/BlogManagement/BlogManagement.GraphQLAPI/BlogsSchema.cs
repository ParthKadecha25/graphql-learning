using BlogManagement.GraphQLAPI.Mutations;
using BlogManagement.GraphQLAPI.Queries;
using GraphQL;
using GraphQL.Types;

namespace BlogManagement.GraphQLAPI
{
    public class BlogsSchema : Schema
    {
        // This will define the schema for GraphQL. 
        public BlogsSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BlogsQuery>();
            Mutation = resolver.Resolve<BlogsMutation>();
        } 
    }
}
