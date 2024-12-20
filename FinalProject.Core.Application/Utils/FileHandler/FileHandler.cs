﻿
using FinalProject.Core.Application.Interfaces.Utils;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Core.Application.Utils.FileHandler
{
    public class FileHandler<TId> : IFileHandler<TId>
    {


        public async Task<string> UpdateFile(IFormFile file, string basePath, string imageUrl, TId id)
        {
            if (file is null)
            {
                return imageUrl;
            }

            if(file is null && imageUrl is not null)
            {
                return imageUrl;
            }

            // first part of the complete image url path is a base pasth somethin like "/Images/somthing" and the provided id
            basePath = $"{basePath}/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);

            using (FileStream stream = new(fileNameWithPath, FileMode.Create))
            {
              await file.CopyToAsync(stream);
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

        public async Task<string> UploadFile(IFormFile file, string basePath, TId id)
        {
            basePath = $"{basePath}/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (FileStream stream = new(fileNameWithPath, FileMode.Create))
            {
              await file.CopyToAsync(stream);
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
