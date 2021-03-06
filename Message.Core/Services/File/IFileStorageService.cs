using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Services.File
{
    public interface IFileStorageService
    {
        Task<Guid?> CreateFile(IFormFile file, Guid sourceId);
        Task<string> GetFile(Guid fileId);
        Task<bool?> DeleteFile(Guid fileId);
    }
}