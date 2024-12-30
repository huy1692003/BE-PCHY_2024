using Microsoft.AspNetCore.Mvc;
using API_PCHY.Models.QLTN.QLTN_YCTN;
using System;
using System.Collections.Generic;

namespace API_PCHY.Controllers.QLTN.QLTN_YCTN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_YCTNController : ControllerBase
    {
        private readonly QLTN_YCTN_Manager _manager;

        public QLTN_YCTNController()
        {
            _manager = new QLTN_YCTN_Manager();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] QLTN_YCTN_Model model)
        {
            try
            {
                string result = _manager.create_QLTN_YCTN(model);
                return !string.IsNullOrEmpty(result) ? Ok(result) : BadRequest("Tạo mới thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] QLTN_YCTN_Model model)
        {
            try
            {
                bool result = _manager.update_QLTN_YCTN(model);
                return result ? Ok("Cập nhật thành công") : BadRequest("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{maYCTN}")]
        public IActionResult Delete( string maYCTN)
        {
            try
            {
                bool result = _manager.delete_QLTN_YCTN(maYCTN);
                return result ? Ok("Xóa thành công") : BadRequest("Xóa thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("GiaoNhiemVu")]
        public IActionResult GiaoNhiemVu([FromBody] QLTN_YCTN_Model model)
        {
            try
            {
                bool result = _manager.giao_nhiem_vu_YCTN(model);
                return result ? Ok("Cập nhật giao nhiệm vụ thành công") : BadRequest("Cập nhật giao nhiệm vụ thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }




        [HttpPost]
        [Route("KhaoSatPhuongAn")]
        public IActionResult KhaoSatPhuongAnThiCong([FromBody] QLTN_YCTN_Model model)
        {
            try
            {
                bool result = _manager.khao_sat_phuong_an_YCTN(model);
                return result ? Ok("Khảo sát thành công") : BadRequest("Khảo sát thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }



        [HttpPost]
        [Route("get_DANH_SACH_YCTN")]
        public IActionResult GetDanhSachYCTN([FromBody] QLTN_YCTN_RequestDTO request)
        {
            long totalRecords;
            List<QLTN_YCTN_ResonseDTO> data = _manager.get_DANH_SACH_YCTN(request, out totalRecords);

            return Ok(new
            {
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Data = data
            });
        }



        [HttpGet]
        [Route("SearchMaYCTN")]
        public IActionResult SearchMaYCTN(string maYCTN)
        {
            try
            {
                var result = _manager.search_Ma_YCTN(maYCTN);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetByMAYCTN")]
        public IActionResult GetByID(string MA_YCTN)
        {
            try
            {
                var result = _manager.get_QLTN_YCTN_ByID(MA_YCTN);
                if (result == null)
                {
                    return NotFound("Không tìm thấy dữ liệu");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("BanGiaoKetQua")]
        public IActionResult BanGiaoKetQua([FromBody] QLTN_YCTN_Model model)
        {
            try
            {
                bool result = _manager.ban_giao_ket_qua_YCTN(model);
                return result ? Ok("Cập nhật bàn giao kết quả thành công") : BadRequest("Cập nhật bàn giao kết quả thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
    }
}
