using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.ViewModels;
using NReco.PdfGenerator;

namespace Caerus.Common.Tools
{
    public class PdfTools
    {
        public static PdfViewModel ToPdf(string html, string fileName = null, bool landscape = false)
        {
            var result = new PdfViewModel();
            try
            {
                //figure out file name
                fileName = string.IsNullOrEmpty(fileName)
                               ? string.Format("{0}.pdf", DateTime.Now.ToString("yyyyMMdd HHmsfff"))
                               : fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ? fileName : string.Format("{0}.pdf", fileName);


                var pdfGenerator = new HtmlToPdfConverter { Size = PageSize.A4, Margins = { Left = 0.0F, Right = 0.0F } };

                var pdfBytes = pdfGenerator.GeneratePdf(html);
                result.Attachment = pdfBytes;
                result.Name = fileName;
                result.ReplyMessage = "Pdf Generated successfully";
                return result;

                //other option is to use Spire.PDF
            }
            catch (Exception e)
            {
                result.ReplyStatus = ReplyStatus.Fatal;
                result.ReplyMessage = e.Message;
                return result;
            }
        }
    }
}
