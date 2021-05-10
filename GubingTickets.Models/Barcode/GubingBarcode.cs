using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GubingTickets.Models.Barcode;
using GubingTickets.Models.Enums;
using GubingTickets.Models.Extensions;
using ZXing;
using ZXing.QrCode;
using ImageFormat = GubingTickets.Models.Enums.ImageFormat;

namespace GubingTickets.Utilities.Barcode
{
    public class GubingBarcode : IGubingBarcode
    {
        public BarcodeImage Encode(string barcodeContent)
        {
            return Encode(barcodeContent, null);
        }

        public BarcodeImage Encode(string barcodeContent, BarcodeEncodingOptions options)
        {
            if (string.IsNullOrWhiteSpace(barcodeContent))
            {
                throw new ArgumentException($"{nameof(barcodeContent)} cannot be null, empty or consist of only whitespace.", nameof(barcodeContent));
            }

            if (options == null)
            {
                options = BarcodeEncodingOptions.DefaultBarcodeEncodingOptions();
            }

            switch (options.BarcodeType)
            {
                case BarcodeType.QRCode:
                    return GenerateQrCode(barcodeContent, options);
                default:
                    throw new NotSupportedException($"{options.BarcodeType} is not supported.");
            }
        }

        public async Task<BarcodeImage> EncodeAsync(string barcodeContent)
        {
            return await Task.Run(() => Encode(barcodeContent));
        }

        public async Task<BarcodeImage> EncodeAsync(string barcodeContent, BarcodeEncodingOptions options)
        {
            return await Task.Run(() => Encode(barcodeContent, options));
        }

        private BarcodeImage GenerateQrCode(string barcodeContent, BarcodeEncodingOptions options)
        {
            switch (options.ImageFormat)
            {
                case ImageFormat.Jpeg:
                case ImageFormat.Png:

                    PixelBarcodeWriter pixelBarcodeWriter = new PixelBarcodeWriter()
                    {
                        Format = BarcodeFormat.QR_CODE,
                        Options = new QrCodeEncodingOptions()
                        {
                            Width = options.Width,
                            Height = options.Height,
                            Margin = options.Margin,
                            PureBarcode = false,
                            ErrorCorrection = options.ErrorCorrection.GetCorrectionLevel()
                        }
                    };

                    PixelBarcodeImage pixelImage = pixelBarcodeWriter.Write(barcodeContent);

                    using (Bitmap bitmap = new Bitmap(options.Width, options.Height, PixelFormat.Format32bppRgb))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelImage.Width, pixelImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                            try
                            {
                                Marshal.Copy(pixelImage.Pixels, 0, bitmapData.Scan0, pixelImage.Pixels.Length);
                            }
                            finally
                            {
                                bitmap.UnlockBits(bitmapData);
                            }

                            if(options.AddImage)
                            {
                                using (Bitmap logo = new Bitmap($"{Path.Combine(options.BaseImagesPath, options.ImageFileName)}"))
                                {
                                    using (Graphics graphics = Graphics.FromImage(bitmap))
                                    {
                                        graphics.DrawImage(logo, CalculatePoint(bitmap.Width, logo.Width, bitmap.Height, logo.Height));
                                    }
                                }
                            }


                            bitmap.Save(memoryStream,
                                options.ImageFormat == ImageFormat.Jpeg
                                    ? System.Drawing.Imaging.ImageFormat.Jpeg
                                    : System.Drawing.Imaging.ImageFormat.Png);

                            return new BarcodeImage
                            {
                                BarcodeType = options.BarcodeType,
                                ImageData = Convert.ToBase64String(memoryStream.ToArray()),
                                ImageFormat = options.ImageFormat,
                                BarcodeContent = barcodeContent
                            };
                        }
                    }


                case ImageFormat.Svg:
                    SvgBarcodeWriter svgBarcodeWriter = new SvgBarcodeWriter()
                    {
                        Format = BarcodeFormat.QR_CODE,
                        Options = new QrCodeEncodingOptions()
                        {
                            Width = options.Width,
                            Height = options.Height,
                            Margin = options.Margin,
                            ErrorCorrection = options.ErrorCorrection.GetCorrectionLevel(),
                            PureBarcode = false
                        }
                    };

                    SvgBarcodeImage svgImage = svgBarcodeWriter.Write(barcodeContent);

                    return new BarcodeImage()
                    {
                        BarcodeType = options.BarcodeType,
                        ImageData = svgImage.Image,
                        ImageFormat = options.ImageFormat,
                        BarcodeContent = barcodeContent
                    };
                default:
                    throw new NotSupportedException();
            }
        }

        private Point CalculatePoint(int qrCodeWidth, int logoWidth, int qrCodeHeight, int logoHeight)
        {
            return new Point((qrCodeWidth - logoWidth) / 2, (qrCodeHeight - logoHeight) / 2);
        }
    }
}
