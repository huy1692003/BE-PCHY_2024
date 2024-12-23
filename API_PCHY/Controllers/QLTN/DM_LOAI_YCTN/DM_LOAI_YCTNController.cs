using Microsoft.AspNetCore.Mvc;
using API_PCHY.Models.QLTN.DM_LOAI_YCTN;
using System;

namespace API_PCHY.Controllers.QLTN.DM_LOAI_YCTN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class DM_LOAI_YCTNController : ControllerBase
    {
        private readonly DM_LOAI_YCTN_Manager _manager;

        public DM_LOAI_YCTNController()
        {
            _manager = new DM_LOAI_YCTN_Manager();
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _manager.get_All_DM_LOAI_YCTN();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] DM_LOAI_YCTN_Model model)
        {
            try
            {
                bool result = _manager.insert_DM_LOAI_YCTN(model);
                return result ? Ok("Thêm mới thành công") : BadRequest("Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] DM_LOAI_YCTN_Model model)
        {
            try
            {
                bool result = _manager.update_DM_LOAI_YCTN(model);
                return result ? Ok("Cập nhật thành công") : BadRequest("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _manager.delete_DM_LOAI_YCTN(id);
                return result ? Ok("Xóa thành công") : BadRequest("Xóa thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
    }
}
