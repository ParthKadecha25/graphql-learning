using BlogManagement.Core.Models;
using BlogManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlogManagement.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BlogContext _db;

        public AuthorRepository(BlogContext blogContext)
        {
            _db = blogContext;
        }

        /// <summary>
        /// Get the author details associated with provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author GetByID(int id)
        {
            return _db.Authors.FirstOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// Searching the authors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Author> SearchByName(string name)
        {
            return _db.Authors.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        /// <summary>
        /// Getting all authors
        /// </summary>
        /// <returns></returns>
        public List<Author> GetAll()
        {
            return _db.Authors.ToList();
        }

        /// <summary>
        /// Add new author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Author Add(Author author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
            return author;
        }

        /// <summary>
        /// Update the author details
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Author Update(Author author)
        {
            _db.Authors.Update(author);
            _db.SaveChanges();
            return author;
        }

        /// <summary>
        /// Delete the author associated with provided ID
        /// </summary>
        /// <param name="authorID"></param>
        /// <returns></returns>
        public bool Delete(int authorID)
        {
            Author author = _db.Authors.FirstOrDefault(x => x.Id == authorID);
            _db.Authors.Remove(author);
            _db.SaveChanges();
            return true;
        }
    }
}
