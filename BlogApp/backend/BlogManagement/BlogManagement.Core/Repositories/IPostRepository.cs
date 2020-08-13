using BlogManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetByAuthor(int authorID);

        List<Post> GetByCategory(int categoryID);

        List<Post> SearchByTitle(string title, int authorID);

        List<Post> SearchByAuthorName(string authorName);

        Post GetByID(int ID);

        Post Add(Post post, int authorID, int categoryID);

        Post Update(Post newPostDetails, Post dbPost, int? categoryID);

        bool Delete(int ID);
    }
}
