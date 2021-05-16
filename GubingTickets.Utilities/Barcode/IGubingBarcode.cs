using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GubingTickets.Utilities.Barcode
{
    public interface IGubingBarcode
    {
        BarcodeImage Encode(string barcodeContent);
        BarcodeImage Encode(string barcodeContent, BarcodeEncodingOptions options);

        Task<BarcodeImage> EncodeAsync(string barcodeContent);
        Task<BarcodeImage> EncodeAsync(string barcodeContent, BarcodeEncodingOptions options);
    }
}
