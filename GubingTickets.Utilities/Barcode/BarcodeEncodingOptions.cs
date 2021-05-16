using GubingTickets.Models.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GubingTickets.Utilities.Barcode
{
    public class BarcodeEncodingOptions
    {
        public BarcodeType BarcodeType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Margin { get; set; }

        public ErrorCorrection ErrorCorrection { get; set; }

        public Color? BackgroundColor { get; set; }

        public Color? ForegroundColor { get; set; }

        public ImageFormat ImageFormat { get; set; }

        public string BaseImagesPath { get; set; }

        public string ImageFileName { get; set; }

        public bool AddImage { get; set; }

        public static BarcodeEncodingOptions DefaultBarcodeEncodingOptions()
        {
            return new BarcodeEncodingOptions
            {
                BarcodeType = BarcodeType.QRCode,
                Height = 350,
                Width = 350,
                Margin = 0,
                BackgroundColor = Color.White,
                ForegroundColor = Color.Orange
            };
        }
    }
}
