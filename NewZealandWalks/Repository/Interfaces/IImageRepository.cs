using NewZealandWalks.Models.Domain;

namespace NewZealandWalks.Repository.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(Image image);
    }
}
