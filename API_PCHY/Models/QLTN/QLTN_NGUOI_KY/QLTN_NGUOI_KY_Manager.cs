using APIPCHY.Helpers;
using System;

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
    }
}
