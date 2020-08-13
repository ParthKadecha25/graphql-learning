using BlogManagement.Core.Models;
using System.Collections.Generic;

namespace BlogManagement.Core.Repositories
{
    public interface ICategoryRepository
    {
        Category GetByID(int categoryID);

        List<Category> GetByAuthor(int authorID);   

        List<Category> GetAll();

        Category Add(Category category, int authorID);

        Category Update(Category category, Category dbCategory, int? authorID);

        bool Delete(int categoryID);
    }
}
