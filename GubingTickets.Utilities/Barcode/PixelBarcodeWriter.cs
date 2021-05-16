using ZXing;

namespace GubingTickets.Utilities.Barcode
{
    public class PixelBarcodeWriter: BarcodeWriter<PixelBarcodeImage>
    {
        public PixelBarcodeWriter()
        {
            Renderer = new PixelBarcodeRenderer();
        }
    }
}