using Blog.API.Models.Domain;

namespace Blog.API.Repositories.IRepository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
