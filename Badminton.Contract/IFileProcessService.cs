using Badminton.Model.Common;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Badminton.Contract
{
    public interface IFileProcessService
    {
        public void SetSubFolder(params string[] subFolder);
        public void SetPermittedType(params string[] permittedType);
        public string SaveFile(IFormFile file);

        public FileResult? GetFile(string filePath);

        public string GetFilePath(string fileName, bool isAbsolute);
        public string GetFileFolder(bool isAbsolute);
        public Task UploadChunks(LargeFileDataChunk chunk);
        public void UploadComplete(string fileName);

        public string MoveLargeFile(string fileName, string fileType);

        public string GetFileJSON(string filePath);
    }
}
