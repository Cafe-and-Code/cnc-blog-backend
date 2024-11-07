using Microsoft.AspNetCore.Mvc;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.IRepository;
using Blog.API.Commons;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO request)
        {
            var apiError = new ApiErrorResponse();
            ValidateFileUpload(request, apiError);

            if (!apiError.Errors.Any())
            {
                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FilePath = string.Empty
                };

                // User repository to upload image
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(apiError);
        }


        private void ValidateFileUpload(ImageUploadDTO request, ApiErrorResponse apiError)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", "webp" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()))
            {
                apiError.Errors.Add("Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                apiError.Errors.Add("File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
