using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API_PCHY.Models.QLTN.QLTN_PHANMIEN_YCTN;

namespace API_PCHY.Controllers.QLTN.QLTN_PHANMIEN_YCTN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_PHANMIEN_YCTNController : ControllerBase
    {
        private readonly QLTN_PHANMIEN_YCTN_Manager _manager;

        public QLTN_PHANMIEN_YCTNController()
        {
            _manager = new QLTN_PHANMIEN_YCTN_Manager();
        }

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] QLTN_PHANMIEN_YCTN_Model model)
        {
            try
            {
                bool result = _manager.insert_QLTN_PHANMIEN_YCTN(model);
                return result ? Ok("Thêm mới thành công") : BadRequest("Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _manager.delete_QLTN_PHANMIEN_YCTN(id);
                return result ? Ok("Xóa thành công") : BadRequest("Xóa thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpGet("GetByLoaiYCTN/{id_loai_yctn}")]
        public IActionResult GetByLoaiYCTN(int id_loai_yctn)
        {
            try
            {
                var result = _manager.get_QLTN_PHANMIEN_YCTN_byLOAI_YCTN(id_loai_yctn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
    }
}
