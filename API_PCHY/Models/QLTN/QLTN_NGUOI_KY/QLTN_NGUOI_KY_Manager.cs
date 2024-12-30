using API_PCHY.Models.QLTN.QLTN_THIET_BI_YCTN;
using APIPCHY.Helpers;
using iTextSharp.text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace API_PCHY.Models.QLTN.QLTN_NGUOI_KY
{
    public class QLTN_NGUOI_KY_Manager
    {
        DataHelper helper = new DataHelper();
        public string insert_QLTN_NGUOI_KY_SO(QLTN_NGUOI_KY_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.insert_QLTN_NGUOI_KY", "p_Error",
                    "p_NHOM_NGUOI_KY", "p_MA_CHI_TIET_TN", "p_ID_NGUOI_KY", 
                    model.nhom_nguoi_ky, model.ma_chi_tiet_tn, model.id_nguoi_ky
                );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm mới: {ex.Message}");
            }
        }

       

        public List<QLTN_NGUOI_KY_Model>? getNguoiKyByMa_CTTN(string ma_CTTN)
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_HUY.getDanhSachNguoiKy_byCTTN", "P_MA_CTTN", ma_CTTN);
                List<QLTN_NGUOI_KY_Model> result = new List<QLTN_NGUOI_KY_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        QLTN_NGUOI_KY_Model model = new QLTN_NGUOI_KY_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? int.Parse(tb.Rows[i]["ID"].ToString()) : null;
                        model.trang_thai_ky = tb.Rows[i]["TRANG_THAI_KY"] != DBNull.Value ? int.Parse(tb.Rows[i]["TRANG_THAI_KY"].ToString()) : null;
                        model.nhom_nguoi_ky = tb.Rows[i]["NHOM_NGUOI_KY"] != DBNull.Value ? int.Parse(tb.Rows[i]["NHOM_NGUOI_KY"].ToString()) : null;
                        model.ly_do_tu_choi = tb.Rows[i]["LYDO_TUCHOI"] != DBNull.Value ? tb.Rows[i]["LY_DO_TU_CHOI"].ToString() : null;
                        model.ten_dang_nhap = tb.Rows[i]["TEN_DANG_NHAP"] != DBNull.Value ? tb.Rows[i]["TEN_DANG_NHAP"].ToString() : null;
                        model.ho_ten = tb.Rows[i]["HO_TEN"] != DBNull.Value ? tb.Rows[i]["HO_TEN"].ToString() : null;
                        model.ma_chi_tiet_tn = tb.Rows[i]["MA_CHI_TIET_TN"] != DBNull.Value ? tb.Rows[i]["MA_CHI_TIET_TN"].ToString() : null;
                        model.id_nguoi_ky = tb.Rows[i]["ID_NGUOI_KY"] != DBNull.Value ? tb.Rows[i]["ID_NGUOI_KY"].ToString() : null;
                        model.thoi_gian_ky = tb.Rows[i]["THOI_GIAN_KY"] != DBNull.Value ? Convert.ToDateTime(tb.Rows[i]["THOI_GIAN_KY"]) : null;
                        model.ngay_tu_choi = tb.Rows[i]["NGAY_TU_CHOI"] != DBNull.Value ? Convert.ToDateTime(tb.Rows[i]["NGAY_TU_CHOI"]) : null;
                        result.Add(model);
                    }
                }
                else
                {
                    result = null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
