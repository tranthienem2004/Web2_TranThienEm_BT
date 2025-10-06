using Web2_TranThienEm_BT.Data;
using Web2_TranThienEm_BT.Repositories;
using Web2_TranThienEm_BT.Models;

namespace Web2_TranThienEm_BT.Services
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public Image Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            // Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            image.File.CopyTo(stream);

            // https://localhost:8080/images/image.jpg
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Add Image to the Images table
            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            return image;
        }

        public List<Image> GetAllInfoImages()
        {
            var allImages = _dbContext.Images.ToList();
            return allImages;
        }
        public (byte[], string, string) DownloadFile(int Id)
        {
            try
            {
                var fileById = _dbContext.Images.Where(x => x.Id == Id).FirstOrDefault();
                if (fileById == null)
                {
                    throw new FileNotFoundException("File not found.");
                }

                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{fileById.FileName}{fileById.FileExtension}");
                var stream = File.ReadAllBytes(path);
                var fileName = fileById.FileName + fileById.FileExtension;
                return (stream, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
