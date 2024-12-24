using API_PCHY.Models.QLTN.QLTN_THIET_BI_YCTN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;

using OfficeOpenXml; // Thư viện để đọc file Excel
using OfficeOpenXml.Style;
using System.Drawing;
using System.Linq;
using System.Transactions;

namespace API_PCHY.Controllers.QLTN.QLTN_THIET_BI_YCTN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_THIET_BI_YCTN_Controller : ControllerBase
    {
        QLTN_THIET_BI_Manager manager = new QLTN_THIET_BI_Manager();

        [Route("getAll_thietbi_byMA_YCTN")]
        [HttpPost]
        public ActionResult getbyMA_yctn(QLTN_THIET_BI_Model model)
        {
            try
            {
                List<QLTN_THIET_BI_Model> result = manager.getALL_QLTN_THIET_BI_byMA_yctn(model);

                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("insert_Thiet_Bi_YCTN")]
        [HttpPost]
        public IActionResult insert([FromBody] QLTN_THIET_BI_Model a)
        {
            try
            {
                string result = manager.insert_QLTN_THIET_BI(a);
                return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [Route("insert_Multiple_Thiet_Bi_YCTN")]
        [HttpPost]
        public string InsertMultiple(List<QLTN_THIET_BI_Model> models)
        {
            string result = string.Empty;

            foreach (var model in models)
            {
                // Gọi hàm insert_QLTN_THIET_BI cho mỗi đối tượng model
                result = manager.insert_QLTN_THIET_BI(model);

                // Kiểm tra kết quả trả về nếu cần
                if (!string.IsNullOrEmpty(result))
                {
                    // Xử lý lỗi nếu cần
                    break;
                }
            }

            return result; // Trả về kết quả cuối cùng
        }




        [Route("update_Thiet_Bi_YCTN")]
        [HttpPut]
        public IActionResult Update([FromBody] QLTN_THIET_BI_Model a)
        {
            try
            {
                // Gọi hàm xử lý logic từ manager
                string result = manager.Update_QLTN_THIET_BI(a);
                // Xử lý kết quả trả về
                return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);
            }
            catch (Exception ex)
            {
                // Trả về lỗi chi tiết từ exception
                return BadRequest(ex.Message);
            }
        }


        [Route("delete_Thiet_Bi_YCTN")]
        [HttpDelete]
        public IActionResult delete(int id)
        {
            try
            {
                string result = manager.Delete_QLTN_THIET_BI(id);
                return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }


        [Route("importExcel_Thiet_Bi_YCTN")]
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }

            try
            {
                var devices = new List<QLTN_THIET_BI_Model>();
                var errors = new List<string>();  // Danh sách chứa các lỗi
                // Đọc file Excel
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Bỏ qua tiêu đề (hàng 1)
                        {
                            // Kiểm tra nếu các cột quan trọng có dữ liệu rỗng
                            var maTbtn = worksheet.Cells[row, 2].Text;
                            var tenThietBi = worksheet.Cells[row, 3].Text;
                            var soLuongText = worksheet.Cells[row, 4].Text;

                            if (string.IsNullOrWhiteSpace(maTbtn) && string.IsNullOrWhiteSpace(tenThietBi) && string.IsNullOrWhiteSpace(soLuongText))
                            {
                                // Nếu dữ liệu rỗng hoặc không hợp lệ, bỏ qua dòng này
                                continue;
                            }

                            if (string.IsNullOrWhiteSpace(maTbtn) || string.IsNullOrWhiteSpace(tenThietBi) || string.IsNullOrWhiteSpace(soLuongText))
                            {
                                // Nếu dữ liệu rỗng 
                                errors.Add($"Dòng {row}: Một trường dữ liệu đang để trống!");
                            }

                            // Kiểm tra xem "số lượng" có phải là số hợp lệ không
                            if (!int.TryParse(soLuongText, out int soLuong) || soLuong < 1)
                            {
                                errors.Add($"Dòng {row}: Số lượng không hợp lệ. Vui lòng nhập số lớn hơn 0.");
                            }
                            // Nếu có lỗi, bỏ qua dòng này
                            if (errors.Count > 0)
                            {
                                continue;
                            }
                            var device = new QLTN_THIET_BI_Model
                            {
                                ma_loai_tb = worksheet.Cells[row, 2].Text,
                                ten_thiet_bi = worksheet.Cells[row, 3].Text,
                                so_luong = string.IsNullOrEmpty(worksheet.Cells[row, 4].Text) ? null : (int?)int.Parse(worksheet.Cells[row, 4].Text),
                            };

                            devices.Add(device);
                        }
                    }
                }
                // Nếu có lỗi, trả về danh sách lỗi
                if (errors.Any())
                {
                    return BadRequest(string.Join("\n", errors));
                }
                // Chèn vào cơ sở dữ liệu
                //foreach (var device in devices)
                //{
                //    string result = manager.insert_QLTN_THIET_BI(device);
                //    if (!string.IsNullOrEmpty(result))
                //    {
                //        return BadRequest($"Lỗi khi chèn thiết bị: {device.ten_thiet_bi}, Lỗi: {result}");
                //    }
                //}

                // Trả về danh sách để preview
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        [Route("downloadExcelTemplate_Thiet_Bi_YCTN")]
        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            try
            {
                // Bật giấy phép không thương mại của EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // 1. Tạo file Excel mẫu
                using (var package = new ExcelPackage())
                {
                    // Tạo worksheet
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Thêm tiêu đề các cột
                    worksheet.Cells[1, 1].Value = "STT";
                    worksheet.Cells[1, 2].Value = "Mã loại thiết bị";
                    worksheet.Cells[1, 3].Value = "Tên thiết bị";
                    worksheet.Cells[1, 4].Value = "Số lượng";

                    // Thêm dữ liệu ví dụ
                    worksheet.Cells[2, 1].Value = "1";
                    worksheet.Cells[2, 2].Value = "Mã loại thiết bị";
                    worksheet.Cells[2, 3].Value = "Tên thiết bị";
                    worksheet.Cells[2, 4].Value = "0";

                    worksheet.Cells[3, 1].Value = "...";
                    worksheet.Cells[3, 2].Value = "...";
                    worksheet.Cells[3, 3].Value = "...";
                    worksheet.Cells[3, 4].Value = "...";

                    worksheet.Row(1).Height = 30;  // Điều chỉnh chiều cao của hàng đầu tiên
                    worksheet.Column(1).Width = 30;
                    worksheet.Column(2).Width = 30;
                    worksheet.Column(3).Width = 30;
                    worksheet.Column(4).Width = 30;

                    // Định dạng cột tiêu đề (hàng đầu tiên)
                    using (var range = worksheet.Cells[1, 1, 1, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 20;  // Thay đổi cỡ chữ cho tiêu đề (14 là cỡ chữ ví dụ)
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        range.Style.Font.Color.SetColor(Color.White);

                    }
                    // 2. Trả file về phía client với tên có thêm ngày tháng
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0; // Reset lại vị trí stream

                    // Lấy ngày tháng hiện tại để thêm vào tên file
                    string datePart = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string fileName = $"ExportTemplateFile_KhoiLuongThucHien_{datePart}.xlsx";

                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }


        [Route("Nhap_Khoi_Luong_Phat_Sinh")]
        [HttpPost]
        public string Nhap_Khoi_Luong_Phat_Sinh(List<QLTN_THIET_BI_Model> models)
        {
            string result = string.Empty;

            foreach (var model in models)
            {
                // Gọi hàm insert_QLTN_THIET_BI cho mỗi đối tượng model
                result = manager.Nhap_Khoi_Luong_Phat_Sinh(model);

                // Kiểm tra kết quả trả về nếu cần
                if (!string.IsNullOrEmpty(result))
                {
                    // Xử lý lỗi nếu cần
                    break;
                }
            }

            return result; // Trả về kết quả cuối cùng
        }

    }
}
