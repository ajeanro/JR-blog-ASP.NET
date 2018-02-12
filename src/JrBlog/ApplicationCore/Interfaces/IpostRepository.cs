using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface IpostRepository
    {
        List<Post> PostList();
        Post GetPost(string permalink);
        void Add(Post newPost);
        void Edit(Post editedPost);
        void Delete(Post PostToDelete);
        Post GetById(int id);
    }
}
