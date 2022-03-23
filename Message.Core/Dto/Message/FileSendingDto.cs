using System;
using Message.Common.Enums;

namespace Message.Core.Dto.Message
{
    public class FileSendingDto
    {
        public Guid? SourceId { get; set; }
        public byte[] UploadedFile { get; set; }
        public string UploadedFileName { get; set; }
        public FileSource FileSource { get; set; } = FileSource.Message;
    }
}