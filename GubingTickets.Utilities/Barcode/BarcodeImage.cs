using GubingTickets.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Utilities.Barcode
{
    public class BarcodeImage
    {
        public BarcodeType BarcodeType { get; set; }

        public string ImageData{ get; set; }

        public string BarcodeContent { get; set; }

        public ImageFormat ImageFormat { get; set; }
    }
}
