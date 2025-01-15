using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using API_PCHY.Models.QLTN.QLTN_KYSO;
using API_PCHY.Models.QLTN.QLTN_NGUOI_KY;
//using API_PCHY.Models.SMART_CA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PCHY.Controllers.QLTN.QLTN_KYSO
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_KYSOController : ControllerBase
    {
        private QLTN_KYSO_Manager qLTN_KYSO_Manager;
        // Tiêm IWebHostEnvironment vào constructor của controller
        public QLTN_KYSOController(QLTN_KYSO_Manager qLTN_KYSO_Manager)
        {
            this.qLTN_KYSO_Manager = qLTN_KYSO_Manager;
        }


        [Route("search_document")]
        [HttpPost]
        public IActionResult get([FromQuery] Paginage paginage, [FromBody] SearchParamDocument model)
        {
            try
            {
                int totalRecord = 0;

                var result = qLTN_KYSO_Manager.SEARCH_VANBAN(paginage, model, out totalRecord);

                return Ok(new { total = totalRecord, data = result });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Route("exportExcel")]
        [HttpPost]
        public IActionResult exportExcel([FromQuery] Paginage paginage, [FromBody] SearchParamDocument model)
        {
            paginage.Page = 1;
            paginage.PageSize = 1000000; // Mặc định 1 triệu bản ghi
            try
            {
                int totalRecord = 0;

                // Gọi phương thức để lấy dữ liệu
                var result = qLTN_KYSO_Manager.SEARCH_VANBAN(paginage, model, out totalRecord);

                // Lấy URL gốc từ request để tạo đường dẫn đầy đủ
                var baseUrl = $"{Request.Scheme}://{Request.Host}";

                // Xử lý dữ liệu để trả về các cột cần thiết và các cột dạng chuỗi hoặc URL
                var processedResult = result.Select(item => new
                {
                    url = item.file_upload != null ? $"{baseUrl}{item.file_upload}" : null,
                    item.ma_yctn, // Các trường bạn muốn giữ từ item
                    item.loai_yctn,
                    item.ten_thietbi,
                    item.ma_loaitb,
                    item.soluong,
                    item.lanthu,
                    item.don_vi_thuc_hien,
                    item.ten_loai_bb,
                    // Đường dẫn file được tạo thành URL đầy đủ
                    ngaytao = item.ngaytao?.ToString("dd/MM/yyyy"),
                    // Xử lý các cột người ký
                    KyNhay = string.Join("\n ",
                        item.list_NguoiKy
                            .Where(s => s.nhom_nguoi_ky == 1)
                            .Select(s =>
                                $"{s.ho_ten} - [ {s.ten_dang_nhap} ] ({(s.trang_thai_ky == 0 ? "Chưa ký" : "Đã ký")})"
                            )
                    ),
                    KyPhongKythuat = string.Join("\n ",
                        item.list_NguoiKy
                            .Where(s => s.nhom_nguoi_ky == 2) // Ký kỹ thuật
                            .Select(s =>
                                $"{s.ho_ten} - [ {s.ten_dang_nhap} ] ({(s.trang_thai_ky == 0 ? "Chưa ký" : "Đã ký")})"
                            )
                    ),
                    KyGiamdoc = string.Join("\n ",
                        item.list_NguoiKy
                            .Where(s => s.nhom_nguoi_ky == 3) // Ký giám đốc
                            .Select(s =>
                                $"{s.ho_ten} - [ {s.ten_dang_nhap} ] ({(s.trang_thai_ky == 0 ? "Chưa ký" : "Đã ký")})"
                            )
                    ),
                    // Đảm bảo đường dẫn đầy đủ cho URL của file

                }).ToList();

                // Khởi tạo ExcelHelper để xuất dữ liệu ra Excel
                var excelHelper = new ExcelHelper();
                var columnNames = new List<string>
                {
                     "Xem Biên Bản","Mã YCTN", "Loại YCTN", "Tên Thiết bị", "Mã Loại TB", "Số Lượng", "Lần Thứ", "Đơn Vị Thực Hiện",
                    "Tên Loại BB", "Ngày Tạo", "Ký Nháy", "Ký Phòng Kỹ Thuật", "Ký Giám Đốc"
                };

                var hyperlinkColumns = new List<string> { "Xem Biên Bản" }; // Cột "Xem Biên Bản" sẽ là hyperlink

                // Xuất file Excel từ processedResult
                byte[] fileContent = excelHelper.ExportToExcel(processedResult, columnNames, hyperlinkColumns);

                // Tạo tên file Excel
                var fileName = $"DanhSachVanBan_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // Trả về file Excel
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






        [Route("update_TrangThai_Ky")]
        [HttpPost]
        public async Task<IActionResult> insert([FromBody] ReqUpdateKySo model)
        {
            try
            {
                // Lấy URL gốc từ request để tạo đường dẫn đầy đủ
                string msgError = await qLTN_KYSO_Manager.UpdateTrangThaiKy(model);

                return string.IsNullOrEmpty(msgError) ? Ok("Thực hiện ký số hoàn tất") : BadRequest(msgError);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }



}
