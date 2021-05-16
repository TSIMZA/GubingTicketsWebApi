using GubingTickets.Utilities.Barcode;
using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Utilities.Extensions
{
    internal static class RendererColorExtensions
    {
        internal static double ConvertAlpha(this RendererColor color)
        {
            return Math.Round((double)color.A / (double)byte.MaxValue, 2);
        }

        internal static string AsRgb(this RendererColor color)
        {
            return ((int)color.R) + "," + (object)color.G + "," + (object)color.B;
        }

        internal static string AsRgba(this RendererColor color)
        {
            var num = color.ConvertAlpha();
            return ((int)color.R).ToString() + "," + (object)color.G + "," + (object)color.B + "," + (object)num;
        }

        internal static string AsBackgroundStyle(this RendererColor color)
        {
            var num = color.ConvertAlpha();
            return
                $"style=\"background-color:rgb({(object)color.R},{(object)color.G},{(object)color.B});background-color:rgba({(object)num});\"";
        }
    }
}
