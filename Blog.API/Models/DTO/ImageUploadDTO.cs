using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class ImageUploadDTO
    {
        [Required]
        public required IFormFile File { get; set; }

        public string? FileDescription { get; set; }
    }
}
