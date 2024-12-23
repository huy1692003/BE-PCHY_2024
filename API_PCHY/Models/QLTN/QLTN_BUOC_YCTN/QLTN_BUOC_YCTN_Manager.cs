using APIPCHY.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace API_PCHY.Models.QLTN.QLTN_BUOC_YCTN
{
    public class QLTN_BUOC_YCTN_Manager
    {
        DataHelper helper = new DataHelper();

        public List<QLTN_BUOC_YCTN_Model> GetAll_QLTN_BUOC_YCTN()
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_VINH.getAll_QLTN_BUOC_YCTN");
                List<QLTN_BUOC_YCTN_Model> result = new List<QLTN_BUOC_YCTN_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        QLTN_BUOC_YCTN_Model model = new QLTN_BUOC_YCTN_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? int.Parse(tb.Rows[i]["ID"].ToString()) : 0;
                        model.ten_buoc_yctn = tb.Rows[i]["TEN_BUOC_YCTN"] != DBNull.Value ? tb.Rows[i]["TEN_BUOC_YCTN"].ToString() : null;
                        model.ngay_tao = tb.Rows[i]["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(tb.Rows[i]["NGAY_TAO"]) : null;
                        model.nguoi_tao = tb.Rows[i]["NGUOI_TAO"] != DBNull.Value ? tb.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ngay_sua = tb.Rows[i]["NGAY_SUA"] != DBNull.Value ? Convert.ToDateTime(tb.Rows[i]["NGAY_SUA"]) : null;
                        model.nguoi_sua = tb.Rows[i]["NGUOI_SUA"] != DBNull.Value ? tb.Rows[i]["NGUOI_SUA"].ToString() : null;
                        model.ghi_chu = tb.Rows[i]["GHI_CHU"] != DBNull.Value ? tb.Rows[i]["GHI_CHU"].ToString() : null;
                        model.buoc = tb.Rows[i]["BUOC"] != DBNull.Value ? int.Parse(tb.Rows[i]["BUOC"].ToString()) : null;

                        result.Add(model);
                    }
                }
                return result ?? new List<QLTN_BUOC_YCTN_Model>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách bước YCTN: {ex.Message}");
            }
        }

        public string Insert_QLTN_BUOC_YCTN(QLTN_BUOC_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.create_BUOC_YCTN", "p_Error",
                    "p_TEN_BUOC_YCTN", "p_NGUOI_TAO", "p_GHI_CHU","p_BUOC",
                    model.ten_buoc_yctn , model.nguoi_tao, model.ghi_chu ,model.buoc 
                );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm mới bước YCTN: {ex.Message}");
            }
        }

        public string Update_QLTN_BUOC_YCTN(QLTN_BUOC_YCTN_Model model)
        {
            try
            {
                string error = helper.ExcuteNonQuery("PKG_QLTN_VINH.update_BUOC_YCTN", "p_Error",
                    "p_ID", "p_TEN_BUOC_YCTN", "p_NGUOI_SUA", "p_GHI_CHU","p_BUOC",
                    model.id, model.ten_buoc_yctn, model.nguoi_sua, model.ghi_chu,model .buoc
                );

                if (!string.IsNullOrWhiteSpace(error))
                {
                    throw new Exception($"Lỗi từ procedure: {error}");
                }
                return "Cập nhật thành công!";
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật bước YCTN: {ex.Message}");
            }
        }

        public string Delete_QLTN_BUOC_YCTN(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.delete_BUOC_YCTN", "p_Error",
                    "p_ID", id
                );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa bước YCTN: {ex.Message}");
            }
        }

    }
}
