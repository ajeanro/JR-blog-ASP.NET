using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Post
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string content { get; set; }
        public string permalink { get; set; }
        public int id { get; set; }

    }
}
