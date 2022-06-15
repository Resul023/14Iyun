using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Helper
{
    public static class FileManager
    {
        public static string Save(string root,string folder,IFormFile file) 
        {

           
            var imageFile = Guid.NewGuid().ToString()+ (file.FileName.Length>64?file.FileName.Substring(file.FileName.Length - 64, 64):file.FileName);
            string path = Path.Combine(root,folder, imageFile);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return imageFile;
        }
        public static bool Delete(string root,string folder,string filename) 
        {
            var path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        public static void DeleteAll(string root, string folder, List<string> fileNames)
        {
            foreach (var file in fileNames)
            {
                var path = Path.Combine(root, folder, file);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
           
        }

    }
}
