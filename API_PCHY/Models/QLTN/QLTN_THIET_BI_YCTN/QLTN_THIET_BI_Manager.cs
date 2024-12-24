using APIPCHY.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace API_PCHY.Models.QLTN.QLTN_THIET_BI_YCTN
{
    public class QLTN_THIET_BI_Manager
    {
        DataHelper helper = new DataHelper();

        public List<QLTN_THIET_BI_Model> getALL_QLTN_THIET_BI_byMA_yctn(QLTN_THIET_BI_Model model_ip)
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_VINH.get_THIET_BI_YCTN_byMa_YCTN", "p_MA_YCTN",model_ip.ma_yctn);
                List<QLTN_THIET_BI_Model> result = new List<QLTN_THIET_BI_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        QLTN_THIET_BI_Model model = new QLTN_THIET_BI_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ma_tbtn = tb.Rows[i]["MA_TBTN"] != DBNull.Value ? tb.Rows[i]["MA_TBTN"].ToString() : null;
                        model.ma_yctn = tb.Rows[i]["MA_YCTN"] != DBNull.Value ? tb.Rows[i]["MA_YCTN"].ToString() : null;
                        model.ten_thiet_bi = tb.Rows[i]["TEN_THIET_BI"] != DBNull.Value ? tb.Rows[i]["TEN_THIET_BI"].ToString() : null;
                        model.ma_loai_tb = tb.Rows[i]["MA_LOAI_TB"] != DBNull.Value ? tb.Rows[i]["MA_LOAI_TB"].ToString() : null;
                        model.so_luong = tb.Rows[i]["SO_LUONG"] != DBNull.Value ? int.Parse(tb.Rows[i]["SO_LUONG"].ToString()) : null;
                        model.trang_thai = tb.Rows[i]["TRANG_THAI"] != DBNull.Value ? int.Parse(tb.Rows[i]["TRANG_THAI"].ToString()) : null;
                        model.ngay_tao = tb.Rows[i]["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(tb.Rows[i]["NGAY_TAO"]) : null;
                        model.nguoi_tao = tb.Rows[i]["NGUOI_TAO"] != DBNull.Value ? tb.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ten_loai_thiet_bi = tb.Rows[i]["TEN_LOAI_THIET_BI"] != DBNull.Value ? tb.Rows[i]["TEN_LOAI_THIET_BI"].ToString() : null;
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

        public string insert_QLTN_THIET_BI(QLTN_THIET_BI_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.create_thiet_bi_yctn", "p_Error",
                        "p_MA_TBTN", "p_MA_YCTN", "p_TEN_THIET_BI", "p_MA_LOAI_TB",
                        "p_SO_LUONG", "p_TRANG_THAI", "p_NGUOI_TAO", "p_TEN_LOAI_THIET_BI",
                        model.ma_tbtn, model.ma_yctn, model.ten_thiet_bi, model.ma_loai_tb,
                        model.so_luong, model.trang_thai,  model.nguoi_tao, model.ten_loai_thiet_bi
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Update_QLTN_THIET_BI(QLTN_THIET_BI_Model model)
        {
            try
            {
                string error = helper.ExcuteNonQuery("PKG_QLTN_VINH.update_QLTN_THIET_BI_YCTN", "p_Error",
                        "p_ID", "p_MA_TBTN", "p_MA_YCTN", "p_TEN_THIET_BI", "p_MA_LOAI_TB",
                        "p_SO_LUONG", "p_TRANG_THAI", "p_NGUOI_TAO", "p_TEN_LOAI_THIET_BI",
                        model.id, model.ma_tbtn, model.ma_yctn, model.ten_thiet_bi, model.ma_loai_tb,
                        model.so_luong, model.trang_thai, model.nguoi_tao, model.ten_loai_thiet_bi
                        );
                // Kiểm tra giá trị trả về từ p_Error
                if (!string.IsNullOrWhiteSpace(error))
                {
                    // Có lỗi xảy ra, trả về hoặc throw Exception
                    throw new Exception($"Lỗi từ procedure: {error}");
                }
                // Không có lỗi
                return "Cập nhật thành công!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Delete_QLTN_THIET_BI(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.delete_QLTN_THIETBI_YCTN", "p_Error",
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

        public List<QLTN_THIET_BI_Model> Search_QLTN_THIET_BI(QLTN_THIET_BI_Model modelip)
        {
            try
            {
                DataTable tb = helper.ExcuteReader(
                    "PKG_QLTN_VINH.search_QLTN_THIET_BI",
                    "p_MA_TBTN", "p_MA_YCTN", "p_TEN_THIET_BI", "p_MA_LOAI_TB",
                    "p_TRANG_THAI", "p_TEN_LOAI_THIET_BI",
                    modelip.ma_tbtn, modelip.ma_yctn, modelip.ten_thiet_bi, modelip.ma_loai_tb,
                    modelip.trang_thai, modelip.ten_loai_thiet_bi
                );

                List<QLTN_THIET_BI_Model> result = new List<QLTN_THIET_BI_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        QLTN_THIET_BI_Model model = new QLTN_THIET_BI_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ma_tbtn = tb.Rows[i]["MA_TBTN"] != DBNull.Value ? tb.Rows[i]["MA_TBTN"].ToString() : null;
                        model.ma_yctn = tb.Rows[i]["MA_YCTN"] != DBNull.Value ? tb.Rows[i]["MA_YCTN"].ToString() : null;
                        model.ten_thiet_bi = tb.Rows[i]["TEN_THIET_BI"] != DBNull.Value ? tb.Rows[i]["TEN_THIET_BI"].ToString() : null;
                        model.ma_loai_tb = tb.Rows[i]["MA_LOAI_TB"] != DBNull.Value ? tb.Rows[i]["MA_LOAI_TB"].ToString() : null;
                        //model.so_luong = tb.Rows[i]["SO_LUONG"] != DBNull.Value ? tb.Rows[i]["SO_LUONG"].ToString() : null;
                        //model.trang_thai = tb.Rows[i]["TRANG_THAI"] != DBNull.Value ? tb.Rows[i]["TRANG_THAI"].ToString() : null;
                        model.ngay_tao = tb.Rows[i]["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(tb.Rows[i]["NGAY_TAO"]) : null;
                        model.nguoi_tao = tb.Rows[i]["NGUOI_TAO"] != DBNull.Value ? tb.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ten_loai_thiet_bi = tb.Rows[i]["TEN_LOAI_THIET_BI"] != DBNull.Value ? tb.Rows[i]["TEN_LOAI_THIET_BI"].ToString() : null;
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
                throw new Exception($"Lỗi khi thực thi Search_QLTN_THIET_BI: {ex.Message}");
            }
        }

        public string Nhap_Khoi_Luong_Phat_Sinh(QLTN_THIET_BI_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_VINH.Nhap_Khoi_Luong_Phat_Sinh", "p_Error",
                        "p_MA_TBTN", "p_MA_YCTN", "p_TEN_THIET_BI", "p_MA_LOAI_TB",
                        "p_SO_LUONG", "p_TRANG_THAI", "p_NGUOI_TAO", "p_TEN_LOAI_THIET_BI",
                        model.ma_tbtn, model.ma_yctn, model.ten_thiet_bi, model.ma_loai_tb,
                        model.so_luong, model.trang_thai, model.nguoi_tao, model.ten_loai_thiet_bi
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