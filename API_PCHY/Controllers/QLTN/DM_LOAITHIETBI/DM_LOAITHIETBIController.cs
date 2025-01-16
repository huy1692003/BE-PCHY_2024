using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_PCHY.Models.QLTN.DM_LOAITHIETBI;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace API_PCHY.Controllers.QLTN.DM_LOAITHIETBI
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class DM_LOAITHIETBIController : ControllerBase
    {
        DM_LOAITHIETBI_Manager manager = new DM_LOAITHIETBI_Manager();

        [HttpGet("getAll_DM_LOAITHIETBI")]
        public ActionResult Get()
        {
            try { 
                List<DM_LOAITHIETBI_Model> result = manager.getALL_DM_LOAITHIETBI();

                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("insert_DM_LOAITHIETBI")]
        [HttpPost]
        public IActionResult insert_DM_LOAITHIETBI([FromBody] DM_LOAITHIETBI_Model dM_LOAITHIETBI)
        {
            string result = manager.insert_DM_LOAITHIETBI(dM_LOAITHIETBI);
            return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);
        }

        [Route("update_DM_LOAITHIETBI")]
        [HttpPut]
        public IActionResult update_DM_LOAITHIETBI([FromBody] DM_LOAITHIETBI_Model dM_LOAITHIETBI)
        {
            string result = manager.Update_DM_LOAITHIETBI(dM_LOAITHIETBI);
            return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);
        }


        [Route("delete_DM_LOAITHIETBI")]
        [HttpDelete]
        public IActionResult delete_DM_LOAITHIETBI(int id)
        {
            try
            {
                string result = manager.Delete_DM_LOAITHIETBI(id);
                return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);

            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("search_DM_LOAITHIETBI")]
        public IActionResult search_DM_LOAITHIETBI([FromBody] DM_LOAITHIETBI_Model modelip)
        {

            try
            {
                
                List<DM_LOAITHIETBI_Model> result = manager.Search_DM_LOAITHIETBI(modelip);



                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



    }
}
