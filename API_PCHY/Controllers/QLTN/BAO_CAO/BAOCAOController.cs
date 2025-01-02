using API_PCHY.Models.QLTN.BAO_CAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PCHY.Controllers.QLTN.BAO_CAO
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class BAOCAOController : ControllerBase
    {
        private SL_QLTN_THEOKHACHHANG_Manager sl_qltn_theokhach_manager = new SL_QLTN_THEOKHACHHANG_Manager();
        private SL_QLTN_THEO_DONVI_Manager _Manager = new SL_QLTN_THEO_DONVI_Manager();
        public class DateRangeRequest
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
        [HttpPost]
        [Route("sl_qltn_theoKH")]
        public IActionResult get_sl_qltn_theoKH([FromBody] DateRangeRequest dateRange)
        {
            try
            {
                var result = sl_qltn_theokhach_manager.getall(dateRange.StartDate, dateRange.EndDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("sl_qltn_theo_DONVITHUCHIEN")]
        public IActionResult get_sl_qltn_theo_DonVi([FromBody] DateRangeRequest dateRange)
        {
            try
            {
                var result = _Manager.getall(dateRange.StartDate, dateRange.EndDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
