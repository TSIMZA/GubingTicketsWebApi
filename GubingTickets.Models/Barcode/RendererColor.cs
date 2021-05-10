using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GubingTickets.Models.Barcode
{
    public class RendererColor
    {
        public static RendererColor Green = new RendererColor(Color.Green.ToArgb());
        public static RendererColor White = new RendererColor(Color.White.ToArgb());
        public static RendererColor Black = new RendererColor(Color.Black.ToArgb());
        public static RendererColor Orange = new RendererColor(Color.Orange.ToArgb());
        public static RendererColor DarkOrange = new RendererColor(Color.DarkOrange.ToArgb());

        /// <summary>
        /// The value of the alpha channel
        /// </summary>
        public byte A { get; }
        /// <summary>
        /// The value of the red channel
        /// </summary>
        public byte R { get; }
        /// <summary>
        /// The value of the green channel
        /// </summary>
        public byte G { get; }
        /// <summary>
        /// The value of the blue channel
        /// </summary>
        public byte B { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RendererColor" /> struct.
        /// </summary>
        /// <param name="color"></param>
        public RendererColor(int color)
        {
            A = (byte)((color & 4278190080L) >> 24);
            R = (byte)((color & 16711680) >> 16);
            G = (byte)((color & 65280) >> 8);
            B = (byte)(color & (int)byte.MaxValue);
        }
    }
}
