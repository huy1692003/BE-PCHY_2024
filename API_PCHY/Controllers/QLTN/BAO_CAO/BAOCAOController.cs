using API_PCHY.Models.QLTN.BAO_CAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PCHY.Controllers.QLTN.BAO_CAO
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class BAOCAOController : ControllerBase
    {
        private readonly BAO_CAO_Manager _baoCaoManager;

        public BAOCAOController()
        {
            _baoCaoManager = new BAO_CAO_Manager();
        }

        public class DateRangeRequest
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        /// <summary>
        /// Báo cáo số lượng yêu cầu thí nghiệm theo khách hàng.
        /// </summary>
        [HttpPost]
        [Route("sl_qltn_theoKH")]
        public IActionResult GetSlQltnTheoKh([FromBody] DateRangeRequest dateRange)
        {
            try
            {
                var result = _baoCaoManager.get_SL_QLTN_THEOKHACHHANG(dateRange.StartDate, dateRange.EndDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Báo cáo số lượng yêu cầu thí nghiệm theo đơn vị thực hiện.
        /// </summary>
        [HttpPost]
        [Route("sl_qltn_theo_DONVITHUCHIEN")]
        public IActionResult GetSlQltnTheoDonVi([FromBody] DateRangeRequest dateRange)
        {
            try
            {
                var result = _baoCaoManager.get_SOLUONG_YCTN(dateRange.StartDate, dateRange.EndDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Thống kê chữ ký số theo đơn vị.
        /// </summary>
        [HttpPost]
        [Route("thongke_chukyso")]
        public IActionResult ThongKeChuKySo([FromBody] DateRangeRequest dateRange)
        {
            try
            {
                var result = _baoCaoManager.thongke_chukyso(dateRange.StartDate, dateRange.EndDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy dữ liệu dashboard.
        /// </summary>
        [HttpGet]
        [Route("getDashboard")]
        public IActionResult GetDashboard([FromQuery] string userID)
        {
            try
            {
                var result = _baoCaoManager.getDashboard(userID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
