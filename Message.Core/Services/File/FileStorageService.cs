using System;
using System.IO;
using System.Threading.Tasks;
using Message.Common.Enums;
using Message.Core.Dto.Message;
using Message.Core.Services.Message;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Message.Core.Services.File
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IFileStorageHttpService _fileStorageHttp;

        public FileStorageService(IFileStorageHttpService fileStorageHttp)
        {
            _fileStorageHttp = fileStorageHttp;
        }
        
        public async Task<Guid?> Send(IFormFile file, Guid postId)
        {
            var data = await CreateContent(file);

            var fileSendingDto = new FileSendingDto()
            {
                SourceId = postId,
                UploadedFile = data,
                UploadedFileName = file.FileName,
                FileSource = FileSource.Message
            };

            var content = await _fileStorageHttp.SendRequest(fileSendingDto);

            var json = JObject.Parse(content);

            var result = new Guid(json["id"].ToString());

            return result;
        }
        
        private static async Task<byte[]> CreateContent(IFormFile file)
        {
            using var binaryReader = new BinaryReader(file.OpenReadStream());
            var data = binaryReader.ReadBytes((int) file.OpenReadStream().Length);

            return data;
        }
    }
}