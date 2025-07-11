﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Extensions
{
    public static class FileExtension
    {
       
        public static string Upload(this IFormFile file, string rootPath, string folderName)
        {
            string filename = file.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length - 64);
            }
            filename = Guid.NewGuid() + file.FileName;
            string path = Path.Combine(rootPath, folderName, filename);

            using (FileStream st = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(st);
            }
            return filename;
        }
        public static bool DeleteFile(string rootPath, string folderName, string filename)
        {
            string path = Path.Combine(rootPath, folderName, filename);
            if (!File.Exists(path))
            {
                return false;
            }
            File.Delete(path);
            return true;
        }
    }
}
