using System.IO;

namespace HerbTrans.Infrastructure.Files
{
    public interface IFileHelper
    {
        bool DirectoryExist(string path);
        string[] GetDirectoryAllFiles(string path);
        void MoveFile(string original, string destination);
        void WriteTextToFile(string path, string text);
    }
    public class FileHelper : IFileHelper
    {
        public bool DirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetDirectoryAllFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public void MoveFile(string original, string destination)
        {
            File.Move(original, destination);
        }

        public void WriteTextToFile(string path, string text)
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(text);
            }

        }
    }
}
