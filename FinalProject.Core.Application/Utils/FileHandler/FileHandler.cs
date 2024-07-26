
using FinalProject.Core.Application.Interfaces.Utils;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Core.Application.Utils.FileHandler
{
    public class FileHandler<TId> : IFileHandler<TId>
    {


        public string UpdateFile(IFormFile file, string basePath, string imageUrl, TId id)
        {
            if(file is null)
            {
                return imageUrl;
            }
            basePath = $"{basePath}/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.Name);
            string fileName = guid + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);

            using (FileStream stream = new(fileNameWithPath, FileMode.Create))
            {
                stream.CopyTo(stream);
            }
            string[] oldImageUrl = imageUrl.Split('/');
            string oldImageUrlName = oldImageUrl[^1];
            string completeOldPath = Path.Combine(path, oldImageUrlName);

            if (System.IO.File.Exists(completeOldPath))
            {
                System.IO.File.Delete(completeOldPath);
            }

            return $"{basePath}/{fileName}";
        }

        public string UploadFile(IFormFile file, string basePath, TId id)
        {
            basePath = $"{basePath}/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.Name);
            string fileName = guid + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (FileStream stream = new(fileNameWithPath, FileMode.Create))
            {
                stream.CopyTo(stream);
            }
            return $"{basePath}/{fileName}";

        }
        public void DeleteFile(string basePath, TId id)
        {
            basePath = $"{basePath}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new(path);
                foreach(FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }
            }
        }
    }
}
