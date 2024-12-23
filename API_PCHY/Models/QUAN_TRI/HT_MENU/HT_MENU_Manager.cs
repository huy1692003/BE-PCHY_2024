using APIPCHY.Helpers;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using System;
using System.Collections.Generic;
using System.Data;

namespace APIPCHY_PhanQuyen.Models.QLTN.HT_MENU
{
    public class HT_MENU_Manager
    {
        DataHelper helper = new DataHelper();

        public List<HT_MENU_Model> get_All_HT_MENU()
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_All_HT_MENU");
                List<HT_MENU_Model> listMenu = new List<HT_MENU_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        HT_MENU_Model model = new HT_MENU_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? int.Parse(tb.Rows[i]["ID"].ToString()) : null;
                        model.ten_menu = tb.Rows[i]["TEN_MENU"] != DBNull.Value ? tb.Rows[i]["TEN_MENU"].ToString() : null;
                        model.ghi_chu = tb.Rows[i]["GHI_CHU"] != DBNull.Value ? tb.Rows[i]["GHI_CHU"].ToString() : null;
                        model.ngay_tao = tb.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(tb.Rows[i]["NGAY_TAO"].ToString()) : null;
                        model.nguoi_tao = tb.Rows[i]["NGUOI_TAO"] != DBNull.Value ? tb.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ngay_sua = tb.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(tb.Rows[i]["NGAY_SUA"].ToString()) : null;
                        model.nguoi_sua = tb.Rows[i]["NGUOI_SUA"] != DBNull.Value ? tb.Rows[i]["NGUOI_SUA"].ToString() : null;
                        model.duong_dan = tb.Rows[i]["DUONG_DAN"] != DBNull.Value ? tb.Rows[i]["DUONG_DAN"].ToString() : null;
                        model.parent_id = tb.Rows[i]["PARENT_ID"] != DBNull.Value ? int.Parse(tb.Rows[i]["PARENT_ID"].ToString()) : null;
                        model.icon = tb.Rows[i]["ICON"] != DBNull.Value ? tb.Rows[i]["ICON"].ToString() : null;
                        model.sap_xep = tb.Rows[i]["SAP_XEP"] != DBNull.Value ? int.Parse(tb.Rows[i]["SAP_XEP"].ToString()) : null;
                        listMenu.Add(model);
                    }
                }

                else listMenu = null;
                return listMenu;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Phương thức tạo mới HT_MENU
        public string create_HT_MENU(HT_MENU_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery(
                    "PKG_QLTN_QUANTRI.insert_HT_MENU",
                    "p_Error",
                    "p_ID", "p_TEN_MENU", "p_GHI_CHU", "p_NGAY_TAO", "p_NGUOI_TAO",
                    "p_NGAY_SUA", "p_NGUOI_SUA", "p_DUONG_DAN", "p_PARENT_ID", "p_ICON", "p_SAP_XEP",
                    model.id, model.ten_menu, model.ghi_chu, model.ngay_tao, model.nguoi_tao,
                    model.ngay_sua, model.nguoi_sua, model.duong_dan, model.parent_id, model.icon, model.sap_xep
                );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Phương thức cập nhật HT_MENU
        public string update_HT_MENU(HT_MENU_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery(
                    "PKG_QLTN_QUANTRI.update_HT_MENU",
                    "p_Error",
                    "p_ID", "p_TEN_MENU", "p_GHI_CHU", "p_NGAY_TAO", "p_NGUOI_TAO",
                    "p_NGAY_SUA", "p_NGUOI_SUA", "p_DUONG_DAN", "p_PARENT_ID", "p_ICON", "p_SAP_XEP",
                    model.id, model.ten_menu, model.ghi_chu, model.ngay_tao, model.nguoi_tao,
                    model.ngay_sua, model.nguoi_sua, model.duong_dan, model.parent_id, model.icon, model.sap_xep
                );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Phương thức xóa HT_MENU
        public string delete_HT_MENU(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.delete_HT_MENU", "p_Error", "p_ID", id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
