using BlogManagement.Core.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Repositories;
using BlogManagement.GraphQLAPI.Helpers;
using BlogManagement.GraphQLAPI.Mutations;
using BlogManagement.GraphQLAPI.Mutations.Types;
using BlogManagement.GraphQLAPI.Queries;
using BlogManagement.GraphQLAPI.Queries.Types;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.GraphQLAPI
{
    public class Startup
    {
        public const string GraphQlPath = "/graphql";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpContextAccessor();

            services.AddSingleton<ContextServiceLocator>();
            services.AddDbContext<BlogContext>();

            // Mapping the Repository implementation and interface
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            // Adding Graph Queries and Types
            services.AddSingleton<BlogsQuery>();
            services.AddSingleton<AuthorType>();
            services.AddSingleton<PostType>();
            services.AddSingleton<CategoryType>();

            // Adding Graph Mutation file and Input Types
            services.AddSingleton<BlogsMutation>();
            services.AddSingleton<AuthorInputType>();
            services.AddSingleton<CategoryInputType>();
            services.AddSingleton<PostInputType>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new BlogsSchema(new FuncDependencyResolver(type => sp.GetService(type)))); // For GraphQL schema

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseGraphiQl(GraphQlPath); // For GraphiQL tool
        }
    }
}
