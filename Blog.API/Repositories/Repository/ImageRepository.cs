using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
/*using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;*/

namespace Blog.API.Repositories.Repository
{
    public class ImageRepository : BaseRepository<Models.Domain.Image>, IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BlogDbContext _dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            BlogDbContext dbContext): base(dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Models.Domain.Image> Upload(Models.Domain.Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Convert to WebP
            using (var imageSharpImage = SixLabors.ImageSharp.Image.Load(image.File.OpenReadStream()))
            {
                // Save file to local path
                await imageSharpImage.SaveAsync(localFilePath, new WebpEncoder());
            }

            // Ensure HttpContext is not null
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }

            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add Image to the Images table
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }
    }
}
