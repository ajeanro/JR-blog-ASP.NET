using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Infrastructure
{
    public class PostRepositoryInFs : IpostRepository
    {
        private static List<Post> _PostList;
        private static int _nextId = 1;

        private const string PATH = "Data";
        private const string FILENAME = "PostData.json";

        private readonly string _fileFullPath = Path.Combine(PATH, FILENAME);

        public PostRepositoryInFs()
        {
            if(_PostList == null)
            {
                _PostList = LoadFile();
                _nextId = _PostList.Select(p => p.id).Max()+1;
            }
        }

        public void Add(Post newPost)
        {
            newPost.id = _nextId++;
            _PostList.Add(newPost);

            SaveFile();
        }

        public void Delete(Post PostToDelete)
        {
            var origPost = GetById(PostToDelete.id);
            _PostList.Remove(origPost);

            SaveFile();
        }

        public void Edit(Post editedPost)
        {
            var origPost = GetById(editedPost.id);
            origPost.Title = editedPost.Title;
            origPost.Author = editedPost.Author;
            origPost.content = editedPost.content;
            origPost.permalink = editedPost.permalink;

            SaveFile();
        }

        public Post GetById(int id)
        {
            return _PostList.Find(p => p.id == id);
        }

        public Post GetPost(string permalink)
        {
            return _PostList.Find(p => p.permalink == permalink);
        }

        public List<Post> PostList()
        {
            return _PostList;
        }

        public List<Post> LoadFile()
        {
            try
            {
                string rawListStr = File.ReadAllText(_fileFullPath);
                List<Post> rawPostList = JsonConvert.DeserializeObject<List<Post>>(rawListStr);

                return rawPostList;
            }
            catch (Exception ex)
            {
                // TODO Log the Exception

                return new List<Post>();  
            }
        }

        public void SaveFile()
        {
            try
            {
                if(!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }
                string rawListString = JsonConvert.SerializeObject(_PostList);
                File.WriteAllText(_fileFullPath, rawListString);

            }
            catch (Exception ex)
            {
                // TODO Log the Exception
                
            }
        }

    }
}
