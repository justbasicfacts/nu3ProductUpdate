using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Data.Enums;

namespace nu3ProductUpdate.Data.Classes
{
    public class FileUploadResult
    {
        public CustomFileInfo FileInfo { get; set; }
        public bool IsSuccessful { get; set; }
        public FileType FileType { get; set; }
    }
}