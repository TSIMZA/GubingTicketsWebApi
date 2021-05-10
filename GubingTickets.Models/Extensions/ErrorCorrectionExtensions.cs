using GubingTickets.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing.QrCode.Internal;

namespace GubingTickets.Models.Extensions
{
    public static class ErrorCorrectionExtensions
    {
        public static ErrorCorrectionLevel GetCorrectionLevel(this ErrorCorrection errorCorrection)
        {
            switch (errorCorrection)
            {
                case ErrorCorrection.L: return ErrorCorrectionLevel.L;
                case ErrorCorrection.M: return ErrorCorrectionLevel.M;
                case ErrorCorrection.Q: return ErrorCorrectionLevel.Q;
                case ErrorCorrection.H: return ErrorCorrectionLevel.H;
                default: return ErrorCorrectionLevel.L;
            }
        }
    }
}
