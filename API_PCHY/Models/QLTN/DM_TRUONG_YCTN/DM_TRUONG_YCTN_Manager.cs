using APIPCHY.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace API_PCHY.Models.QLTN.DM_TRUONG_YCTN
{
    public class DM_TRUONG_YCTN_Manager
    {
        DataHelper helper = new DataHelper();

        public List<DM_TRUONG_YCTN_Model> getALL_DM_TRUONG_YCTN()
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_VINH.get_DM_TRUONG_YCTN");
                List<DM_TRUONG_YCTN_Model> result = new List<DM_TRUONG_YCTN_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        DM_TRUONG_YCTN_Model model = new DM_TRUONG_YCTN_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ten_truong = tb.Rows[i]["TEN_TRUONG"] != DBNull.Value ? tb.Rows[i]["TEN_TRUONG"].ToString() : null;
                        model.vi_tri = tb.Rows[i]["VI_TRI"] != DBNull.Value ? tb.Rows[i]["VI_TRI"].ToString() : null;
                        model.ma_code = tb.Rows[i]["MA_CODE"] != DBNull.Value ? tb.Rows[i]["MA_CODE"].ToString() : null;
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

        public string insert_DM_TRUONG_YCTN(DM_TRUONG_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.insert_DM_TRUONG_YCTN", "p_Error",
                        "p_TEN_TRUONG", "p_VI_TRI", "p_MA_CODE",
                        model.ten_truong, model.vi_tri, model.ma_code
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Update_DM_TRUONG_YCTN(DM_TRUONG_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.UPDATE_DM_TRUONG_YCTN", "p_Error",
                        "p_ID", "p_TEN_TRUONG", "p_VI_TRI", "p_MA_CODE",
                        model.id, model.ten_truong, model.vi_tri, model.ma_code
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Delete_DM_TRUONG_YCTN(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.delete_DM_TRUONG_YCTN", "p_Error",
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

        public List<DM_TRUONG_YCTN_Model> Search_DM_TRUONG_YCTN(DM_TRUONG_YCTN_Model modelip)
        {
            try
            {
                DataTable tb = helper.ExcuteReader(
                    "PKG_QLTN_VINH.search_DM_TRUONG_YCTN",
                    "p_TEN_TRUONG", "p_VI_TRI", "p_MA_CODE",
                    modelip.ten_truong, modelip.vi_tri, modelip.ma_code
                );

                List<DM_TRUONG_YCTN_Model> result = new List<DM_TRUONG_YCTN_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        DM_TRUONG_YCTN_Model model = new DM_TRUONG_YCTN_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ten_truong = tb.Rows[i]["TEN_TRUONG"] != DBNull.Value ? tb.Rows[i]["TEN_TRUONG"].ToString() : null;
                        model.vi_tri = tb.Rows[i]["VI_TRI"] != DBNull.Value ? tb.Rows[i]["VI_TRI"].ToString() : null;
                        model.ma_code = tb.Rows[i]["MA_CODE"] != DBNull.Value ? tb.Rows[i]["MA_CODE"].ToString() : null;
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
                throw new Exception($"Lỗi khi thực thi Search_DM_TRUONG_YCTN: {ex.Message}");
            }
        }
    }
}