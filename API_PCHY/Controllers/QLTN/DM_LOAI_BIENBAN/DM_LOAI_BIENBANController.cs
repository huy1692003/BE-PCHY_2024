using API_PCHY.Models.QLTN.DM_KHACH_HANG;
using API_PCHY.Models.QLTN.DM_LOAI_BIENBAN;
using API_PCHY.Models.QLTN.DM_LOAI_TAI_SAN;
using API_PCHY.Models.QLTN.DM_LOAI_YCTN;
using API_PCHY.Models.QLTN.DM_TRUONG_YCTN;
using APIPCHY.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_PCHY.Controllers.QLTN.DM_LOAI_BIENBAN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class DM_LOAI_BIENBANController : ControllerBase
    {
        private readonly DM_LOAI_BIENBAN_Manager _manager;

        public DM_LOAI_BIENBANController()
        {
            _manager = new DM_LOAI_BIENBAN_Manager();
        }


        [HttpGet("get_all_DM_LOAI_BIENBAN")]
        public ActionResult Get()
        {
            try
            {
                List<DM_LOAI_BIENBAN_Model> result = _manager.get_All_DM_LOAI_BIENBAN();

                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("insert_DM_LOAI_BIENBAN")]
        public IActionResult Insert([FromBody] DM_LOAI_BIENBAN_Model model)
        {
            try
            {
                bool result = _manager.insert_DM_LOAI_BIENBAN(model);
                return result ? Ok("Thêm mới thành công") : BadRequest("Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("update_DM_LOAI_BIENBAN")]
        public IActionResult Update([FromBody] DM_LOAI_BIENBAN_Model model)
        {
            try
            {
                bool result = _manager.update_DM_LOAI_BIENBAN(model);
                return result ? Ok("Cập nhật thành công") : BadRequest("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete_DM_LOAI_BIENBAN/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _manager.delete_DM_LOAI_BIENBAN(id);
                return result ? Ok("Xóa thành công") : BadRequest("Xóa thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


        [HttpPost("search_DM_LOAI_BIENBAN")]
        public IActionResult Search([FromBody] DM_LOAI_BIENBAN_Request request)
        {

            int totalRecords = 0;
            int totalPages = 0;
            var results = _manager.search_DM_LOAI_BIENBAN(request, out totalRecords, out totalPages);

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