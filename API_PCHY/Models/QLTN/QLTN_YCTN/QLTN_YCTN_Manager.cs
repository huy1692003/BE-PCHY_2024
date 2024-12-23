using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.Json;
using API_PCHY.Models.QLTN.DM_LOAI_YCTN;
using API_PCHY.Models.QLTN.DM_LOAITHIETBI;
using API_PCHY.Models.QLTN.DM_TRUONG_YCTN;
using APIPCHY.Helpers;
using APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN;
using Oracle.ManagedDataAccess.Types;

namespace API_PCHY.Models.QLTN.QLTN_YCTN
{
    public class QLTN_YCTN_Manager
    {
        DataHelper helper = new DataHelper();

        public bool create_QLTN_YCTN(QLTN_YCTN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.create_QLTN_YCTN", "p_Error",
                                                    "p_MA_LOAI_YCTN", "p_MA_YCTN", "p_TEN_YCTN", "p_NGAY_TAO",
                                                    "p_ID_KHACH_HANG", "p_MA_KHACH_HANG", "p_LOAI_TAI_SAN", "p_NOI_DUNG",
                                                    "p_GTDT_TRUOC_THUE", "p_GTDT_THUE", "p_GTDT_SAU_THUE",
                                                    "p_GTDT_CHIET_GIAM", "p_GTDT_SAU_CHIET_GIAM", "p_NGAY_KY_HOP_DONG",
                                                    "p_NGAY_XAY_RA_SU_CO", "p_FILE_UPLOAD", "p_PHAN_TRAM_CHIET_GIAM", "p_PHAN_TRAM_THUE" ,"p_NGUOI_TAO",
                                                    model.ma_loai_yctn, model.ma_yctn, model.ten_yctn, model.ngay_tao,
                                                    model.id_khach_hang, model.ma_khach_hang, model.loai_tai_san, model.noi_dung,
                                                    model.gtdt_truoc_thue, model.gtdt_thue, model.gtdt_sau_thue,
                                                    model.gtdt_chiet_giam, model.gtdt_sau_chiet_giam, model.ngay_ky_hop_dong,
                                                    model.ngay_xay_ra_su_co, model.file_upload, model.phan_tram_chiet_giam ,model.phan_tram_thue , model.nguoi_tao
                                                    );

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool update_QLTN_YCTN(QLTN_YCTN_Model model)
        {
            string log = null;
            if(model.qltn_yctn_log!=null)
            {
                log= JsonSerializer.Serialize(model.qltn_yctn_log);
            }    
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.update_YCTN", "p_Error",
                                                        "p_MA_LOAI_YCTN", "p_MA_YCTN", "p_TEN_YCTN", "p_ID_KHACH_HANG",
                                                        "p_MA_KHACH_HANG", "p_LOAI_TAI_SAN", "p_NOI_DUNG", "p_GTDT_TRUOC_THUE",
                                                        "p_GTDT_THUE", "p_GTDT_SAU_THUE", "p_GTDT_CHIET_GIAM", "p_GTDT_SAU_CHIET_GIAM",
                                                        "p_NGAY_KY_HOP_DONG", "p_NGAY_XAY_RA_SU_CO", "p_FILE_UPLOAD", "p_PHAN_TRAM_CHIET_GIAM",
                                                        "p_PHAN_TRAM_THUE", "p_NGUOI_SUA", "p_YCTN_LOG",
                                                        model.ma_loai_yctn, model.ma_yctn, model.ten_yctn, model.id_khach_hang,
                                                        model.ma_khach_hang, model.loai_tai_san, model.noi_dung, model.gtdt_truoc_thue,
                                                        model.gtdt_thue, model.gtdt_sau_thue, model.gtdt_chiet_giam, model.gtdt_sau_chiet_giam,
                                                        model.ngay_ky_hop_dong, model.ngay_xay_ra_su_co, model.file_upload,
                                                        model.phan_tram_chiet_giam, model.phan_tram_thue, model.nguoi_sua,log);
                return string.IsNullOrEmpty(result); // Nếu không có lỗi, trả về true
            }
            catch (Exception)
            {
                return false; // Nếu có lỗi trong quá trình thực hiện, trả về false
            }
        }


        public bool delete_QLTN_YCTN(string maYCTN)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.delete_YCTN", "p_Error",
                                                        "p_MA_YCTN", maYCTN);
                return string.IsNullOrEmpty(result); // Nếu không có lỗi, trả về true
            }
            catch (Exception)
            {
                return false; // Nếu có lỗi trong quá trình thực hiện, trả về false
            }
        }


        public bool giao_nhiem_vu_YCTN(QLTN_YCTN_Model model)
        {
            string dv_thục_hien= JsonSerializer.Serialize(model.don_vi_thuc_hien);
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.giao_nhiem_vu_YCTN", "p_Error",
                                                    "p_MA_YCTN", "p_NGAY_GIAO_NV", "p_FILE_DINH_KEM_GIAO_NV",
                                                    "p_NGUOI_GIAO_NHIEM_VU", "p_DON_VI_THUC_HIEN",
                                                    model.ma_yctn, model.ngay_giao_nv, model.file_dinh_kem_giao_nv,
                                                    model.nguoi_giao_nhiem_vu, dv_thục_hien);

                return string.IsNullOrEmpty(result);
            }
            catch (Exception)
            {
                return false;
            }
        }



        //buoc 4: khao sat phuong an thi cong
        //  "ten_yctn": "YCTN.HD-9",
        //  "file_pa_thi_cong": "string",
        //  "nguoi_th_ks_lap_pa_thi_cong": "string",
        //  "ngay_ks_lap_pa_thi_cong": "2024-12-02T23:07:41.140Z",

        //}
        public bool khao_sat_phuong_an_YCTN(QLTN_YCTN_Model models)
        {
            try
            {
                string result = helper.ExcuteNonQuery(
                    "PKG_QLTN_TANH.khao_sat_phuong_an_YCTN",
                    "p_Error",
                    "p_MA_YCTN",
                    "p_FILE_PA_THI_CONG",
                    "p_NGUOI_TH_KS_LAP_PA_THI_CONG",
                    "p_NGAY_KS_LAP_PA_THI_CONG",
                    models.ma_yctn,
                    models.file_pa_thi_cong,
                    models.nguoi_th_ks_lap_pa_thi_cong,
                    models.ngay_ks_lap_pa_thi_cong
                );

                return string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public List<string> search_Ma_YCTN(string Ma_YCTN)
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_HUY.search_MaYCTN", "@p_MA_YCTN",Ma_YCTN);
                List<string> result = new List<string>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                       
                        string MA = tb.Rows[i]["MA_YCTN"] != DBNull.Value ? tb.Rows[i]["MA_YCTN"].ToString() : null;
                       
                        result.Add(MA);
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

        public QLTN_YCTN_Model get_QLTN_YCTN_ByID(string Ma_YCTN)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_HUY.getQLTN_YCTN_by_MA_YCTN", "p_MA_YCTN", Ma_YCTN);

                if (ds != null && ds.Rows.Count > 0)
                {
                    QLTN_YCTN_Model data = new QLTN_YCTN_Model();

                    data.id = ds.Rows[0]["ID"] != DBNull.Value ? int.Parse(ds.Rows[0]["ID"].ToString()) : null;
                    data.ten_yctn = ds.Rows[0]["TEN_YCTN"] != DBNull.Value ? ds.Rows[0]["TEN_YCTN"].ToString() : null;
                    data.ma_yctn = ds.Rows[0]["MA_YCTN"] != DBNull.Value ? ds.Rows[0]["MA_YCTN"].ToString() : null;
                    data.ma_loai_yctn = ds.Rows[0]["MA_LOAI_YCTN"] != DBNull.Value ? ds.Rows[0]["MA_LOAI_YCTN"].ToString() : null;
                    data.tong_gia_tri = ds.Rows[0]["TONG_GIA_TRI"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["TONG_GIA_TRI"].ToString()) : null;
                    data.id_khach_hang = ds.Rows[0]["ID_KHACH_HANG"] != DBNull.Value ? int.Parse(ds.Rows[0]["ID_KHACH_HANG"].ToString()) : null;
                    data.ma_khach_hang = ds.Rows[0]["MA_KHACH_HANG"] != DBNull.Value ? ds.Rows[0]["MA_KHACH_HANG"].ToString() : null;
                    data.loai_tai_san = ds.Rows[0]["LOAI_TAI_SAN"] != DBNull.Value ? int.Parse(ds.Rows[0]["LOAI_TAI_SAN"].ToString()) : null;
                    data.crr_step = ds.Rows[0]["CRR_STEP"] != DBNull.Value ? int.Parse(ds.Rows[0]["CRR_STEP"].ToString()) : null;
                    data.next_step = ds.Rows[0]["NEXT_STEP"] != DBNull.Value ? int.Parse(ds.Rows[0]["NEXT_STEP"].ToString()) : null;
                    data.nam_thuc_hien = ds.Rows[0]["NAM_THUC_HIEN"] != DBNull.Value ? int.Parse(ds.Rows[0]["NAM_THUC_HIEN"].ToString()) : null;
                    data.noi_dung = ds.Rows[0]["NOI_DUNG"] != DBNull.Value ? ds.Rows[0]["NOI_DUNG"].ToString() : null;
                    data.gtdt_truoc_thue = ds.Rows[0]["GTDT_TRUOC_THUE"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["GTDT_TRUOC_THUE"].ToString()) : null;
                    data.gtdt_thue = ds.Rows[0]["GTDT_THUE"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["GTDT_THUE"].ToString()) : null;
                    data.gtdt_sau_thue = ds.Rows[0]["GTDT_SAU_THUE"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["GTDT_SAU_THUE"].ToString()) : null;
                    data.gtdt_chiet_giam = ds.Rows[0]["GTDT_CHIET_GIAM"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["GTDT_CHIET_GIAM"].ToString()) : null;
                    data.gtdt_sau_chiet_giam = ds.Rows[0]["GTDT_SAU_CHIET_GIAM"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["GTDT_SAU_CHIET_GIAM"].ToString()) : null;
                    data.ngay_ky_hop_dong = ds.Rows[0]["NGAY_KY_HOP_DONG"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_KY_HOP_DONG"].ToString()) : null;
                    data.ngay_xay_ra_su_co = ds.Rows[0]["NGAY_XAY_RA_SU_CO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_XAY_RA_SU_CO"].ToString()) : null;
                    data.file_upload = ds.Rows[0]["FILE_UPLOAD"] != DBNull.Value ? ds.Rows[0]["FILE_UPLOAD"].ToString() : null;
                    data.ngay_giao_nv = ds.Rows[0]["NGAY_GIAO_NV"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_GIAO_NV"].ToString()) : null;
                    data.ngay_sua = ds.Rows[0]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_SUA"].ToString()) : null;
                    data.file_dinh_kem_giao_nv = ds.Rows[0]["FILE_DINH_KEM_GIAO_NV"] != DBNull.Value ? ds.Rows[0]["FILE_DINH_KEM_GIAO_NV"].ToString() : null;
                    data.nguoi_giao_nhiem_vu = ds.Rows[0]["NGUOI_GIAO_NHIEM_VU"] != DBNull.Value ? ds.Rows[0]["NGUOI_GIAO_NHIEM_VU"].ToString() : null;
                    data.file_pa_thi_cong = ds.Rows[0]["FILE_PA_THI_CONG"] != DBNull.Value ? ds.Rows[0]["FILE_PA_THI_CONG"].ToString() : null;
                    data.nguoi_th_ks_lap_pa_thi_cong = ds.Rows[0]["NGUOI_TH_KS_LAP_PA_THI_CONG"] != DBNull.Value ? ds.Rows[0]["NGUOI_TH_KS_LAP_PA_THI_CONG"].ToString() : null;
                    data.ngay_nhap_kl_thuc_hien = ds.Rows[0]["NGAY_NHAP_KL_THUC_HIEN"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_NHAP_KL_THUC_HIEN"].ToString()) : null;
                    data.nguoi_nhap_kl_thuc_hien = ds.Rows[0]["NGUOI_NHAP_KL_THUC_HIEN"] != DBNull.Value ? ds.Rows[0]["NGUOI_NHAP_KL_THUC_HIEN"].ToString() : null;
                    data.nguoi_ban_giao = ds.Rows[0]["NGUOI_BAN_GIAO"] != DBNull.Value ? ds.Rows[0]["NGUOI_BAN_GIAO"].ToString() : null;
                    data.nguoi_tao = ds.Rows[0]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[0]["NGUOI_TAO"].ToString() : null;
                    data.nguoi_sua = ds.Rows[0]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[0]["NGUOI_SUA"].ToString() : null;
                    data.don_vi_nhan_ban_giao = ds.Rows[0]["DON_VI_NHAN_BAN_GIAO"] != DBNull.Value ? ds.Rows[0]["DON_VI_NHAN_BAN_GIAO"].ToString() : null;
                    data.ngay_ban_giao = ds.Rows[0]["NGAY_BAN_GIAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_BAN_GIAO"].ToString()) : null;
                    data.ghi_chu_ban_giao = ds.Rows[0]["GHI_CHU_BAN_GIAO"] != DBNull.Value ? ds.Rows[0]["GHI_CHU_BAN_GIAO"].ToString() : null;
                    data.phan_tram_thue = ds.Rows[0]["PHAN_TRAM_THUE"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["PHAN_TRAM_THUE"].ToString()) : null;
                    data.phan_tram_chiet_giam = ds.Rows[0]["PHAN_TRAM_CHIET_GIAM"] != DBNull.Value ? decimal.Parse(ds.Rows[0]["PHAN_TRAM_CHIET_GIAM"].ToString()) : null;
                    data.ngay_ks_lap_pa_thi_cong = ds.Rows[0]["NGAY_KS_LAP_PA_THI_CONG"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_KS_LAP_PA_THI_CONG"].ToString()) : null;
                    data.ngay_tao = ds.Rows[0]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_TAO"].ToString()) : null;
                    data.don_vi_thuc_hien = ds.Rows[0]["DON_VI_THUC_HIEN"] != DBNull.Value ? JsonSerializer.Deserialize<List<string>>(ds.Rows[0]["DON_VI_THUC_HIEN"].ToString())     : null;
                    data.loai_yctn_model = data.ma_loai_yctn != null ? new DM_LOAI_YCTN_Manager().get_DM_LOAI_YCTN_ByMaLoai(data.ma_loai_yctn) : null;
                    if (ds.Rows[0]["QLTN_YCTN_LOG"] != DBNull.Value)
                    {
                        // Lấy giá trị NClob từ cột QLTN_YCTN_LOG
                         //Đọc dữ liệu từ NClob và chuyển nó thành chuỗi
                            string jsonString = ds.Rows[0]["QLTN_YCTN_LOG"].ToString();

                            // Sau đó tiến hành deserialize
                            if (!string.IsNullOrWhiteSpace(jsonString))
                            {
                                data.qltn_yctn_log = JsonSerializer.Deserialize<List<QLTN_YCTN_LOG_Model>>(jsonString);
                            }
                            else
                            {
                                data.qltn_yctn_log = null;
                            }
                        
                    }
                    else
                    {
                        data.qltn_yctn_log = null;
                    }

                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log exception tại đây nếu cần thiết
                return null;
            }
        }

    }
}
