using API_PCHY.Models.QLTN.QLTN_NGUOI_KY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PCHY.Controllers.QLTN.QLTN_NGUOI_KY
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class QLTN_NGUOI_KYController : ControllerBase
    {
        QLTN_NGUOI_KY_Manager manager = new QLTN_NGUOI_KY_Manager();

        [Route("insert_NGUOI_KY_SO")]
        [HttpPost]
        public IActionResult insert([FromBody] QLTN_NGUOI_KY_Model model)
        {
            try
            {   
                string result = manager.insert_QLTN_NGUOI_KY_SO(model);

                return string.IsNullOrEmpty(result) ? Ok("Thành công") : BadRequest(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
