using BlogManagement.Core.Models;
using BlogManagement.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogManagement.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _db;

        public PostRepository(BlogContext blogContext)
        {
            _db = blogContext;
        }

        /// <summary>
        /// Getting all posts of the given author
        /// </summary>
        /// <param name="authorID"></param>
        /// <returns></returns>
        public List<Post> GetByAuthor(int authorID)
        {
            return _db.Posts.Include(x => x.Category).Where(x => x.Author.Id == authorID).ToList();
        }

        /// <summary>
        /// Getting all posts of the particular category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Post> GetByCategory(int categoryID)
        {
            return _db.Posts.Include(x => x.Author).Where(x => x.Category.Id == categoryID).ToList();
        }

        /// <summary>
        /// Getting all posts that matched the provided title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Post> SearchByTitle(string title, int authorID)
        {
            return _db.Posts.Include(x => x.Category).Include(x=> x.Author).Where(x => x.Author.Id == authorID && x.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        /// <summary>
        /// Getting all post that matches provided author name
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        public List<Post> SearchByAuthorName(string authorName)
        {
            return _db.Posts.Include(x => x.Category).Include(x => x.Author).Where(x => x.Author.Name.ToLower().Contains(authorName.ToLower())).ToList();
        }

        /// <summary>
        /// Getting the particular post
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Post GetByID(int ID)
        {
            return _db.Posts.Include(x => x.Category).Include(x => x.Author).Where(x => x.Id == ID).FirstOrDefault();
        }

        /// <summary>
        /// Adding new Post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="authorID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Post Add(Post post, int authorID, int categoryID)
        {
            post.Author = _db.Authors.Where(x => x.Id == authorID).FirstOrDefault();
            post.Category = _db.Categories.Where(x => x.Id == categoryID).FirstOrDefault();
            post.Date = DateTime.Now;
            _db.Posts.Add(post);
            _db.SaveChanges();
            return post;
        }

        /// <summary>
        /// Updating the post
        /// </summary>
        /// <param name="newPostDetails"></param>
        /// <param name="dbPost"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Post Update(Post newPostDetails, Post dbPost, int? categoryID)
        {
            if(newPostDetails != null)
            {
                dbPost.Title = newPostDetails.Title != null ? newPostDetails.Title : dbPost.Title;
                dbPost.Description = newPostDetails.Description != null ? newPostDetails.Description : dbPost.Description;
                dbPost.Url = newPostDetails.Url != null ? newPostDetails.Url : dbPost.Url;
            }
            if (categoryID != null && categoryID != 0)
            {
                dbPost.Category = _db.Categories.Where(x => x.Id == categoryID).FirstOrDefault();
            }
            _db.Posts.Update(dbPost);
            _db.SaveChanges();
            return dbPost;
        }

        /// <summary>
        /// Removing the particular post
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Delete(int ID)
        {
            Post post = _db.Posts.FirstOrDefault(x => x.Id == ID);
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return true;
        }
    }
}
