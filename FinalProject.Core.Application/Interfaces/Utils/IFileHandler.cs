﻿
using Microsoft.AspNetCore.Http;

namespace FinalProject.Core.Application.Interfaces.Utils
{
    public interface IFileHandler<TId>
    {
        public string UploadFile(IFormFile file, string basePath, TId id);
        public string UpdateFile(IFormFile file, string basepath, string imgeUrl, TId id);
        public void DeleteFile(string basePath, TId id);
    }
}