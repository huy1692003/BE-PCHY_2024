using System;
using System.IO;
using GrapeCity.Documents.Word;
using GrapeCity.Documents.Word.Layout;
using System.IO.Compression;

namespace API_PCHY.Helpers
{
    public class FileHelper
    {
        public  string ConvertFileToBase64(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("File không tồn tại.");
            }

            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".docx" || extension == ".doc")
            {
                return ConvertDocxToBase64(filePath);
            }
            else if (extension == ".pdf")
            {
                return ConvertPdfToBase64(filePath);
            }
            else
            {
                throw new NotSupportedException("Chỉ hỗ trợ file DOCX và PDF.");
            }
        }

        private static string ConvertDocxToBase64(string filePath)
        {
            var wordDoc = new GcWordDocument();
            // Load DOCX file
            wordDoc.Load(filePath);

            using (var layout = new GcWordLayout(wordDoc))
            {
                // Define the PDF output settings
                PdfOutputSettings pdfOutputSettings = new PdfOutputSettings
                {
                    CompressionLevel = CompressionLevel.Fastest,
                    ConformanceLevel = GrapeCity.Documents.Pdf.PdfAConformanceLevel.PdfA1a
                };

                using (var memoryStream = new MemoryStream())
                {
                    // Save the Word layout to a MemoryStream as PDF
                    // Lưu vào MemoryStream thay vì truyền đường dẫn tệp
                    layout.SaveAsPdf(memoryStream, null, pdfOutputSettings);

                    // Reset vị trí của MemoryStream trước khi đọc
                    memoryStream.Position = 0;

                    // Chuyển dữ liệu trong MemoryStream thành Base64
                    return ConvertToBase64(memoryStream);
                }
            }
        }

        private static string ConvertPdfToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }

        private static string ConvertToBase64(MemoryStream memoryStream)
        {
            // Đọc dữ liệu từ MemoryStream và chuyển thành Base64
            byte[] fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }



    }
}
