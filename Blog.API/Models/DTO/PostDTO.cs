﻿using Blog.API.Models.Domain;

namespace Blog.API.Models.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string CategoryIds { get; set; }
        public Guid AuthorId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required Category Category { get; set; }
        public required User Author { get; set; }
    }
}
