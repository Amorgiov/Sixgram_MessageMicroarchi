﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Message.Core.Dto.Message;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Services.File
{
    public class FileStorageHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpContext _httpContext;
        private readonly Uri _fileStorageBaseAddress;

        public FileStorageHttpService
        (
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            BaseAddresses addresses
        )
        {
            _httpClientFactory = httpClientFactory;
            _httpContext = httpContextAccessor.HttpContext;
            _fileStorageBaseAddress = new Uri(addresses.FileStorage);
        }


        public async Task<string> SendRequest(FileSendingDto data)
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
    }
}