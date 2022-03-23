using System.Threading.Tasks;
using Message.Core.Dto.Message;

namespace Message.Core.Services.File
{
    public interface IFileStorageHttpService
    {
        public Task<string> SendRequest(FileSendingDto data);
    }
}