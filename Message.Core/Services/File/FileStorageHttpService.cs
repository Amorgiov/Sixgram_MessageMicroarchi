using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Message.Core.Dto.Message;
using Message.Core.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Services.File
{
    public class FileStorageHttpService : IFileStorageHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpContext _httpContext;
        private readonly Uri _fileStorageBaseAddress;

        public FileStorageHttpService
        (
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            BaseAdresses addresses
        )
        {
            _httpClientFactory = httpClientFactory;
            _httpContext = httpContextAccessor.HttpContext;
            _fileStorageBaseAddress = new Uri(addresses.FileStorage);
        }


        public async Task<string> SendCreateRequest(FileSendingDto data)
        {
            using  var client = _httpClientFactory.CreateClient("FileStorage");

            var bytes = new ByteArrayContent(data.UploadedFile);
            var postId = new StringContent(data.SourceId.ToString());
            var fileSource = new StringContent(data.FileSource.ToString());

            var multiContent = new MultipartFormDataContent();

            multiContent.Add(bytes, "UploadedFile", data.UploadedFileName);
            multiContent.Add(postId, "SourceId");
            multiContent.Add(fileSource, "FileSource");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _httpContext.GetTokenAsync("access_token"));

            try
            {
                var responseMessage = await client.PostAsync("uploadfile", multiContent);
                var result = await responseMessage.Content.ReadAsStringAsync();
                return result;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<string> SendGetRequest(Guid fileId)
        {
            using var client = _httpClientFactory.CreateClient("FileStorage");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _httpContext.GetTokenAsync("access_token"));
            
            try
            {
                var responseMessage = await client.GetAsync($"uploadfile/{fileId}");
                var result = await responseMessage.Content.ReadAsStringAsync();
                return result;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
        
        public async Task<HttpStatusCode?> SendDeleteRequest(Guid fileId)
        {
            using var client = _httpClientFactory.CreateClient("FileStorage");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _httpContext.GetTokenAsync("access_token"));

            try
            {
                var responseMessage = await client.DeleteAsync($"{fileId}");
                var result = responseMessage.StatusCode;
                return result;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}