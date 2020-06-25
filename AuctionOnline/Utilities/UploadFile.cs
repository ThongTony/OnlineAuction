using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace AuctionOnline.Utilities
{
    public static class UploadFile
    {
        public async static Task<string> UploadAsync(IWebHostEnvironment webHostEnvironment, IFormFile fileType, string path)
        {
            string uniqueFileName = null;

            if (fileType != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, path);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileType.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Directory.CreateDirectory(uploadsFolder);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileType.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
