using APIPCHY.Helpers;
using System.Collections.Generic;
using System.Data;
using System;
using APIPCHY_PhanQuyen.Models.QLKC.HT_PHANQUYEN;
namespace APIPCHY_PhanQuyen.Models.QLTN.HT_PHANQUYEN
{
    public class HT_PHANQUYEN_Manager
    {
        DataHelper helper = new DataHelper();
        public string insert_HT_PHANQUYEN(HT_PHANQUYEN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.insert_HT_PHANQUYEN", "p_Error",
                        "p_TIEU_DE", "p_GHI_CHU", "p_TT_KHOA", "p_NGUOI_KHOA", "p_TT_XOA",
                        "p_NGUOI_XOA", "p_NGAY_TAO", "p_NGUOI_TAO", "p_NGAY_SUA", "p_NGUOI_SUA",
                        "p_MENU_ID", "p_VIEW", "p_INSERT", "p_EDIT", "p_DELETE", "p_EXPORT",
                        "p_DONG_BO", "p_HARD_EDIT", "p_CHUYEN_BUOC", "p_MA_NHOM_TV", "p_MA_DVIQLY",
                        model.tieu_de, model.ghi_chu, model.tt_khoa, model.nguoi_khoa, model.tt_xoa,
                        model.nguoi_xoa, model.ngay_tao, model.nguoi_tao, model.ngay_sua, model.nguoi_sua,
                        model.menu_id, model.view, model.insert, model.edit, model.delete,
                        model.export, model.dong_bo, model.hard_edit, model.chuyen_buoc, model.ma_nhom_tv,
                        model.ma_dviqly
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<HT_PHANQUYEN_Model> get_HT_PHANQUYENByMA_NHOM_TV(int ma_nhom_tv)
        {
            try
            {
                DataTable result = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_HT_PHANQUYENByMA_NHOM_TV",
                       "p_MA_NHOM_TV",
                       ma_nhom_tv
                        );
                if (result != null)
                {
                    List<HT_PHANQUYEN_Model> list = new List<HT_PHANQUYEN_Model>();
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        HT_PHANQUYEN_Model model = new HT_PHANQUYEN_Model();
                        model.id = int.Parse(result.Rows[i]["ID"].ToString());
                        model.tieu_de = result.Rows[i]["TIEU_DE"] != DBNull.Value ? (result.Rows[i]["GHI_CHU"].ToString()) : null;
                        model.ghi_chu = result.Rows[i]["GHI_CHU"] != DBNull.Value ? (result.Rows[i]["GHI_CHU"].ToString()) : null;
                        model.tt_khoa = result.Rows[i]["TT_KHOA"] != DBNull.Value ? int.Parse(result.Rows[i]["TT_KHOA"].ToString()) : null;
                        model.nguoi_khoa = result.Rows[i]["NGUOI_KHOA"] != DBNull.Value ? (result.Rows[i]["NGUOI_TAO"].ToString()) : null;
                        model.tt_xoa = result.Rows[i]["TT_XOA"] != DBNull.Value ? int.Parse(result.Rows[i]["TT_XOA"].ToString()) : null;
                        model.nguoi_xoa = result.Rows[i]["NGUOI_XOA"] != DBNull.Value ? (result.Rows[i]["NGUOI_XOA"].ToString()) : null;
                        model.ngay_tao = result.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(result.Rows[i]["NGAY_TAO"].ToString()) : null;
                        model.nguoi_tao = result.Rows[i]["NGUOI_TAO"] != DBNull.Value ? (result.Rows[i]["NGUOI_TAO"].ToString()) : null;
                        model.ngay_sua = result.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(result.Rows[i]["NGAY_SUA"].ToString()) : null;
                        model.nguoi_sua = result.Rows[i]["NGUOI_SUA"] != DBNull.Value ? (result.Rows[i]["NGUOI_SUA"].ToString()) : null;
                        model.menu_id = result.Rows[i]["MENU_ID"] != DBNull.Value ? int.Parse(result.Rows[i]["MENU_ID"].ToString()) : null;
                        model.view = result.Rows[i]["VIEW"] != DBNull.Value ? int.Parse(result.Rows[i]["VIEW"].ToString()) : null;
                        model.insert = result.Rows[i]["INSERT"] != DBNull.Value ? int.Parse(result.Rows[i]["INSERT"].ToString()) : null;
                        model.edit = result.Rows[i]["EDIT"] != DBNull.Value ? int.Parse(result.Rows[i]["EDIT"].ToString()) : null;
                        model.delete = result.Rows[i]["DELETE"] != DBNull.Value ? int.Parse(result.Rows[i]["DELETE"].ToString()) : null;
                        model.export = result.Rows[i]["EXPORT"] != DBNull.Value ? int.Parse(result.Rows[i]["EXPORT"].ToString()) : null;
                        model.dong_bo = result.Rows[i]["DONG_BO"] != DBNull.Value ? int.Parse(result.Rows[i]["DONG_BO"].ToString()) : null;

                        model.hard_edit = result.Rows[i]["HARD_EDIT"] != DBNull.Value ? int.Parse(result.Rows[i]["HARD_EDIT"].ToString()) : null;
                        model.chuyen_buoc = result.Rows[i]["CHUYEN_BUOC"] != DBNull.Value ? int.Parse(result.Rows[i]["CHUYEN_BUOC"].ToString()) : null;
                        model.ma_nhom_tv = result.Rows[i]["MA_NHOM_TV"] != DBNull.Value ? int.Parse(result.Rows[i]["MA_NHOM_TV"].ToString()) : null;
                        model.ma_dviqly = result.Rows[i]["MA_DVIQLY"] != DBNull.Value ? (result.Rows[i]["MA_DVIQLY"].ToString()) : null;
                        list.Add(model);


                    }
                    return list;
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string delete_HT_PHANQUYEN(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.delete_HT_PHANQUYEN", "p_Error",
                       "p_ID",
                       id
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
