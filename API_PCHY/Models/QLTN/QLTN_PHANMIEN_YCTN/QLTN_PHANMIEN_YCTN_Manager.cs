using System;
using APIPCHY.Helpers;
using System.Data;
using System.Collections.Generic;

namespace API_PCHY.Models.QLTN.QLTN_PHANMIEN_YCTN
{
    public class QLTN_PHANMIEN_YCTN_Manager
    {
        DataHelper helper = new DataHelper();

        public bool insert_QLTN_PHANMIEN_YCTN(QLTN_PHANMIEN_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.insert_PHAN_MIEN_YCTN", "p_Error",
                                                    "p_MA_TRUONG_YCTN", "p_MA_LOAI_YCTN",
                                                    model.ma_truong_yctn, model.ma_loai_yctn);

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool delete_QLTN_PHANMIEN_YCTN(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.delete_PHAN_MIEN_YCTN_ByID", "p_Error",
                                                    "p_ID", id);

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<QLTN_PHANMIEN_YCTN_Model> get_QLTN_PHANMIEN_YCTN_byLOAI_YCTN(int id_loai_yctn)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_HUY.get_PHAN_MIEN_YCTN_byLOAI_YCTN",
                                                 "p_ID_LOAI_YCTN", id_loai_yctn);
                List<QLTN_PHANMIEN_YCTN_Model> list = new List<QLTN_PHANMIEN_YCTN_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        QLTN_PHANMIEN_YCTN_Model model = new QLTN_PHANMIEN_YCTN_Model();
                        model.id = ds.Rows[i]["ID"] != DBNull.Value ? int.Parse(ds.Rows[i]["ID"].ToString()) : 0;
                        model.ma_truong_yctn = ds.Rows[i]["MA_TRUONG_YCTN"] != DBNull.Value ? int.Parse(ds.Rows[i]["MA_TRUONG_YCTN"].ToString()) : 0;
                        model.ma_loai_yctn = ds.Rows[i]["MA_LOAI_YCTN"] != DBNull.Value ? int.Parse(ds.Rows[i]["MA_LOAI_YCTN"].ToString()) : 0;
                        model.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : DateTime.Now;

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
    }
}