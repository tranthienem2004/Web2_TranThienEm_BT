using Web2_TranThienEm_BT.Models;

namespace Web2_TranThienEm_BT.Repositories
{
    public interface IImageRepository
    {
        Image Upload(Image image);
        List<Image> GetAllInfoImages();

        (byte[], string, string) DownloadFile(int id);
    }
}