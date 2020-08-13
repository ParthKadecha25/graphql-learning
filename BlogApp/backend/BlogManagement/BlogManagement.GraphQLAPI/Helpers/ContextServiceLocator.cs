using BlogManagement.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.GraphQLAPI.Helpers
{
    public class ContextServiceLocator
    {        
        public IAuthorRepository AuthorRepository => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IAuthorRepository>();
        public ICategoryRepository CategoryRepository => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ICategoryRepository>();
        public IPostRepository PostRepository => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IPostRepository>();
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
