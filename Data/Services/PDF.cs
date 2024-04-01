using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MudBlazor;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace DotNetCW.Data
{
    public class PDF : IDocument
    {

        public PdfDoc pdfDocObj;

        public PDF(PdfDoc model)
        {
            pdfDocObj = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {

            container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Header().Text($"Revenue Analysis And Most Purchased Items - ({pdfDocObj.PdfGeneratedDate})").FontSize(18).FontColor("#EE4420");
                page.Content().Element(ComposeContent);
            });
        }


        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Item().PaddingTop(5).Text("Sales Transactions Report").FontSize(15).Bold().FontColor("#2095EE");
                column.Item().PaddingTop(10).Element(ComposeOrdersTable);


                column.Item().PaddingTop(20).Text("Most Purchased Items").FontSize(15).Bold().FontColor("#2095EE"); ;
                column.Item().PaddingTop(15).Element(ComposeAddInsTable);
                column.Item().PaddingTop(20).Element(ComposeCoffeeTable);


            });
        }

        void ComposeOrdersTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(100);
                    columns.ConstantColumn(80);
                    columns.ConstantColumn(60);
                    columns.ConstantColumn(60);
                    columns.ConstantColumn(60);



                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Customer Name");
                    header.Cell().Element(CellStyle).Text("Phone Number");
                    header.Cell().Element(CellStyle).Text("Order Total");
                    header.Cell().Element(CellStyle).Text("Discount Amount");
                    header.Cell().Element(CellStyle).Text("Grand Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var order in pdfDocObj.Orders)
                {
                    table.Cell().Element(CellStyle).Text((pdfDocObj.Orders.IndexOf(order) + 1).ToString()).FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text(order.MemberName).FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text(order.MemberPhoneNum).FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.GrandTotal}").FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.Discount}").FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.GrandTotal - order.Discount}").FontColor("#EE2020");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeCoffeeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(170);
                    columns.ConstantColumn(100);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Coffee Name");
                    header.Cell().Element(CellStyle).Text("Sold Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var coffee in pdfDocObj.Coffees)
                {
                    table.Cell().Element(CellStyle).Text(coffee.ItemName).FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text(coffee.Quantity.ToString()).FontColor("#EE2020");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeAddInsTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(180);
                    columns.ConstantColumn(100);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Add-In Name");
                    header.Cell().Element(CellStyle).Text("Sold Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var addIn in pdfDocObj.AddIns)
                {
                    table.Cell().Element(CellStyle).Text(addIn.ItemName).FontColor("#EE2020");
                    table.Cell().Element(CellStyle).Text(addIn.Quantity.ToString()).FontColor("#EE2020");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
    }
}
