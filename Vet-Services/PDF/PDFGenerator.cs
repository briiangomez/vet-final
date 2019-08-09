using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vet_Data.Models;

namespace Veterinaria_Services.PDF
{
    public class PdfGenerator
    {
        public MemoryStream GetPDF(Factura entity)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

            var titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/img/logo.png"));
            logo.ScaleToFit(100, 15);

            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 1f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 1f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Happy Pets", titleFont));
                nextPostCell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Bernardo de Irigoyen 696, Boulogne", bodyFont));
                nextPostCell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("(111) 4444-5555", bodyFont));
                nextPostCell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("info@happypets.com", EmailFont));
                nextPostCell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = iTextSharp.text.Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }


            PdfPTable Invoicetable = new PdfPTable(3);
            Invoicetable.HorizontalAlignment = 0;
            Invoicetable.WidthPercentage = 100;
            Invoicetable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            Invoicetable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Cliente:", bodyFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(entity.Cliente.NombreCompleto, titleFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(entity.Cliente.Direccion, bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("", EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                //middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //middlecell.BorderWidthBottom = 1f;
                Invoicetable.AddCell(middlecell);
            }


            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Factura Nro " + entity.Numero.ToString(), titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Fecha: " + entity.Fecha, bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                //PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Due Date: " + DateTime.Now.AddDays(30).ToShortDateString(), bodyFont));
                //nextPostCell3.Border = Rectangle.NO_BORDER;
                //nested.AddCell(nextPostCell3);
                nested.AddCell("");
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }


            pdfDoc.Add(headertable);
            Invoicetable.PaddingTop = 10f;

            pdfDoc.Add(Invoicetable);
            #endregion

            #region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(5);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell1 = new PdfPCell(new Phrase("NRO.", boldTableFont));
            cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("DESCRIPCION", boldTableFont));
            cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);
            PdfPCell cell3 = new PdfPCell(new Phrase("CANTIDAD", boldTableFont));
            cell3.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("PRECIO UNITARIO", boldTableFont));
            cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
            cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell5);
            foreach (var item in entity.ItemFactura.ToList())
            {
                int ind = entity.ItemFactura.ToList().IndexOf(item) + 1;
                PdfPCell numberCell = new PdfPCell(new Phrase(ind.ToString(), bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk(item.Item.Descripcion, EmailFont));
                _phrase.Add(new Chunk("", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase(item.Cantidad.ToString(), bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(item.Precio.ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(item.Total.ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);

            }
            // Table footer
            PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
            totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell1);
            PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
            totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell2);
            PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(""));
            totalAmtCell3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell3);
            PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total", boldTableFont));
            totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            totalAmtStrCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtStrCell);
            PdfPCell totalAmtCell = new PdfPCell(new Phrase(entity.Importe.ToString(), boldTableFont));
            totalAmtCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtCell);

            //PdfPCell cell = new PdfPCell(new Phrase("***NOTICE: A finance charge of 1.5% will be made on unpaid balances after 30 days. ***", bodyFont));
            //cell.Colspan = 5;
            //cell.HorizontalAlignment = 1;
            //itemTable.AddCell(cell);
            pdfDoc.Add(itemTable);
            #endregion

            PdfContentByte cb = new PdfContentByte(writer);


            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            //cb = new PdfContentByte(writer);
            //cb = writer.DirectContent;
            //cb.BeginText();
            //cb.SetFontAndSize(bf, 8);
            //cb.SetTextMatrix(pageSize.GetLeft(120), 20);
            //cb.ShowText("Invoice was created on a computer and is valid without the signature and seal. ");
            //cb.EndText();

            ////Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, pdfDoc.PageSize.GetBottom(50));
            //cb.LineTo(pdfDoc.PageSize.Width - 40, pdfDoc.PageSize.GetBottom(50));
            //cb.Stroke();

            pdfDoc.Close();
            return PDFData;
        }
    }
}
