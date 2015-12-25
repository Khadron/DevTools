using System.Collections.Specialized;
using System.IO;

namespace KongQiang.DevTools.Utils.Helper
{
    public class FileHelper
    {
        public static StringCollection GetAllFiles(string rootdir, string extension)
        {
            StringCollection result = new StringCollection();
            GetAllFiles(rootdir, extension, result);
            return result;
        }

        public static void GetAllFiles(string parentDir, string extension, StringCollection result)
        {
            string[] file = Directory.GetFiles(parentDir, extension);

            foreach (var s in file)
            {
                result.Add(s);
            }

            var subDirectory = Directory.GetDirectories(parentDir);
            foreach (var sd in subDirectory)
            {
                GetAllFiles(sd, extension, result);
            }
        }

        public static void CopyFile(string sourcePath, string destPath, string fileName)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }
            string filePath = Path.Combine(destPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.Copy(sourcePath, filePath);

        }
    }
}
