using APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG;
using APIPCHY_PhanQuyen.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using APIPCHY_PhanQuyen.Models.QLTN.HT_NGUOIDUNG;

namespace APIPCHY_PhanQuyen.Controllers.QLKC.HT_NGUOIDUNG
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class HT_NGUOIDUNGController : ControllerBase
    {
        private readonly HT_NGUOIDUNG_Manager _manager;
        private readonly TokenService _tokenService; // Thêm biến cho TokenService

        public HT_NGUOIDUNGController(IConfiguration configuration)
        {
            _manager = new HT_NGUOIDUNG_Manager(configuration);
            _tokenService = new TokenService(configuration); // Khởi tạo TokenService
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] HT_NGUOIDUNG_Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _manager.login_HT_NGUOIDUNG(model.ten_dang_nhap, model.mat_khau);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            //var token = _tokenService.GenerateJwtToken(user); // Đảm bảo bạn đã có hàm GenerateToken

            return Ok(new { user });
        }
        [HttpPost("get_ListNguoiDung")]
        public IActionResult GET_HT_NGUOIDUNG([FromBody] Dictionary<string, string> formData)
        {
            try
            {
                long total = 0;

                if (formData == null || !formData.ContainsKey("pageIndex") || !formData.ContainsKey("pageSize"))
                {
                    return BadRequest("Invalid request data. pageIndex and pageSize are required.");
                }
                if (!int.TryParse(formData["pageIndex"], out int pageIndex) ||
                    !int.TryParse(formData["pageSize"], out int pageSize))
                {
                    return BadRequest("Invalid pageIndex or pageSize format.");
                }

                var data = _manager.GET_HT_NGUOIDUNG(pageIndex, pageSize, out total);

                var result = new
                {
                    page = pageIndex,
                    Total = total,
                    PageSize = pageSize,
                    Data = data
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetNguoiDungById(string id)
        {
            try
            {
                var nguoiDung = _manager.GET_DM_NGUOIDUNG_byID(id);
                if (nguoiDung == null)
                {
                    return NotFound($"Không tìm thấy người dùng với ID: {id}");
                }
                return Ok(nguoiDung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


        [HttpPost("create")]
        public IActionResult PostInsertNguoiDung([FromBody] HT_NGUOIDUNG_Model nd)
        {
            var result = _manager.Insert_QLTN_NGUOI_DUNG(nd);

            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { message = "Thêm người dùng thanh công", data = nd });
            }
            return BadRequest(new { message = "Thêm người dùng thất bại hãy đổi lại tên tài khoản khác" });


        }

        [HttpPatch("update")]
        public void UpdateQuyen([FromBody] HT_NGUOIDUNG_Model nd)
        {
            _manager.Update_HT_NGUOIDUNG(nd);
        }


        [HttpPost("search")]
        public IActionResult FilterUsers([FromBody] UserFilterRequest request)
        {
            try
            {
                int totalRecords;
                List<UserResponse> users = _manager.FILTER_HT_NGUOIDUNG(request, out totalRecords);

                int totalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize);

                var result = new
                {
                    page = request.PageIndex,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    PageSize = request.PageSize,
                    data = users
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        //đổi mật khẩu 
        [HttpPost("resetPassword")]
        public IActionResult Reset_Password_HT_NGUOIDUNG([FromBody] Dictionary<string, string> request)
        {
            // Lấy các giá trị từ Dictionary
            if (!request.TryGetValue("ID", out string id) ||
                !request.TryGetValue("currentPassword", out string currentPassword) ||
                !request.TryGetValue("newPassword", out string newPassword))
            {
                return BadRequest(new { message = "Thiếu thông tin yêu cầu." });
            }

            // Gọi hàm reset password
            string resultMessage = _manager.Reset_Password_HT_NGUOIDUNG(id, currentPassword, newPassword);

            if (resultMessage == "Mật khẩu hiện tại không chính xác.")
            {
                return Unauthorized(new { message = resultMessage });
            }

            return Ok(new { message = resultMessage });
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete_HT_NGUOIDUNG(string id)
        {
            _manager.Delete_HT_NGUOIDUNG(id);
            return Ok("Xoa thanh cong");
        }

        [HttpPut("update_Trangthai/{id}/{trangthai}")]
        public IActionResult updateTrangThai(string id, int trangthai)
        {
            _manager.updateTrangThai_NguoiDung(id, trangthai);
            return Ok("Thành công");
        }

        [Route("get_HT_MENUByIdUser")]
        [HttpGet]
        public IActionResult get_HT_MENUByIdUser(string userId)
        {
            try
            {
                List<HT_MENU_Model> result = _manager.get_HT_MENU_ByIDUser(userId);
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
