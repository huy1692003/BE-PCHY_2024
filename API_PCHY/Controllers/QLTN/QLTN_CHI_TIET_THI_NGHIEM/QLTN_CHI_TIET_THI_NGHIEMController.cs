using Microsoft.AspNetCore.Mvc;
using API_PCHY.Models.QLTN.QLTN_CHI_TIET_THI_NGHIEM;
using System;
using API_PCHY.Models.QLTN.QLTN_THIET_BI_YCTN;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_PCHY.Controllers.QLTN.QLTN_CHI_TIET_THI_NGHIEM
{
    [Route("APIPCHY/[controller]")]
    [ApiController]
    public class QLTN_CHI_TIET_THI_NGHIEMController : ControllerBase
    {
        private readonly QLTN_CHI_TIET_THI_NGHIEM_Manager _manager;
        private readonly QLTN_THIET_BI_Manager _manager_tb;

        public QLTN_CHI_TIET_THI_NGHIEMController()
        {
            _manager = new QLTN_CHI_TIET_THI_NGHIEM_Manager();
            _manager_tb = new QLTN_THIET_BI_Manager();
        }

        [HttpGet]
        [Route("GetByMaTBTN")]
        public IActionResult GetByMaTBTN(string maTBTN, string maYCTN)
        {
            try
            {
                var result = _manager.get_QLTN_CHITIET_TN_by_MATBTN(maTBTN, maYCTN);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("deleteCTTN/{ma_CTTN}")]
        public IActionResult GetByMaTBTN(string ma_CTTN)
        {
            try
            {
                var result = _manager.deleteQLTN_CHITIET_TN_BYID(ma_CTTN);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }

        //Lấy danh sách thiết bị kèm chi tiết thí nghiệm tương ứng 
        [Route("getAll_TBTN_byMA_YCTN")]
        [HttpGet]
        public  IActionResult getbyMA_yctn(string ma_yctn)
        {
            try
            {
                List<QLTN_THIET_BI_Model> result =  _manager_tb.getALL_QLTN_THIET_BI_byMA_yctn(new QLTN_THIET_BI_Model { ma_yctn=ma_yctn});
                foreach (var tb in result)
                {
                    tb.listTN = _manager.get_QLTN_CHITIET_TN_by_MATBTN(tb.ma_tbtn, tb.ma_yctn);
                }    

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] QLTN_CHI_TIET_THI_NGHIEM_Model model)
        {
            try
            {
                var result = _manager.insert_QLTN_CHI_TIET_THI_NGHIEM(model);
                return result !=null ? Ok(result) : BadRequest("Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


    }
}
