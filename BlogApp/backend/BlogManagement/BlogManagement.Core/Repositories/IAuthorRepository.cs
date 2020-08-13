using BlogManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core.Repositories
{
    public interface IAuthorRepository
    {
        Author GetByID(int id);

        List<Author> SearchByName(string name);

        List<Author> GetAll();
        Author Add(Author author);
        Author Update(Author author);
        bool Delete(int id);
    }
}
