﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Services.File
{
    public interface IFileStorageService
    {
        Task<Guid?> Send(IFormFile file, Guid sourceId);
    }
}