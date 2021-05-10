using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Models.Enums
{
    public enum ErrorCorrection: byte
    {
        /// <summary>
        /// -7% correction
        /// </summary>
        L=0,

        /// <summary>
        /// -15% correction
        /// </summary>
        M = 1,

        /// <summary>
        /// -25% correction
        /// </summary>
        Q = 2,

        /// <summary>
        /// -30% correction
        /// </summary>
        H = 3
    }
}
