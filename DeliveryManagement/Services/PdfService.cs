using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using DeliveryManagement.Models;
using System.IO;

namespace DeliveryManagement.Services
{
    public class PdfService
    {
        public byte[] GenerateReceiptPdf(ReceiptViewModel model)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header().Text(text =>
                    {
                        text.Span(model.CompanyName).Bold().FontSize(20);
                        text.Line(model.PrintDate.ToString("dd/MM/yyyy")).FontSize(10).FontColor(Colors.Grey.Darken2);
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Receipt Number: {model.ReceiptNumber}").Bold();
                        col.Item().Text($"Customer: {model.CustomerName}");
                        col.Item().Text($"Address: {model.Address}");
                        col.Item().Text($"Product: {model.Product}");
                        col.Item().Text($"Delivery Date: {model.DeliveryDate:dd/MM/yyyy}");
                        col.Item().Text($"Delivered By: {model.DelivererName} ({model.DelivererPhone})");
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Thank you for your business!").Italic().FontSize(12);
                    });
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}
