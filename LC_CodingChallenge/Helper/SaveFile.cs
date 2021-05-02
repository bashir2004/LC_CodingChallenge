using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC_CodingChallenge.Helper
{
    public static class SaveFile
    {
        public static string Save(IFormFile file, IWebHostEnvironment hostEnvironment)
        {
            string filePath = string.Empty;
            string uploads = Path.Combine(hostEnvironment.ContentRootPath, "Uploads", DateTime.Now.Ticks.ToString());
            if (file.Length > 0)
            {
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);
                filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
            }
            //string path = Current.Server.MapPath(string.Format("{0}{1}/", "~/Uploads/", DateTime.Now.Ticks));
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //filePath = string.Format("{0}{1}", path, Path.GetFileName(postedFile.FileName));
            //postedFile.SaveAs(filePath);
            return filePath;
            //return null;
        }
    }
}
