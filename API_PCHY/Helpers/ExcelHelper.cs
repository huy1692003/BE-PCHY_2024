using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Drawing;

public class ExcelHelper
{
    public ExcelHelper()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public byte[] ExportToExcel<T>(List<T> data, List<string> columnNames, List<string> hyperlinkColumns)
    {
        using (var package = new ExcelPackage())
        {
            // Tạo một worksheet mới
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            // Tạo tiêu đề cho các cột và căn chỉnh header
            for (int col = 0; col < columnNames.Count; col++)
            {
                var headerCell = worksheet.Cells[1, col + 1];
                headerCell.Value = columnNames[col];
                headerCell.Style.Font.Color.SetColor(Color.White); // Chữ trắng
                headerCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // Đặt kiểu nền là solid
                headerCell.Style.Fill.BackgroundColor.SetColor(Color.Green); // Nền xanh
                headerCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Căn giữa
                headerCell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Căn giữa theo chiều dọc

                // Thêm border cho các cạnh của header
                headerCell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                headerCell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                headerCell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                headerCell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // Tăng padding bằng cách điều chỉnh chiều cao dòng
                worksheet.Row(1).Height = 25; // Điều chỉnh chiều cao của dòng header để tạo thêm không gian
            }

            // Điền dữ liệu vào bảng
            for (int row = 0; row < data.Count; row++)
            {
                var item = data[row];

                // Duyệt qua các cột để lấy giá trị
                for (int col = 0; col < columnNames.Count; col++)
                {
                    var value = GetPropertyValue(item, col); // Lấy giá trị từ đối tượng theo thứ tự cột

                    // Điền giá trị cho các cột thông thường
                    worksheet.Cells[row + 2, col + 1].Value = value;

                    // Kiểm tra nếu cột này là hyperlink và giá trị là một URL hợp lệ
                    if (hyperlinkColumns.Contains(columnNames[col]) && value != null && Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute))
                    {
                        // Hiển thị "Link" thay vì URL trong ô Excel
                        worksheet.Cells[row + 2, col + 1].Hyperlink = new Uri(value.ToString());
                        worksheet.Cells[row + 2, col + 1].Value = "Link";

                        // Định dạng liên kết: chữ xanh nước biển và gạch chân
                        worksheet.Cells[row + 2, col + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row + 2, col + 1].Style.Font.UnderLine = true; // Gạch chân
                        worksheet.Cells[row + 2, col + 1].Style.Font.Color.SetColor(Color.Blue); // Màu chữ xanh nước biển
                    }
                }
            }

            // Tự động điều chỉnh chiều rộng cột để vừa với nội dung của cả header và dữ liệu
            for (int col = 0; col < columnNames.Count; col++)
            {
                worksheet.Column(col + 1).AutoFit();
            }

            // Trả về mảng byte của file Excel
            return package.GetAsByteArray();
        }
    }

    // Hàm hỗ trợ lấy giá trị của đối tượng theo thứ tự cột (dựa trên chỉ số cột)
    private object GetPropertyValue<T>(T item, int columnIndex)
    {
        var properties = item.GetType().GetProperties();
        if (columnIndex >= 0 && columnIndex < properties.Length)
        {
            return properties[columnIndex].GetValue(item);
        }

        return null;
    }
}
