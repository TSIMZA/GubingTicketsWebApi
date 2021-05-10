using GubingTickets.Models.Event;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GubingTickets.Utilities.Barcode
{
    public static class TicketFileHelper
    {
        public static void PrintTickets(string fileName, byte[] gubingTicket, List<TicketDetail> ticketDetails, EventDetail eventDetail, Patron patron)
        {
            PdfWriter writer = new PdfWriter($@"C:\Users\tsimy\Desktop\{fileName}.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            int ticketCount = 0;
            foreach (TicketDetail ticketDetail in ticketDetails)
            {

                Table table = new Table(new float[] { 80, 80, 320, 150 }, false);
                table.SetBorder(Border.NO_BORDER);

                Cell cellGubingTicket = new Cell(6, 1)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetBackgroundColor(ColorConstants.BLACK)
                .SetBorder(new SolidBorder(1))
                .Add(new Image(ImageDataFactory.Create(gubingTicket)));


                Cell cellBarcode = new Cell(6, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(1))
                .SetBorderRight(new SolidBorder(1))
                .SetBorderBottom(new SolidBorder(1))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(new Image(ImageDataFactory.Create(ticketDetail.Barcode)));



                Cell cellEventName = new Cell(1, 2)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(14)
                .SetBold()
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(1))
                .Add(new Paragraph(eventDetail.EventName).SetFixedLeading(35));


                Cell cellLocationLabel = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10)
                .SetBold()
                .SetFontColor(ColorConstants.GRAY)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Venue: ").SetFixedLeading(20));


                Cell cellEventocation = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(11)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph(eventDetail.EventLocation).SetFixedLeading(20));


                Cell cellDateLabel = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10)
                .SetBold()
                .SetFontColor(ColorConstants.GRAY)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Date: ").SetFixedLeading(20));

                Cell cellDate = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"{eventDetail.EventStartDateTime.ToString("ddd, dd MMM yyy, HH:mm")}").SetFixedLeading(20));
                // - {(eventDetail.EventEndDateTime.HasValue ? eventDetail.EventEndDateTime.Value.ToString() : "End")}

                Cell cellPatronLabel = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10)
                .SetBold()
                .SetFontColor(ColorConstants.GRAY)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Patron: ").SetFixedLeading(20));

                Cell cellPatron = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"{patron.FirstName} {patron.LastName}").SetFixedLeading(20));


                Cell cellPriceLabel = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10)
                .SetBold()
                .SetFontColor(ColorConstants.GRAY)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Price: ").SetFixedLeading(20));

                Cell cellPrice = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"R{ticketDetail.TicketPrice}").SetFixedLeading(20));


                Cell cellTicketLevelLabel = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10)
                .SetBold()
                .SetFontColor(ColorConstants.GRAY)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .Add(new Paragraph("Type: ").SetFixedLeading(20));

                Cell cellTicketLevel = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(11)
                .SetBold()
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .Add(new Paragraph(ticketDetail.TicketLevel).SetFixedLeading(20));

                table.AddCell(cellGubingTicket);
                table.AddCell(cellEventName);
                table.AddCell(cellBarcode);

                table.AddCell(cellLocationLabel);
                table.AddCell(cellEventocation);

                table.AddCell(cellDateLabel);
                table.AddCell(cellDate);

                table.AddCell(cellPatronLabel);
                table.AddCell(cellPatron);

                table.AddCell(cellPriceLabel);
                table.AddCell(cellPrice);

                table.AddCell(cellTicketLevelLabel);
                table.AddCell(cellTicketLevel);


                document.Add(table);

                ticketCount++;

                if(ticketCount < 4)
                {
                    document.Add(new Cell().Add(new Paragraph("\r\n")));
                }
                else
                {
                    ticketCount = 0;
                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }
            }
            document.Close();
        }
    }
}
