using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL.Helpers
{
    public interface IFileValidate
    {
        (bool Valid, string ErrorMessage) ValidateFile(IFormFile filetoValidate);
    }

    public class FileValidate : IFileValidate
    {
        public (bool Valid, string ErrorMessage) ValidateFile(IFormFile filetoValidate)
        {
            //            if filetoValidate == null then return (false, $"Provide valid Image file "
            //if (!CheckIfImageFile(filetoValidate) then
            //return (false, $"Provide valid image file )

            //return (true, $"sample error message)

            if (filetoValidate==null)
            {
                return (false, $"Provide valid Image file");

            }
            if (!CheckIfImageFile(filetoValidate))
            {
                return (false, $"Provide valid Image file");
            }

            return (true, $"sample error message");
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }

        public ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");
            var gif = Encoding.ASCII.GetBytes("GIF");
            var png = new byte[] { 137, 80,78, 71 };
            var tiff = new byte[] { 73, 73,42 };
            var tiff2 = new byte[] { 77, 77,42 };
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpeg2 = new byte[] { 255, 216, 255, 225 };

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;

        }

        public enum ImageFormat
        { 
            jpeg,
            png,
            unknown
        }
    }
}
