using System;
using System.Collections.Generic;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace GubingTickets.Models.Barcode
{
    public class PixelBarcodeRenderer : IBarcodeRenderer<PixelBarcodeImage>
    {
        public RendererColor Background { get; }
        public RendererColor Foreground { get; }

        public PixelBarcodeRenderer()
        {
            Background = RendererColor.White;
            Foreground = RendererColor.Black;
        }

        public PixelBarcodeImage Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, null);
        }

        public PixelBarcodeImage Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            int num1 = (options != null && options.PureBarcode || string.IsNullOrEmpty(content) ? 0 : (format == BarcodeFormat.CODE_39 || format == BarcodeFormat.CODE_128 || (format == BarcodeFormat.EAN_13 || format == BarcodeFormat.EAN_8) || (format == BarcodeFormat.CODABAR || format == BarcodeFormat.ITF || (format == BarcodeFormat.UPC_A || format == BarcodeFormat.MSI)) ? 1 : (format == BarcodeFormat.PLESSEY ? 1 : 0))) != 0 ? 16 : 0;
            int num2 = 1;
            if (options != null)
            {
                if (options.Width > width)
                {
                    width = options.Width;
                }

                if (options.Height > height)
                {
                    height = options.Height;
                }

                num2 = width / matrix.Width;
                if (num2 > height / matrix.Height)
                {
                    num2 = height / matrix.Height;
                }
            }
            if (num1 < height)
            {
                num1 = 0;
            }

            byte[] pixels = new byte[width * height * 4];
            int num3 = 0;
            for (int index1 = 0; index1 < matrix.Height - num1; ++index1)
            {
                for (int index2 = 0; index2 < num2; ++index2)
                {
                    for (int index3 = 0; index3 < matrix.Width; ++index3)
                    {
                        RendererColor color = matrix[index3, index1] ? Foreground : Background;
                        for (int index4 = 0; index4 < num2; ++index4)
                        {
                            byte[] numArray1 = pixels;
                            int index5 = num3;
                            int num4 = index5 + 1;
                            int b = color.B;
                            numArray1[index5] = (byte)b;
                            byte[] numArray2 = pixels;
                            int index6 = num4;
                            int num5 = index6 + 1;
                            int g = color.G;
                            numArray2[index6] = (byte)g;
                            byte[] numArray3 = pixels;
                            int index7 = num5;
                            int num6 = index7 + 1;
                            int r = color.R;
                            numArray3[index7] = (byte)r;
                            byte[] numArray4 = pixels;
                            int index8 = num6;
                            num3 = index8 + 1;
                            int a = color.A;
                            numArray4[index8] = (byte)a;
                        }
                    }
                    for (int index3 = num2 * matrix.Width; index3 < width; ++index3)
                    {
                        byte[] numArray1 = pixels;
                        int index4 = num3;
                        int num4 = index4 + 1;
                        int b = Background.B;
                        numArray1[index4] = (byte)b;
                        byte[] numArray2 = pixels;
                        int index5 = num4;
                        int num5 = index5 + 1;
                        int g = Background.G;
                        numArray2[index5] = (byte)g;
                        byte[] numArray3 = pixels;
                        int index6 = num5;
                        int num6 = index6 + 1;
                        int r = Background.R;
                        numArray3[index6] = (byte)r;
                        byte[] numArray4 = pixels;
                        int index7 = num6;
                        num3 = index7 + 1;
                        int a = Background.A;
                        numArray4[index7] = (byte)a;
                    }
                }
            }
            for (int index1 = matrix.Height * num2 - num1; index1 < height; ++index1)
            {
                for (int index2 = 0; index2 < width; ++index2)
                {
                    byte[] numArray1 = pixels;
                    int index3 = num3;
                    int num4 = index3 + 1;
                    int b = Background.B;
                    numArray1[index3] = (byte)b;
                    byte[] numArray2 = pixels;
                    int index4 = num4;
                    int num5 = index4 + 1;
                    int g = Background.G;
                    numArray2[index4] = (byte)g;
                    byte[] numArray3 = pixels;
                    int index5 = num5;
                    int num6 = index5 + 1;
                    int r = Background.R;
                    numArray3[index5] = (byte)r;
                    byte[] numArray4 = pixels;
                    int index6 = num6;
                    num3 = index6 + 1;
                    int a = Background.A;
                    numArray4[index6] = (byte)a;
                }
            }
            return new PixelBarcodeImage(width, height, pixels);
        }
    }
}
