using System;
using System.Threading.Tasks;
using API_PCHY.Models.QLTN.QLTN_KYSO;
using API_PCHY.Models.QLTN.QLTN_NGUOI_KY;
//using API_PCHY.Models.SMART_CA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PCHY.Controllers.QLTN.QLTN_KYSO
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_KYSOController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        // Tiêm IWebHostEnvironment vào constructor của controller
        public QLTN_KYSOController(IWebHostEnvironment env)
        {
            _env = env;
        }

        QLTN_KYSO_Manager manager = new QLTN_KYSO_Manager();

        [Route("search_document")]
        [HttpPost]
        public IActionResult get([FromQuery] Paginage paginage , [FromBody] SearchParamDocument model)
        {
            try
            {
                int totalRecord = 0;

                var result = manager.SEARCH_VANBAN(paginage,model, out totalRecord);

                return Ok(new { total = totalRecord, data = result });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        //[Route("create_sign")]
        //[HttpPost]
        //public async Task<IActionResult> create_sign()
        //{
           
        //    try
        //    {
        //        SmartCA769 helper=new SmartCA769();
        //        helper._signSmartCAPDF();
        //        return Ok("xong");

        //    }
        //    catch (Exception ex) { return BadRequest(ex.Message); }
        //}


        [Route("update_TrangThai_Ky")]
        [HttpPost]
        public IActionResult insert([FromBody] ReqUpdateKySo model)
        {
            try
            {
                var check = manager.update_TrangThai_Ky(model);

                return check? Ok("Kỹ hoàn tất") : BadRequest("Có lỗi xảy ra");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }



}
