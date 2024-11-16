using Microsoft.AspNetCore.Mvc;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.IRepository;
using Blog.API.Commons;
using Microsoft.AspNetCore.Authorization;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public ImagesController(IImageRepository imageRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _imageRepository = imageRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _imageRepository.GetAllAsync();
            return Ok(images);
        }

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
                    FileExtension = Constants.DefaultImageExtension,
                    FileSizeInBytes = request.File.Length,
                    FileName = Guid.NewGuid().ToString(),
                    FileDescription = request.FileDescription,
                    FilePath = string.Empty
                };

                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(apiError);
        }

        private void ValidateFileUpload(ImageUploadDTO request, ApiErrorResponse apiError)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()))
            {
                apiError.Errors.Add("Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                apiError.Errors.Add("File size more than 10MB, please upload a smaller size file.");
            }
        }

        /*[HttpDelete]
        [Route("Unused")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> DeleteUnusedImages()
        {
            var images = await _imageRepository.GetAllAsync();
            var isHaveUnsedImage = false;

            if (images == null)
            {
                return NotFound(
                    new ApiErrorResponse(StatusCodes.Status404NotFound, Constants.NotFound, Constants.NotFound)
                );
            }

            foreach (var image in images)
            {
                var isImageUsingInPost = await _postRepository.AnyAsync(post => post.Content.Contains(image.FilePath) || post.TitleImageUrl.Equals(image.FilePath));
                var isImageUsingInAvatar = await _userRepository.AnyAsync(user => user.AvatarImageUrl != null && user.AvatarImageUrl.Contains(image.FilePath));

                if (isImageUsingInPost || isImageUsingInAvatar)
                {
                    continue;
                }

                isHaveUnsedImage = true;
                await _imageRepository.DeleteAsync(image);
            }

            if (!isHaveUnsedImage)
            {
                return NotFound(
                    new ApiErrorResponse(StatusCodes.Status404NotFound, Constants.NotFound, Constants.NotFound)
                );
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var image = await _imageRepository.FindOneAsync(image => image.Id == id);
            if (image == null)
            {
                return NotFound(
                    new ApiErrorResponse(StatusCodes.Status404NotFound, Constants.NotFound, Constants.NotFound)
                );
            }

            var isImageUsingInPost = await _postRepository.AnyAsync(post => post.Content.Contains(image.FilePath) || post.TitleImageUrl.Equals(image.FilePath));
            var isImageUsingInAvatar = await _userRepository.AnyAsync(user => user.AvatarImageUrl != null && user.AvatarImageUrl.Contains(image.FilePath));

            if (isImageUsingInPost || isImageUsingInAvatar)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new ApiErrorResponse(StatusCodes.Status409Conflict, Constants.Conflict, Constants.ImageInUse)
                );
            }

            await _imageRepository.DeleteAsync(image);

            return Ok();
        }*/
    }
}
