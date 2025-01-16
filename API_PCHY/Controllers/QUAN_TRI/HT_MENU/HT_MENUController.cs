using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using APIPCHY_PhanQuyen.Models.QLTN.HT_MENU;
using Microsoft.AspNetCore.Authorization;

namespace APIPCHY_PhanQuyen.Controllers.QLKC.HT_MENU
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    [Authorize]
    public class HT_MENUController : ControllerBase
    {
        HT_MENU_Manager manager = new HT_MENU_Manager();

        [Route("get_All_HT_MENU")]
        [HttpGet]
        public IActionResult get_All_HT_MENU()
        {
            try
            {
                List<HT_MENU_Model> result = manager.get_All_HT_MENU();
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("create_HT_MENU")]
        [HttpPost]
        public IActionResult create_HT_MENU([FromBody] HT_MENU_Model model)
        {
            try
            {
                string result = manager.create_HT_MENU(model);
                return string.IsNullOrEmpty(result) ? Ok("Menu created successfully.") : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("update_HT_MENU")]
        [HttpPut]
        public IActionResult update_HT_MENU([FromBody] HT_MENU_Model model)
        {
            try
            {
                string result = manager.update_HT_MENU(model);
                return string.IsNullOrEmpty(result) ? Ok("Menu updated successfully.") : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete_HT_MENU")]
        [HttpDelete]
        public IActionResult delete_HT_MENU(int id)
        {
            try
            {
                string result = manager.delete_HT_MENU(id);
                return string.IsNullOrEmpty(result) ? Ok("Menu deleted successfully.") : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
