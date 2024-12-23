using System;
using APIPCHY.Helpers;
using System.Data;
using System.Collections.Generic;

namespace API_PCHY.Models.QLTN.DM_LOAI_YCTN
{
    public class DM_LOAI_YCTN_Manager
    {
        DataHelper helper = new DataHelper();

        public bool insert_DM_LOAI_YCTN(DM_LOAI_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.insert_DM_LOAI_YCTN", "p_Error",
                                                    "p_MA_LOAI_YCTN", "p_TEN_LOAI_YC", "p_KEY_WORD", "p_NGUOI_TAO",
                                                    model.ma_loai_yctn, model.ten_loai_yc, model.key_word, model.nguoi_tao);

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update_DM_LOAI_YCTN(DM_LOAI_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.update_DM_LOAI_YCTN", "p_Error",
                                                    "p_ID", "p_MA_LOAI_YCTN", "p_TEN_LOAI_YC", "p_KEY_WORD", "p_NGUOI_SUA",
                                                    model.id, model.ma_loai_yctn, model.ten_loai_yc, model.key_word, model.nguoi_sua);

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool delete_DM_LOAI_YCTN(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.delete_DM_LOAI_YCTN_ByID", "p_Error",
                                                    "p_ID", id);

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<DM_LOAI_YCTN_Model> get_All_DM_LOAI_YCTN()
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_HUY.get_DM_LOAI_YCTN");
                List<DM_LOAI_YCTN_Model> list = new List<DM_LOAI_YCTN_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        DM_LOAI_YCTN_Model model = new DM_LOAI_YCTN_Model();
                        model.id = ds.Rows[i]["ID"] != DBNull.Value ? int.Parse(ds.Rows[i]["ID"].ToString()) : null;
                        model.ma_loai_yctn = ds.Rows[i]["MA_LOAI_YCTN"] != DBNull.Value ? ds.Rows[i]["MA_LOAI_YCTN"].ToString() : null;
                        model.ten_loai_yc = ds.Rows[i]["TEN_LOAI_YC"] != DBNull.Value ? ds.Rows[i]["TEN_LOAI_YC"].ToString() : null;
                        model.key_word = ds.Rows[i]["KEY_WORD"] != DBNull.Value ? ds.Rows[i]["KEY_WORD"].ToString() : null;

                        list.Add(model);
                    }
                }
                
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DM_LOAI_YCTN_Model get_DM_LOAI_YCTN_ByMaLoai(string maLoai)
        {
            try
            {
                var list = get_All_DM_LOAI_YCTN();
                return list?.Find(x => x.ma_loai_yctn == maLoai);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
