using System;
using System.Net;
using System.Threading.Tasks;
using Message.Core.Dto.Message;

namespace Message.Core.Services.File
{
    public interface IFileStorageHttpService
    {
        public Task<string> SendCreateRequest(FileSendingDto data);
        public Task<string> SendGetRequest(Guid fileId);
        public Task<HttpStatusCode?> SendDeleteRequest(Guid fileId);
    }
}