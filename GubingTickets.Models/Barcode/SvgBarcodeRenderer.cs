using GubingTickets.Models.Barcode;
using System;
using ZXing;
using ZXing.Common;
using ZXing.OneD;
using ZXing.Rendering;

namespace GubingTickets.Models.Barcode
{
    public class SvgBarcodeRenderer : IBarcodeRenderer<SvgBarcodeImage>
    {
        public const string DefaultFontName = "Ariel";
        public const int DefaultFontSize = 10;

        public string FontName { get; set; }
        public int FontSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public RendererColor Background { get; set; }
        public RendererColor Foreground { get; set; }

        public SvgBarcodeRenderer()
        {
            Background = RendererColor.White;
            Foreground = RendererColor.Black;
        }

        public SvgBarcodeRenderer(string fontName, int fontSize, int width, int height, RendererColor background, RendererColor foreground) : this()
        {
            FontName = fontName;
            FontSize = fontSize;
            Width = width;
            Height = height;

            if (background != null)
            {
                Background = background;
            }

            if (foreground != null)
            {
                Foreground = foreground;
            }
        }

        public SvgBarcodeImage Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, null);
        }

        public SvgBarcodeImage Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var barcodeImage = new SvgBarcodeImage(matrix.Width, matrix.Height);

            var width = matrix.Width;
            var height = matrix.Height;

            var num1 = options != null && options.PureBarcode || string.IsNullOrEmpty(content)
                ? 0
                : (format == BarcodeFormat.CODE_39 || format == BarcodeFormat.CODE_93 ||
                   (format == BarcodeFormat.CODE_128 || format == BarcodeFormat.EAN_13) ||
                   (format == BarcodeFormat.EAN_8 || format == BarcodeFormat.CODABAR ||
                    (format == BarcodeFormat.ITF || format == BarcodeFormat.UPC_A)) ||
                   (format == BarcodeFormat.UPC_E || format == BarcodeFormat.MSI)
                    ? 1
                    : (format == BarcodeFormat.PLESSEY ? 1 : 0));
            if (num1 != 0)
            {
                var num2 = FontSize < 1 ? DefaultFontSize : FontSize;
                height += num2 + 3;
            }

            barcodeImage.AddHeader();
            barcodeImage.AddTag(0, 0, width, height, Background, Foreground);

            AppendDarkCell(barcodeImage, matrix, 0, 0);
            if (num1 != 0)
            {
                var fontName = string.IsNullOrEmpty(FontName) ? DefaultFontName : FontName;
                var fontSize = FontSize < 1 ? DefaultFontSize : FontSize;
                content = this.ModifyContentDependingOnBarcodeFormat(format, content);
                barcodeImage.AddText(content, fontName, fontSize);
            }

            barcodeImage.AddEnd();

            return barcodeImage;
        }

        private static void AppendDarkCell(SvgBarcodeImage image, BitMatrix matrix, int offsetX, int offSetY)
        {
            if (matrix == null)
            {
                return;
            }

            var width = matrix.Width;
            var height = matrix.Height;
            var processed = new BitMatrix(width, height);
            var flag = false;
            var startPosX = 0;
            var startPosY = 0;
            for (var index = 0; index < width; ++index)
            {
                int endPosX;
                for (var endPosY = 0; endPosY < height; ++endPosY)
                {
                    if (processed[index, endPosY])
                    {
                        continue;
                    }

                    processed[index, endPosY] = true;
                    if (matrix[index, endPosY])
                    {
                        if (flag)
                        {
                            continue;
                        }

                        startPosX = index;
                        startPosY = endPosY;
                        flag = true;
                    }
                    else if (flag)
                    {
                        FindMaximumRectangle(matrix, processed, startPosX, startPosY, endPosY,
                            out endPosX);
                        image.AddRec(startPosX + offsetX, startPosY + offSetY, endPosX - startPosX + 1,
                            endPosY - startPosY);
                        flag = false;
                    }
                }

                if (!flag)
                {
                    continue;
                }

                FindMaximumRectangle(matrix, processed, startPosX, startPosY, height, out endPosX);
                image.AddRec(startPosX + offsetX, startPosY + offSetY, endPosX - startPosX + 1, height - startPosY);
                flag = false;
            }
        }

        private static void FindMaximumRectangle(BitMatrix matrix, BitMatrix processed, int startPosX, int startPosY, int endPosY, out int endPosX)
        {
            endPosX = startPosX;
            for (var index1 = startPosX + 1; index1 < matrix.Width; ++index1)
            {
                for (var index2 = startPosY; index2 < endPosY; ++index2)
                {
                    if (!matrix[index1, index2])
                    {
                        return;
                    }
                }

                endPosX = index1;
                for (var index2 = startPosY; index2 < endPosY; ++index2)
                {
                    processed[index1, index2] = true;
                }
            }
        }

        private string ModifyContentDependingOnBarcodeFormat(BarcodeFormat format, string content)
        {
            switch (format)
            {
                case BarcodeFormat.EAN_8:
                    if (content.Length < 8)
                    {
                        content = OneDimensionalCodeWriter.CalculateChecksumDigitModulo10(content);
                    }

                    content = content.Insert(4, "   ");
                    break;
                case BarcodeFormat.EAN_13:
                    if (content.Length < 13)
                    {
                        content = OneDimensionalCodeWriter.CalculateChecksumDigitModulo10(content);
                    }

                    content = content.Insert(7, "   ");
                    content = content.Insert(1, "   ");
                    break;
            }
            return content;
        }
    }
}
