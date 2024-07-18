using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public string CategoryIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}