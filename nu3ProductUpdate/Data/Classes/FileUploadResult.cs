using LiteDB;
using nu3ProductUpdate.Data.Enums;

namespace nu3ProductUpdate.Data.Classes
{
    public class FileUploadResult
    {
        public LiteFileInfo<string> FileInfo { get; set; }
        public bool IsSuccessful { get; set; }
        public FileType FileType { get; set; }
    }
}