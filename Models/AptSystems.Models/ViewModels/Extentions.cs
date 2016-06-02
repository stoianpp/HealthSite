using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptSystems.Models.ViewModels
{
    public static class Extentions
    {
        public static Image GetImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        public static Image ResizeAnImage(this byte[] byteArrayIn, Size size)
        {
            var imgToResize = GetImage(byteArrayIn);
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
