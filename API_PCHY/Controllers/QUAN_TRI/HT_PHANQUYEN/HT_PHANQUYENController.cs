using APIPCHY_PhanQuyen.Models.QLKC.HT_PHANQUYEN;
using APIPCHY_PhanQuyen.Models.QLTN.HT_PHANQUYEN;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APIPCHY_PhanQuyen.Controllers.QLKC.HT_PHANQUYEN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class HT_PHANQUYENController : Controller
    {
        HT_PHANQUYEN_Manager manager = new HT_PHANQUYEN_Manager();
        [Route("insert_HT_PHANQUYEN")]
        [HttpPost]
        public IActionResult a([FromBody] HT_PHANQUYEN_Model model)
        {
            try
            {
                string result = manager.insert_HT_PHANQUYEN(model);
                return !string.IsNullOrEmpty(result) ? BadRequest(result) : Ok("Thêm thành công");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("get_HT_PHANQUYENByMA_NHOM_TV")]
        [HttpGet]
        public IActionResult get_HT_PHANQUYENByMA_NHOM_TV(int ma_nhomtv)
        {
            try
            {
                List<HT_PHANQUYEN_Model> result = manager.get_HT_PHANQUYENByMA_NHOM_TV(ma_nhomtv);
                return result == null ? NotFound("Không có bản ghi nào") : Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete_HT_PHANQUYEN")]
        [HttpDelete]
        public IActionResult delete_HT_PHANQUYEN(int id)
        {
            try
            {
                string result = manager.delete_HT_PHANQUYEN(id);
                return !string.IsNullOrEmpty(result) ? BadRequest(result) : Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
