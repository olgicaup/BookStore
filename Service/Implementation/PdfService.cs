using Domain.Domain_Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Service.Interface;
using System.Collections.Generic;
using System.IO;

public class PdfService : IPdfService
{
    public byte[] GenerateOrdersPdf(List<Order> orders)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.Header()
                    .AlignCenter()
                    .Text("Order Report")
                    .FontSize(18)
                    .SemiBold();

                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.RelativeColumn(); 
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Order ID").Bold();
                            header.Cell().Text("Total Price").Bold();
                        });

                        foreach (var order in orders)
                        {
                            table.Cell().Text(order.Id.ToString());
                            table.Cell().Text(order.BooksInOrder.Sum(b => b.Book.Price).ToString("C"));
                        }
                    });

                page.Footer()
                    .AlignRight()
                    .Text($"Generated on {System.DateTime.Now.ToShortDateString()}");
            });
        });

        return document.GeneratePdf();
    }
}
