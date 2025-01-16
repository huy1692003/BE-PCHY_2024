using APIPCHY_PhanQuyen.Models.QLKC.DM_DONVI;
using APIPCHY_PhanQuyen.Models.QLTN.DM_DONVI;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIPCHY_PhanQuyen.Controllers.QLKC.DM_DONVI
{

    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class DM_DONVIController : ControllerBase
    {
        DM_DONVI_Manager db = new DM_DONVI_Manager();

        [Route("insert_DM_DONVI")]
        [HttpPost]
        public IActionResult insert_DM_DONVI([FromBody] DM_DONVI_Model model)
        {
            string result = db.insert_DM_DONVI(model);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("update_DM_DONVI")]
        [HttpPut]
        public IActionResult update_DM_DONVI([FromBody] DM_DONVI_Model model)
        {
            string result = db.update_DM_DONVI(model);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("delete_DM_DONVI")]
        [HttpDelete]
        public IActionResult delete_DM_DONVI(Guid id)
        {
            string result = db.delete_DM_DONVI(id.ToString());
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("get_DM_DONVI_ByID")]
        [HttpGet]
        public IActionResult get_DM_DONVI_ByID(Guid id)
        {
            DM_DONVI_Model result = db.get_DM_DONVI_ByID(id.ToString());
            return result != null ? Ok(result) : NotFound();
        }

        [Route("get_All_DM_DONVI")]
        [HttpGet]
        public IActionResult get_All_DM_DONVI(string ma_dviqly)
        {
            List<DM_DONVI_Model> result = db.get_All_DM_DONVI(ma_dviqly);
            return result != null ? Ok(result) : NotFound();
        }

        [Route("search_DM_DONVI")]
        [HttpPost]
        public IActionResult search_DM_DONVI([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int? pageIndex = 1;
                int? pageSize = 5;
                string ten = null;
                string ma = null;
                int? trang_thai = -1;
                string ma_dviqly = null;
                if (formData.Keys.Contains("pageIndex") && !string.IsNullOrEmpty(formData["pageIndex"].ToString()))
                {
                    pageIndex = int.Parse(formData["pageIndex"].ToString());
                }
                if (formData.Keys.Contains("pageSize") && !string.IsNullOrEmpty(formData["pageSize"].ToString()))
                {
                    pageSize = int.Parse(formData["pageSize"].ToString());
                }
                if (formData.Keys.Contains("ten") && !string.IsNullOrEmpty(formData["ten"].ToString()))
                {
                    ten = formData["ten"].ToString();
                }
                if (formData.Keys.Contains("ma") && !string.IsNullOrEmpty(formData["ma"].ToString()))
                {
                    ma = formData["ma"].ToString();
                }
                if (formData.Keys.Contains("trang_thai") && !string.IsNullOrEmpty(formData["trang_thai"].ToString()))
                {
                    trang_thai = int.Parse(formData["trang_thai"].ToString());
                }
                if (formData.Keys.Contains("ma_dviqly") && !string.IsNullOrEmpty(formData["ma_dviqly"].ToString()))
                {
                    ma_dviqly = formData["ma_dviqly"].ToString();
                }

                int totalItems = 0;
                List<DM_DONVI_Model> result = db.search_DM_DONVI(pageIndex, pageSize, ten, ma, trang_thai, ma_dviqly, out totalItems);
                // Nếu không có kết quả, trả về mảng trống
                if (result == null || result.Count == 0)
                {
                    return Ok(new
                    {
                        page = pageIndex,
                        pageSize = pageSize,
                        totalItems = 0,
                        data = new List<DM_DONVI_Model>(), // Trả về mảng trống

                    });
                }

                // Nếu có kết quả, trả về dữ liệu như bình thường
                return Ok(new
                {
                    page = pageIndex,
                    pageSize = pageSize,
                    totalItems = totalItems,
                    data = result,

                });

            }

            //new
            catch (Exception ex)
            {
                return Ok(new
                {
                    page = 0,
                    pageSize = 0,
                    totalItems = 0,
                    data = new List<DM_DONVI_Model>(), // Trả về mảng trống

                });
            }
        }
    }
}
