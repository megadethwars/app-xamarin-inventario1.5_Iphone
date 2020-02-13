using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;

namespace Inventario2
{
    public class GeneratePDF : ContentPage
    {
        DataTable table;
        string IdSalida;
        string correo;
        MemoryStream streamPDF;
        public GeneratePDF()
        {

        }

        public async Task InitPDFAsync(string idSalida)
        {
            streamPDF = new MemoryStream();
            IdSalida = idSalida;

            table = new DataTable();

            await MainTask();

        }


        public bool CreatePDF(Movimientos movimientos, DataTable tablacarrito, Model.Usuario User)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.All = 50;
                //Adds a page to the document
                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;

                //Loads the image from disk
                //PdfImage image = PdfImage.FromFile("Logo.png");

                Stream imageStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("Inventario2.Assets.NewLogo.jpeg");
                //Load the image from the disk.
                PdfBitmap image = new PdfBitmap(imageStream);
                //Draw the image
                RectangleF bounds = new RectangleF(0, 0, 110, 110);
                //Draws the image to the PDF page
                page.Graphics.DrawImage(image, bounds);


                //DRAW THE MAIN TITLE
                PdfFont Headfont = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                //Creates a text element to add the invoice number
                PdfTextElement headelement = new PdfTextElement("AUDIO VIDEO STUDIOS ", Headfont);
                headelement.Brush = PdfBrushes.Red;
                PdfLayoutResult result = headelement.Draw(page, new PointF(graphics.ClientSize.Width - 350, graphics.ClientSize.Height - 740));


                PdfFont Subtitle = new PdfStandardFont(PdfFontFamily.Helvetica, 14);
                //Creates a text element to add the invoice number
                PdfTextElement subtitelement = new PdfTextElement("ORDEN DE SALIDA ", Subtitle);
                subtitelement.Brush = PdfBrushes.Red;
                PdfLayoutResult Subresult = subtitelement.Draw(page, new PointF(graphics.ClientSize.Width - 300, graphics.ClientSize.Height - 710));


                PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(222, 237, 242));
                bounds = new RectangleF(bounds.Right, Subresult.Bounds.Bottom, graphics.ClientSize.Width - 300, 50);
                //Draws a rectangle to place the heading in that region.
                graphics.DrawRectangle(solidBrush, bounds);

                //creating fields, folio, fecha, lugar
                PdfFont campofont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement lblugar = new PdfTextElement("EVENTO: ", campofont);
                lblugar.Brush = PdfBrushes.Black;
                PdfLayoutResult reslblugar = lblugar.Draw(page, new PointF(bounds.Left + 40, bounds.Top));

                PdfTextElement lbfecha = new PdfTextElement("FECHA: ", campofont);
                lbfecha.Brush = PdfBrushes.Black;
                PdfLayoutResult reslbfecha = lbfecha.Draw(page, new PointF(bounds.Left + 40, bounds.Top + 16));

                PdfTextElement lbfolio = new PdfTextElement("FOLIO: ", campofont);
                lbfolio.Brush = PdfBrushes.Black;
                PdfLayoutResult reslbfolio = lbfolio.Draw(page, new PointF(bounds.Left + 40, bounds.Top + 32));


                PdfBrush solidBrush2 = new PdfSolidBrush(new PdfColor(190, 220, 228));
                bounds = new RectangleF(bounds.Right, Subresult.Bounds.Bottom, graphics.ClientSize.Width - 300, 50);
                //Draws a rectangle to place the heading in that region.
                graphics.DrawRectangle(solidBrush2, bounds);


                //variables de campos
                PdfTextElement lugar = new PdfTextElement(movimientos.lugar, campofont);
                lugar.Brush = PdfBrushes.Black;
                PdfLayoutResult reslugar = lugar.Draw(page, new PointF(bounds.Left + 40, bounds.Top));

                PdfTextElement fecha = new PdfTextElement(DateTime.Now.ToString(), campofont);
                fecha.Brush = PdfBrushes.Black;
                PdfLayoutResult resfecha = fecha.Draw(page, new PointF(bounds.Left + 40, bounds.Top + 16));

                PdfTextElement folio = new PdfTextElement(movimientos.ID, campofont);
                folio.Brush = PdfBrushes.Black;
                PdfLayoutResult resfolio = folio.Draw(page, new PointF(bounds.Left + 40, bounds.Top + 32));

                //create table

                //Creates the datasource for the table
                DataTable invoiceDetails = tablacarrito;
                //Creates a PDF grid
                PdfGrid grid = new PdfGrid();
                //Adds the data source
                grid.DataSource = invoiceDetails;
                //Creates the grid cell styles
                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = PdfPens.White;
                PdfGridRow header = grid.Headers[0];
                //Creates the header style
                PdfGridCellStyle headerStyle = new PdfGridCellStyle();
                headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
                headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
                headerStyle.TextBrush = PdfBrushes.White;
                headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16f, PdfFontStyle.Regular);

                //Adds cell customizations
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    if (i == 0 || i == 1)
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }

                //Applies the header style
                header.ApplyStyle(headerStyle);
                cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
                cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f);
                cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
                //Creates the layout format for grid
                PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
                // Creates layout format settings to allow the table pagination
                layoutFormat.Layout = PdfLayoutType.Paginate;

                //Draws the grid to the PDF page.
                PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 150), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

                PdfGraphics graphicsSecond = gridResult.Page.Graphics;

                PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 1.0f);
                PointF startPoint = new PointF(0, gridResult.Bounds.Bottom + 60);
                PointF endPoint = new PointF(150, gridResult.Bounds.Bottom + 60);
                //Draws a line at the bottom of the address
                graphicsSecond.DrawLine(linePen, startPoint, endPoint);


                PdfFont entregafont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement lbentrega = new PdfTextElement("ENTREGA: ", entregafont);
                lbentrega.Brush = PdfBrushes.Black;
                PdfLayoutResult reslbentrega = lbentrega.Draw(gridResult.Page, new PointF(linePen.Width / 2.0f, startPoint.Y + 5));

                //texto de quien entrega
                PdfFont usuarioentregafont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement lbusuarioentrega = new PdfTextElement(Model.User.nombre + " " + Model.User.apellido_paterno, usuarioentregafont);
                lbusuarioentrega.Brush = PdfBrushes.Black;
                PdfLayoutResult reslbusuarioentrega = lbusuarioentrega.Draw(gridResult.Page, new PointF(linePen.Width / 2.0f, startPoint.Y - 20));


                PdfPen linePenfinal = new PdfPen(new PdfColor(126, 151, 173), 1.0f);
                PointF startPointfinal = new PointF(350, gridResult.Bounds.Bottom + 60);
                PointF endPointfinal = new PointF(graphics.ClientSize.Width, gridResult.Bounds.Bottom + 60);
                //Draws a line at the bottom of the address
                graphicsSecond.DrawLine(linePenfinal, startPointfinal, endPointfinal);

                PdfFont recibefont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement lbrecibe = new PdfTextElement("RECIBE: ", recibefont);
                lbrecibe.Brush = PdfBrushes.Black;
                PdfLayoutResult reslbrecibe = lbrecibe.Draw(gridResult.Page, new PointF(350.0f + (linePenfinal.Width / 2.0f), startPoint.Y + 5));

                //texto de quien recibe
                PdfFont usuariofont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement lbusuario = new PdfTextElement(User.nombre + " " + User.apellido_paterno, usuariofont);
                lbusuario.Brush = PdfBrushes.Black;
                PdfLayoutResult reslbusuario = lbusuario.Draw(gridResult.Page, new PointF(350.0f + (linePenfinal.Width / 2.0f), startPoint.Y - 20));


                MemoryStream stream = new MemoryStream();

                //Save the document.
                document.Save(stream);
                streamPDF = stream;
                //Close the document.
                document.Close(true);

                byte[] bytes = stream.ToArray();


                bool res = SendSTMPT(bytes, correo);
                string save = "OrdenDeSalida-" + movimientos.ID;
                //Save the stream as a file in the device and invoke it for viewing
                // Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView(save + ".pdf", "application/pdf", stream);
                //The operation in Save under Xamarin varies between Windows Phone, Android and iOS platforms. Please refer PDF/Xamarin section for respective code samples.

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    // Xamarin.Forms.DependencyService.Get<ISaveWindowsPhone>().Save("Output.pdf", "application/pdf", stream);
                    Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView(save + ".pdf", "application/pdf", stream);
                }
                else
                {
                    Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView(save + ".pdf", "application/pdf", stream);
                }


                return true;
            }
            catch
            {
                return false;
            }

        }

        private async Task MainTask()
        {
            DataTable tablacarrito;
            tablacarrito = new DataTable();
            try
            {
                List<Movimientos> lista = await queryData(IdSalida);
                if (lista.Count != 0)
                {
                    List<Model.Usuario> listaUsuario = await getUser(lista[0].usuario);
                    //fill table
                    if (listaUsuario.Count != 0)
                    {
                        correo = Model.User.correo;
                    }

                    tablacarrito.Columns.Add("CANT", typeof(int));
                    tablacarrito.Columns.Add("CODIGO", typeof(string));
                    tablacarrito.Columns.Add("DESCRP", typeof(string));
                    tablacarrito.Columns.Add("MARCA", typeof(string));
                    tablacarrito.Columns.Add("MODELO", typeof(string));
                    tablacarrito.Columns.Add("SERIE", typeof(string));



                    foreach (Movimientos mov in lista)
                    {
                        tablacarrito.Rows.Add(mov.cantidad, mov.codigo, mov.producto, mov.marca, mov.modelo, mov.serie);
                    }


                    CreatePDF(lista[0], tablacarrito, listaUsuario[0]);
                    tablacarrito.Dispose();
                }
                else
                {

                }
            }
            catch
            {
                await DisplayAlert("Error", "Error de consulta", "Aceptar");
            }


        }

        private async Task<List<Movimientos>> queryData(string IDsalida)
        {
            try
            {
                var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.ID == IDsalida).ToListAsync();


                return table;
            }
            catch
            {
                return null;
            }
            // searching only idproduct


        }

        private async Task<List<Model.Usuario>> getUser(string usuario)
        {
            try
            {
                var table = await App.MobileService.GetTable<Model.Usuario>().Where(u => u.nombre == usuario).ToListAsync();


                return table;
            }
            catch
            {
                return null;
            }
        }

        private bool SendSTMPT(byte[] bytes, string correo)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("avsinventario@gmail.com");
                mail.To.Add(correo);
                mail.Subject = "Orden de salida";
                mail.Body = "AVS Orden de salida, no responder";
                System.Net.Mail.Attachment attachment;

                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                ct.MediaType = MediaTypeNames.Application.Pdf;
                ct.Name = "output " + DateTime.Now.ToString() + ".pdf";

                attachment = new System.Net.Mail.Attachment(new MemoryStream(bytes), ct);
                mail.Attachments.Add(attachment);
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("avsinventario@gmail.com", "avs123456");
                SmtpServer.Send(mail);



                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DisplayAlert("Error", "Error de envio a correo", "Aceptar");
                return false;
            }



        }

    }


}