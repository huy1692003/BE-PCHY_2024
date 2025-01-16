using API_PCHY.Models.QLTN.DM_LOAI_TAI_SAN;
using APIPCHY.Helpers;
using APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_PCHY.Controllers.QLTN.DM_LOAI_TAI_SAN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class DM_LOAI_TAI_SANController : ControllerBase
    {
        private readonly DM_LOAI_TAI_SAN_Manager _dmLoaiTS = new DM_LOAI_TAI_SAN_Manager();


        /*
        * Tim kiem loai tai san 
        * POST: api/search
        */
        [HttpPost("Search_DM_LOAI_TAI_SAN")]
        public ActionResult<DM_LOAI_TAI_SAN_Model> Search_DM_LOAI_TAI_SAN([FromBody] DM_LOAI_TAI_SAN_Request request)
        {
            int totalRecord = 0;
            List<DM_LOAI_TAI_SAN_Model> resultList;
            try
            {
                resultList = _dmLoaiTS.SearchDMLoaiTaiSan(request.Search, request.Page, request.PageSize, out totalRecord);
                return Ok(new { TotalCount = totalRecord, Data = resultList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Controller: loi >>{ex.Message}");
            }

        }


        /*
        * Tim kiem loai tai san
        * GET: api/DM_LOAI_TAI_SAN/getByID
        */
        [HttpGet]
        [Route("GET_DM_LOAI_TAI_SAN")]
        public IActionResult GET_DM_LOAI_TAI_SA()
        {
            try
            {
                var result = _dmLoaiTS.get_DM_LOAI_TAI_SAN_BYID();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Loại tài sản không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        [Route("insert_DM_LOAI_TAI_SAN")]
        [HttpPost]
        public IActionResult Insert_DM_LOAI_TAI_SAN([FromBody] DM_LOAI_TAI_SAN_Model model)
        {

            // Gọi phương thức DAL
            string result = _dmLoaiTS.Insert_DM_LOAI_TAI_SAN(model);

            return Ok(result);
        }




        [Route("update_DM_LOAI_TAI_SAN")]
        [HttpPut]
        public void UpdateLoaiTaiSan([FromBody] DM_LOAI_TAI_SAN_Model model)
        {
            _dmLoaiTS.Update_DM_LOAI_TAI_SAN(model);
        }



        [Route("delete_DM_LOAI_TAI_SAN")]
        [HttpDelete]
        public IActionResult delete_LOAI_TAI_SAN_ByID(int id)
        {
            try
            {
                string result = _dmLoaiTS.delete_DM_LOAI_TAI_SAN_ByID(id);
                return !string.IsNullOrEmpty(result) ? BadRequest(result) : Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
