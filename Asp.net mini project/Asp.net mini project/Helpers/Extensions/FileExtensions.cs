using Azure.Core;
namespace FiorelloBackendPB103.Helpers.Extensions
{
    public static class FileExtensions
    {
        public static bool CheckFilesSize(this IFormFile file,int size)
        {
            return file.Length / 1024 > 100;
        }

        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }

        public static void Delete(this string path)
            
        {
            if (File.Exists(path))
            {
               File.Delete(path);
            }
        }

    }
}
