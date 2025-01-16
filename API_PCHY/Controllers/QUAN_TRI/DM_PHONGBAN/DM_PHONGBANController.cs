using APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN;
using APIPCHY_PhanQuyen.Models.QLTN.DM_PHONGBAN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIPCHY_PhanQuyen.Controllers.QLKC.DM_PHONGBAN
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class DM_PHONGBANController : ControllerBase
    {
        DM_PHONGBAN_Manager db = new DM_PHONGBAN_Manager();

        [Route("insert_DM_PHONGBAN")]
        [HttpPost]
        public IActionResult insert_DM_PHONGBAN([FromBody] DM_PHONGBAN_Model dM_PHONGBAN)
        {
            string result = db.insert_DM_PHONGBAN(dM_PHONGBAN);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }
        [Route("update_DM_PHONGBAN")]
        [HttpPut]
        public IActionResult update_DM_PHONGBAN([FromBody] DM_PHONGBAN_Model dM_PHONGBAN)
        {
            string result = db.update_DM_PHONGBAN(dM_PHONGBAN);
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }

        [Route("delete_DM_PHONGBAN")]
        [HttpDelete]
        public IActionResult delete_DM_PHONGBAN(Guid ID)
        {
            string result = db.delete_DM_PHONGBAN(ID.ToString());
            return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
        }
        [Route("get_DM_PHONGBANByID")]
        [HttpGet]
        public IActionResult get_DM_PHONGBANByID(Guid ID)
        {
            DM_PHONGBAN_Model result = db.get_DM_PHONGBANByID(ID.ToString());
            return result != null ? Ok(result) : NotFound();
        }
        [Route("search_DM_PHONGBAN")]
        [HttpPost]
        public IActionResult search_DM_PHONGBANByID([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int? pageIndex = 0;
                int? pageSize = 0;
                string ten = null;
                string ma = null;
                int? trang_thai = null;
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
                    ma_dviqly = (formData["ma_dviqly"].ToString());
                }

                int totalItems = 0;
                List<DM_PHONGBAN_Model> result = db.search_DM_PHONGBANByID(pageIndex, pageSize, ten, ma, trang_thai, ma_dviqly, out totalItems);
                return result != null ? Ok(new
                {
                    page = pageIndex,
                    pageSize = pageSize,
                    totalItems = totalItems,
                    data = result,
                    ten = ten,
                    ma = ma,
                    ma_dviqly = ma_dviqly,
                    trang_thai = trang_thai
                }) : NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
