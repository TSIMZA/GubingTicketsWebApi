using GubingTickets.Models.Enums;
using GubingTickets.Models.Event;
using GubingTickets.Utilities.Barcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GubingTickets.ConsoleService
{
    class Program
    {
        static Random random = new Random();

        static byte[] GetGubingTickets()
        {
            byte[] bytes = System.IO.File.ReadAllBytes($@"C:\Users\tsimy\Desktop\GB.png");

            return bytes;
        }

        
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void Main(string[] args)
        {
            EventDetail eventDetail = new EventDetail
            {
                EventName = "BALLERS All Black Premiere",
                EventId = 1,
                EventLocation = "Kae Kae Italy",
                EventStartDateTime = new DateTime(2021, 6, 5, 13, 0, 0),
                EventEndDateTime = new DateTime(2021, 6, 5, 23, 0, 0)
            };

            Patron patron = new Patron
            {
                FirstName = "Matsemela",
                LastName = "Moloi",
                EmailAddress = "",
                MobileNumber = ""
            };

            IGubingBarcode barcode = new GubingBarcode();
            List<TicketDetail> ticketDetails = new List<TicketDetail>();

            for (int i = 0; i < 8; i++)
            {

                BarcodeImage barcodeImage = barcode.Encode($"G{eventDetail.EventIdString} {RandomString(6)}",
                    new BarcodeEncodingOptions
                    {
                        BarcodeType = BarcodeType.QRCode,
                        ImageFormat = ImageFormat.Png,
                        Height = 125,
                        Width = 125,
                        Margin = 0,
                        ErrorCorrection = ErrorCorrection.H,
                        //BaseImagesPath = $@"C:\Users\tsimy\Desktop\",
                        //ImageFileName = "gubing76x76.png"
                    });

                byte[] barcodeBytes = Convert.FromBase64String(barcodeImage.ImageData);


                ticketDetails.Add(new TicketDetail
                {
                    TicketLevel = "VIP",
                    Barcode = barcodeBytes,
                    TicketPrice = 200, 
                    BarcodeContent = barcodeImage.BarcodeContent
                });

            }

            TicketFileHelper.GenerateTickets("fdfdfdf",$"TickeSample_{DateTime.Now.ToString("MMddyyyyhhmmsstt")}", GetGubingTickets(), ticketDetails, eventDetail, patron);

            Console.WriteLine("Done");

            Console.ReadLine();
        }
    }
}
