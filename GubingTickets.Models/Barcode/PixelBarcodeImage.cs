using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Models.Barcode
{
    public class PixelBarcodeImage
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public byte[] Pixels { get; set; }

        public PixelBarcodeImage(int width, int height, byte[] pixels)
        {
            Width = width;
            Height = height;
            Pixels = pixels;
        }
    }
}
