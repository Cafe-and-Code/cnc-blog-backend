using Blog.API.Models.Domain;

namespace Blog.API.Repositories.IRepository
{
    public interface IImageRepository: IBaseRepository<Image>
    {
        Task<Image> Upload(Image image);
    }
}
