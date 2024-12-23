using API_PCHY.Models.QLTN.DM_KHACH_HANG;
using API_PCHY.Models.QLTN.DM_LOAI_TAI_SAN;
using APIPCHY.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PCHY.Controllers.QLTN.DM_KHACH_HANG
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class DM_KHACH_HANGController : ControllerBase
    {
        private readonly DM_KHACH_HANG_Manager _dmKhachHang = new DM_KHACH_HANG_Manager();



        [HttpGet]
        [Route("getAll_DM_KHACH_HANG")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _dmKhachHang.get_DM_KHACHHANG();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


        // Thêm khách hàng mới (POST)
        //{
        //  "ten_kh": "string",
        //  "ghi_chu": "string",
        //  "nguoi_tao": "string",
        //  "so_dt": "08997668",
        //  "email": "string",
        //  "ma_so_thue": "string",
        //  "dia_chi": "string"
        //}
        [HttpPost]
        [Route("insert_DM_KHACH_HANG")]
        public IActionResult Insert([FromBody] DM_KHACH_HANG_Model khachHang)
        {
            string result = _dmKhachHang.insert_DM_KHACHHANG(khachHang);

            if (string.IsNullOrEmpty(result))
            {
                return Ok("Thêm khách hàng thành công.");
            }
            else
            {
                return BadRequest($"Lỗi: {result}");
            }
        }


        // Sửa khách hàng 
        //{
        //  "id": 62,
        //  "ten_kh": "Tên khách hàng đã sửa",
        //  "ghi_chu": "Ghi chú đã sửa",
        //  "nguoi_tao": "Người tạo đã sửa",
        //  "so_dt": "0987654321",
        //  "email": "newemail@example.com",
        //  "ma_so_thue": "987654321",
        //  "dia_chi": "Địa chỉ đã sửa"
        //}
        [HttpPut]
        [Route("update_DM_KHACH_HANG")]
        public IActionResult Update([FromBody] DM_KHACH_HANG_Model khachHang)
        {
            string result = _dmKhachHang.update_DM_KHACHHANG(khachHang);

            if (string.IsNullOrEmpty(result))
            {
                return Ok("Sửa khách hàng thành công.");
            }
            else
            {
                return BadRequest($"Lỗi: {result}");
            }
        }


        //Xoa khac hang
        [Route("delete_DM_KHACH_HANG")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string result = _dmKhachHang.delete_DM_KHACHHANG(id);
            if (string.IsNullOrEmpty(result))
            {
                return Ok("Xóa khách hàng thành công.");
            }
            else
            {
                return BadRequest($"Lỗi: {result}"); // Nếu có lỗi từ DAL
            }
        }


      [HttpPost("search_DM_KHACH_HANG")]
      public IActionResult Search([FromBody] DM_KHACH_HANG_Request request)
        {

            int totalRecords = 0;
            int totalPages = 0;
            var results = _dmKhachHang.search_DM_KHACH_HANG(request, out totalRecords, out totalPages);

            var result = new
            {
                page = request.pageIndex,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageSize = request.pageSize,
                data = results
            };

            return Ok(result);
        }

    }

}
