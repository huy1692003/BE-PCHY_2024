using APIPCHY.Helpers;
using System.Collections.Generic;
using System;
using System.Data;
using API_PCHY.Models.QLTN.DM_LOAITHIETBI;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN;
using APIPCHY_PhanQuyen.Models.QLKC.HT_PHANQUYEN;
using System.Linq;

namespace API_PCHY.Models.QLTN.DM_LOAITHIETBI
{
    public class DM_LOAITHIETBI_Manager
    {
        DataHelper helper = new DataHelper();
        public  List<DM_LOAITHIETBI_Model> getALL_DM_LOAITHIETBI()
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_VINH.get_DM_LOAI_THIET_BI");
                List<DM_LOAITHIETBI_Model> result = new List<DM_LOAITHIETBI_Model>();
                if(tb != null)
                {
                    for(int i = 0; i < tb.Rows.Count; i++)
                    {
                        DM_LOAITHIETBI_Model model = new DM_LOAITHIETBI_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ten_loai_tb = tb.Rows[i]["TEN_LOAI_TB"] != DBNull.Value ? tb.Rows[i]["TEN_LOAI_TB"].ToString() : null;
                        model.ngay_tao = tb.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(tb.Rows[i]["NGAY_TAO"].ToString()) : null;
                        model.nguoi_tao = tb.Rows[i]["NGUOI_TAO"] != DBNull.Value ? tb.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ngay_sua = tb.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(tb.Rows[i]["NGAY_SUA"].ToString()) : null;
                        model.nguoi_sua = tb.Rows[i]["NGUOI_SUA"] != DBNull.Value ? tb.Rows[i]["NGUOI_SUA"].ToString() : null;
                        model.ghi_chu = tb.Rows[i]["GHI_CHU"] != DBNull.Value ? tb.Rows[i]["GHI_CHU"].ToString() : null;
                        model.ma_loai_tb = tb.Rows[i]["MA_LOAI_TB"] != DBNull.Value ? tb.Rows[i]["MA_LOAI_TB"].ToString() : null;

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

        public string insert_DM_LOAITHIETBI(DM_LOAITHIETBI_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.insert_DM_LOAI_THIET_BI", "p_Error",
                        "p_TEN_LOAI_TB", "p_MA_LOAI_TB", "p_NGUOI_TAO", "p_GHI_CHU",
                        model.ten_loai_tb, model.ma_loai_tb, model.nguoi_tao, model.ghi_chu
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Update_DM_LOAITHIETBI(DM_LOAITHIETBI_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.update_DM_LOAI_THIET_BI", "p_Error",
                        "p_ID", "p_TEN_LOAI_TB", "p_MA_LOAI_TB", "p_NGUOI_SUA", "p_GHI_CHU",
                        model.id, model.ten_loai_tb, model.ma_loai_tb, model.nguoi_sua,model.ghi_chu
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Delete_DM_LOAITHIETBI(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.delete_DM_LOAI_THIET_BI_ByID", "p_Error",
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

        public List<DM_LOAITHIETBI_Model> Search_DM_LOAITHIETBI(DM_LOAITHIETBI_Model modelip)
        {
            try
            {
                // Gọi stored procedure
                DataTable tb = helper.ExcuteReader(
                    "PKG_QLTN_VINH.search_DM_LOAI_THIET_BI", // Tên stored procedure
                    "p_TEN_LOAI_TB" , "p_MA_LOAI_TB", "p_NGAY_TAO", "p_NGUOI_TAO", "p_NGAY_SUA", "p_NGUOI_SUA",
                    modelip.ten_loai_tb,modelip.ma_loai_tb,modelip.ngay_tao,modelip.nguoi_tao,modelip.ngay_sua,modelip.nguoi_sua
                );

                List<DM_LOAITHIETBI_Model> result = new List<DM_LOAITHIETBI_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        DM_LOAITHIETBI_Model model = new DM_LOAITHIETBI_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ten_loai_tb = tb.Rows[i]["TEN_LOAI_TB"] != DBNull.Value ? tb.Rows[i]["TEN_LOAI_TB"].ToString() : null;
                        model.ngay_tao = tb.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse( tb.Rows[i]["NGAY_TAO"].ToString()) : null;
                        model.nguoi_tao = tb.Rows[i]["NGUOI_TAO"] != DBNull.Value ? tb.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ngay_sua = tb.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(tb.Rows[i]["NGAY_SUA"].ToString()) : null;
                        model.nguoi_sua = tb.Rows[i]["NGUOI_SUA"] != DBNull.Value ? tb.Rows[i]["NGUOI_SUA"].ToString() : null;
                        model.ghi_chu = tb.Rows[i]["GHI_CHU"] != DBNull.Value ? tb.Rows[i]["GHI_CHU"].ToString() : null;
                        model.ma_loai_tb = tb.Rows[i]["MA_LOAI_TB"] != DBNull.Value ? tb.Rows[i]["MA_LOAI_TB"].ToString() : null;
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
                throw new Exception($"Lỗi khi thực thi Search_DM_LOAITHIETBI: {ex.Message}");
            }
        }


    }
}
