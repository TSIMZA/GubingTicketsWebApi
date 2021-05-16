using ZXing;

namespace GubingTickets.Utilities.Barcode
{
    public class SvgBarcodeWriter: BarcodeWriter<SvgBarcodeImage>
    {
        public SvgBarcodeWriter()
        {
            Renderer = new SvgBarcodeRenderer();
        }
    }
}