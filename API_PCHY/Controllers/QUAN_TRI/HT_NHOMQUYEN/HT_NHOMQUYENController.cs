using APIPCHY_PhanQuyen.Models.QLKC.DM_DONVI;
using APIPCHY_PhanQuyen.Models.QLKC.HT_NHOMQUYEN;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using APIPCHY_PhanQuyen.Models.QLTN.HT_NHOMQUYEN;
using Microsoft.AspNetCore.Authorization;

namespace APIPCHY_PhanQuyen.Controllers.QLKC.HT_NHOMQUYEN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class HT_NHOMQUYENController : Controller
    {
        HT_NHOMQUYEN_Manager manager = new HT_NHOMQUYEN_Manager();

        [Route("create_HT_NHOMQUYEN")]
        [HttpPost]
        public IActionResult create_HT_NHOMQUYEN([FromBody] HT_NHOMQUYEN_Model model)
        {
            string result = manager.create_HT_NHOMQUYEN(model);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("update_HT_NHOMQUYEN")]
        [HttpPut]
        public IActionResult update_HT_NHOMQUYEN([FromBody] HT_NHOMQUYEN_Model model)
        {
            string result = manager.update_HT_NHOMQUYEN(model);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("delete_HT_NHOMQUYEN")]
        [HttpDelete]
        public IActionResult delete_HT_NHOMQUYEN(int id)
        {
            string result = manager.delete_HT_NHOMQUYEN(id);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("get_HT_NHOMQUYEN_By_ID")]
        [HttpGet]
        public IActionResult get_HT_NHOMQUYEN_By_ID(int id)
        {
            HT_NHOMQUYEN_Model result = manager.get_HT_NHOMQUYEN_By_ID(id);
            return result != null ? Ok(result) : NotFound();
        }


        [Route("search_HT_NHOMQUYEN")]
        [HttpPost]
        public IActionResult search_HT_NHOMQUYEN([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int? pageIndex = 0;
                int? pageSize = 0;
                string ten_nhom = null;
                string ma_dviqly = null;

                if (formData.Keys.Contains("pageIndex") && !string.IsNullOrEmpty(formData["pageIndex"].ToString()))
                {
                    pageIndex = int.Parse(formData["pageIndex"].ToString());
                }
                if (formData.Keys.Contains("pageSize") && !string.IsNullOrEmpty(formData["pageSize"].ToString()))
                {
                    pageSize = int.Parse(formData["pageSize"].ToString());
                }
                if (formData.Keys.Contains("ten_nhom") && !string.IsNullOrEmpty(formData["ten_nhom"].ToString()))
                {
                    ten_nhom = formData["ten_nhom"].ToString();
                }
                if (formData.Keys.Contains("ma_dviqly") && !string.IsNullOrEmpty(formData["ma_dviqly"].ToString()))
                {
                    ma_dviqly = formData["ma_dviqly"].ToString();
                }

                int totalItems = 0;
                List<HT_NHOMQUYEN_Model> result = manager.search_HT_NHOMQUYEN(pageSize, pageIndex, ten_nhom, ma_dviqly, out totalItems);
                return result != null ? Ok(new
                {
                    page = pageIndex,
                    pageSize = pageSize,
                    totalItems = totalItems,
                    data = result,

                }) : NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getNhomQuyen_byMaDV")]
        public ActionResult<List<HT_NHOMQUYEN_Model>> GetNhomQuyenByMaDVI(string maDviqly)
        {
            try
            {
                var nhomList = manager.GET_NHOMQUYEN_BY_MADV(maDviqly);
                if (nhomList == null || nhomList.Count == 0)
                {
                    return new List<HT_NHOMQUYEN_Model>();
                }
                return Ok(nhomList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}

