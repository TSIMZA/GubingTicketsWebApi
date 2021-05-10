using GubingTickets.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GubingTickets.Models.Barcode
{
    public class SvgBarcodeImage
    {
        private readonly StringBuilder _barcodeBuilder = new StringBuilder();

        public int Width { get; set; }
        public int Height { get; set; }
        public string Image => _barcodeBuilder.ToString();

        public SvgBarcodeImage(int width, int height)
        {
            Width = width;
            Height = height;
        }

        internal void AddHeader()
        {
            _barcodeBuilder.Append("<?xml version=\"1.0\" standalone=\"no\"?>");
            _barcodeBuilder.Append("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
        }

        internal void AddTag(int displaySizeX, int displaySizeY, int viewBoxSizeX, int viewBoxSizeY, RendererColor background, RendererColor foreground)
        {
            if (displaySizeX <= 0 || displaySizeY <= 0)
            {
                _barcodeBuilder.Append($"<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" shape-rendering=\"crispEdges\" viewBox=\"0 0 {viewBoxSizeX as object} {viewBoxSizeY as object}\" viewport-fill=\"rgb({(object)background.AsRgb()})\" viewport-fill-opacity=\"{(object)background.ConvertAlpha()}\" fill=\"rgb({(object)foreground.AsRgb()})\" fill-opacity=\"{(object)foreground.ConvertAlpha()}\" {(object)background.AsBackgroundStyle()}>");
            }
            else
            {
                _barcodeBuilder.Append($"<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" shape-rendering=\"crispEdges\" viewBox=\"0 0 {viewBoxSizeX as object} {viewBoxSizeY as object}\" viewport-fill=\"rgb({(object)background.AsRgb()})\" viewport-fill-opacity=\"{(object)background.AsRgb()}\" fill=\"rgb({(object)foreground.AsRgb()})\" fill-opacity=\"{(object)foreground.ConvertAlpha()}\" {(object)background.AsBackgroundStyle()} width=\"{(object)displaySizeX}\" height=\"{(object)displaySizeY}\">");
            }
        }

        internal void AddText(string text, string fontName, int fontSize)
        {
            _barcodeBuilder.AppendFormat(CultureInfo.InvariantCulture, "<text x=\"50%\" y=\"98%\" style=\"font-family: {0}; font-size: {1}px\" text-anchor=\"middle\">{2}</text>", fontName, fontSize, text);
        }

        internal void AddRec(int posX, int posY, int width, int height)
        {
            _barcodeBuilder.AppendFormat(CultureInfo.InvariantCulture, "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{3}\"/>", (object)posX, (object)posY, (object)width, (object)height);
        }


        internal void AddEnd()
        {
            _barcodeBuilder.Append("</svg>");
        }
    }
}
