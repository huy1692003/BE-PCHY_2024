using API_PCHY.Models.QLTN.QLTN_BUOC_YCTN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace API_PCHY.Controllers.QLTN.QLTN_BUOC_YCTN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_BUOC_YCTNController : ControllerBase
    {
        QLTN_BUOC_YCTN_Manager manager = new QLTN_BUOC_YCTN_Manager();

        [Route("getAll_QLTN_BUOC_YCTN")]
        [HttpGet]
        public ActionResult getbyMA_yctn()
        {
            try
            {
                List<QLTN_BUOC_YCTN_Model> result = manager.GetAll_QLTN_BUOC_YCTN();

                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("insert_QLTN_BUOC_YCTN")]
        [HttpPost]
        public IActionResult insert_BUOC_YCTN([FromBody] QLTN_BUOC_YCTN_Model QLTN_BUOC_YCTN_Model)
        {
            string result = manager.Insert_QLTN_BUOC_YCTN(QLTN_BUOC_YCTN_Model);
            return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);
        }

        [Route("update_QLTN_BUOC_YCTN")]
        [HttpPut]
        public IActionResult update_DM_LOAITHIETBI([FromBody] QLTN_BUOC_YCTN_Model QLTN_BUOC_YCTN_Model)
        {
            string result = manager.Update_QLTN_BUOC_YCTN(QLTN_BUOC_YCTN_Model);
            return result == "Cập nhật thành công!" ? Ok("Thành công") : BadRequest(result);

        }

        [Route("delete_QLTN_BUOC_YCTN")]
        [HttpDelete]
        public IActionResult delete_QLTN_BUOC_YCTN(int id)
        {
            string result = manager.Delete_QLTN_BUOC_YCTN(id);
            return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);
        }
    }
}
