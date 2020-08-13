using BlogManagement.Core.Models;
using BlogManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogContext _db;

        public CategoryRepository(BlogContext blogContext)
        {
            _db = blogContext;
        }

        /// <summary>
        /// Getting particular category by ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category GetByID(int categoryID)
        {
            return _db.Categories.Include(x => x.Author).Where(x => x.Id == categoryID).FirstOrDefault();
        }

        /// <summary>
        /// Getting all category list
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAll()
        {
            return _db.Categories.Include(x => x.Author).ToList();
        }

        /// <summary>
        /// Getting all categories for particular author
        /// </summary>
        /// <param name="authorID"></param>
        /// <returns></returns>
        public List<Category> GetByAuthor(int authorID)
        {
            return _db.Categories.Where(x => x.Author.Id == authorID).ToList();
        }

        /// <summary>
        /// Adding category for the particular author 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="authorID"></param>
        /// <returns></returns>
        public Category Add(Category category, int authorID)
        {
            category.Author = _db.Authors.Where(x => x.Id == authorID).FirstOrDefault();
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        /// <summary>
        /// Updating the category details
        /// </summary>
        /// <param name="newCategoryDetails"></param>
        /// <param name="dbCategory"></param>
        /// <param name="authorID"></param>
        /// <returns></returns>
        public Category Update(Category newCategoryDetails, Category dbCategory, int? authorID)
        {
            dbCategory.Name = newCategoryDetails.Name != null ? newCategoryDetails.Name : dbCategory.Name;
            dbCategory.Description = newCategoryDetails.Description != null ? newCategoryDetails.Description : dbCategory.Description;
            if(authorID != null && authorID != 0)
            {
                dbCategory.Author = _db.Authors.Where(x => x.Id == authorID).FirstOrDefault();
            }
            _db.Categories.Update(dbCategory);
            _db.SaveChanges();
            return dbCategory;
        }

        /// <summary>
        /// Deleting the category by ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            Category category = _db.Categories.FirstOrDefault(x => x.Id == categoryID);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return true;
        }
    }
}
