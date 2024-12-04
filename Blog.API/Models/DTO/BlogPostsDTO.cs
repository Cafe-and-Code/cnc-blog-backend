using System;

namespace Blog.API.Models.DTO;

public class BlogPostsDTO
{
    public BlogPostsDTO(int totalPosts, List<PostDTO> posts){
        TotalPosts = totalPosts;
        Posts = posts;
    }
    public int TotalPosts { get; set; }
    public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
}
