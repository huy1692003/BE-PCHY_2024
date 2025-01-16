using API_PCHY.Models.QUAN_TRI.DM_CHUCVU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using API_PCHY.Models.QUAN_TRI.DM_CHUC_VU;
using Microsoft.AspNetCore.Authorization;

namespace API_PCHY.Controllers.QUAN_TRI.DM_CHUCVU
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class DM_CHUCVUController : ControllerBase
    {
        DM_CHUCVU_Manager manager = new DM_CHUCVU_Manager();
        [HttpGet("getAll_DM_CHUCVU")]
        public IActionResult Get()
        {
            try
            {
                List<DM_CHUCVU_Model> result = manager.getAll_DM_CHUCVU();
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
