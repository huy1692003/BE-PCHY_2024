using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using APIPCHY.Helpers;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace API_PCHY.Models.QLTN.QLTN_CHI_TIET_THI_NGHIEM
{
    public class QLTN_CHI_TIET_THI_NGHIEM_Manager
    {
        DataHelper helper = new DataHelper();

        public QLTN_CHI_TIET_THI_NGHIEM_Model? insert_QLTN_CHI_TIET_THI_NGHIEM(QLTN_CHI_TIET_THI_NGHIEM_Model model)
        {
            model.ma_chi_tiet_tn = Guid.NewGuid().ToString();
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_HUY.insert_QLTN_CHI_TIET_THI_NGHIEM",
                                                    "p_MA_CHI_TIET_TN", "p_MA_TBTN", "p_SO_LUONG",
                                                    "p_MA_LOAI_BB", "p_MA_YCTN", "p_FILE_UPLOAD",
                                                    "p_NGAY_TT_TN", "p_NGAY_TAO", "p_NGUOI_TAO",
                                                    "p_NGAY_SUA", "p_NGUOI_SUA", "p_LANTHU",
                                                    model.ma_chi_tiet_tn, model.ma_tbtn, model.so_luong,
                                                    model.ma_loai_bb, model.ma_yctn, model.file_upload,
                                                    model.ngay_tt_tn, model.ngay_tao, model.nguoi_tao,
                                                    model.ngay_sua, model.nguoi_sua, model.lanthu);

                List<QLTN_CHI_TIET_THI_NGHIEM_Model> list = new List<QLTN_CHI_TIET_THI_NGHIEM_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        QLTN_CHI_TIET_THI_NGHIEM_Model entity = new QLTN_CHI_TIET_THI_NGHIEM_Model();
                        entity.id = ds.Rows[i]["ID"] != DBNull.Value ? int.Parse(ds.Rows[i]["ID"].ToString()) : null;
                        entity.ma_chi_tiet_tn = ds.Rows[i]["MA_CHI_TIET_TN"] != DBNull.Value ? ds.Rows[i]["MA_CHI_TIET_TN"].ToString() : null;
                        entity.ma_tbtn = ds.Rows[i]["MA_TBTN"] != DBNull.Value ? ds.Rows[i]["MA_TBTN"].ToString() : null;
                        entity.so_luong = ds.Rows[i]["SO_LUONG"] != DBNull.Value ? int.Parse(ds.Rows[i]["SO_LUONG"].ToString()) : null;
                        entity.ma_loai_bb = ds.Rows[i]["MA_LOAI_BB"] != DBNull.Value ? ds.Rows[i]["MA_LOAI_BB"].ToString() : null;
                        entity.ma_yctn = ds.Rows[i]["MA_YCTN"] != DBNull.Value ? ds.Rows[i]["MA_YCTN"].ToString() : null;
                        entity.file_upload = ds.Rows[i]["FILE_UPLOAD"] != DBNull.Value ? ds.Rows[i]["FILE_UPLOAD"].ToString() : null;
                        entity.ngay_tt_tn = ds.Rows[i]["NGAY_TT_TN"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TT_TN"].ToString()) : null;
                        entity.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : null;
                        entity.nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null;
                        entity.ngay_sua = ds.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_SUA"].ToString()) : null;
                        entity.nguoi_sua = ds.Rows[i]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[i]["NGUOI_SUA"].ToString() : null;
                        entity.lanthu = ds.Rows[i]["LANTHU"] != DBNull.Value ? int.Parse(ds.Rows[i]["LANTHU"].ToString()) : null;
                        entity.trang_thai_ky = ds.Rows[i]["TRANG_THAI_KY"] != DBNull.Value ? int.Parse(ds.Rows[i]["TRANG_THAI_KY"].ToString()) : null;

                        list.Add(model);
                    }
                    
                }
                return list.FirstOrDefault();
               
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool deleteQLTN_CHITIET_TN_BYID(string maCTTN)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.deleteQLTN_CHITIET_TN_BYID", "p_Error",
                    "p_maCTTTN",maCTTN
                );
                return string.IsNullOrEmpty(result);
            }
          
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm mới: {ex.Message}");
            }

        }
        public List<QLTN_CHI_TIET_THI_NGHIEM_Model> get_QLTN_CHITIET_TN_by_MATBTN(string maTBTN, string maYCTN)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_HUY.getQLTN_CHITIET_TN_by_MATBTN",
                                                 "p_MA_TBTN", "p_MA_YCTN", maTBTN,
                                                  maYCTN);

                List<QLTN_CHI_TIET_THI_NGHIEM_Model> list = new List<QLTN_CHI_TIET_THI_NGHIEM_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        QLTN_CHI_TIET_THI_NGHIEM_Model model = new QLTN_CHI_TIET_THI_NGHIEM_Model();
                        model.id = ds.Rows[i]["ID"] != DBNull.Value ? int.Parse(ds.Rows[i]["ID"].ToString()) : null;
                        model.ma_chi_tiet_tn = ds.Rows[i]["MA_CHI_TIET_TN"] != DBNull.Value ? ds.Rows[i]["MA_CHI_TIET_TN"].ToString() : null;
                        model.ma_tbtn = ds.Rows[i]["MA_TBTN"] != DBNull.Value ? ds.Rows[i]["MA_TBTN"].ToString() : null;
                        model.so_luong = ds.Rows[i]["SO_LUONG"] != DBNull.Value ? int.Parse(ds.Rows[i]["SO_LUONG"].ToString()) : null;
                        model.ma_loai_bb = ds.Rows[i]["MA_LOAI_BB"] != DBNull.Value ? ds.Rows[i]["MA_LOAI_BB"].ToString() : null;
                        model.ma_yctn = ds.Rows[i]["MA_YCTN"] != DBNull.Value ? ds.Rows[i]["MA_YCTN"].ToString() : null;
                        model.file_upload = ds.Rows[i]["FILE_UPLOAD"] != DBNull.Value ? ds.Rows[i]["FILE_UPLOAD"].ToString() : null;
                        model.ngay_tt_tn = ds.Rows[i]["NGAY_TT_TN"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TT_TN"].ToString()) : null;
                        model.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : null;
                        model.nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ngay_sua = ds.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_SUA"].ToString()) : null;
                        model.nguoi_sua = ds.Rows[i]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[i]["NGUOI_SUA"].ToString() : null;
                        model.lanthu = ds.Rows[i]["LANTHU"] != DBNull.Value ? int.Parse(ds.Rows[i]["LANTHU"].ToString()) : null;
                        model.trang_thai_ky = ds.Rows[i]["TRANG_THAI_KY"] != DBNull.Value ? int.Parse(ds.Rows[i]["TRANG_THAI_KY"].ToString()) : null;

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

        public QLTN_CHI_TIET_THI_NGHIEM_Model get_QLTN_CHITIET_TN_ByMA_CTTN(string Ma_CTTN)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_HUY.get_QLTN_CHITIET_TN_ByMA_CTTN",
                                                   "p_MA_CTTN", Ma_CTTN);

                if (ds != null && ds.Rows.Count > 0)
                {
                    QLTN_CHI_TIET_THI_NGHIEM_Model model = new QLTN_CHI_TIET_THI_NGHIEM_Model();
                    model.id = ds.Rows[0]["ID"] != DBNull.Value ? int.Parse(ds.Rows[0]["ID"].ToString()) : null;
                    model.ma_chi_tiet_tn = ds.Rows[0]["MA_CHI_TIET_TN"] != DBNull.Value ? ds.Rows[0]["MA_CHI_TIET_TN"].ToString() : null;
                    model.ma_tbtn = ds.Rows[0]["MA_TBTN"] != DBNull.Value ? ds.Rows[0]["MA_TBTN"].ToString() : null;
                    model.so_luong = ds.Rows[0]["SO_LUONG"] != DBNull.Value ? int.Parse(ds.Rows[0]["SO_LUONG"].ToString()) : null;
                    model.ma_loai_bb = ds.Rows[0]["MA_LOAI_BB"] != DBNull.Value ? ds.Rows[0]["MA_LOAI_BB"].ToString() : null;
                    model.ma_yctn = ds.Rows[0]["MA_YCTN"] != DBNull.Value ? ds.Rows[0]["MA_YCTN"].ToString() : null;
                    model.file_upload = ds.Rows[0]["FILE_UPLOAD"] != DBNull.Value ? ds.Rows[0]["FILE_UPLOAD"].ToString() : null;
                    model.ngay_tt_tn = ds.Rows[0]["NGAY_TT_TN"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_TT_TN"].ToString()) : null;
                    model.ngay_tao = ds.Rows[0]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_TAO"].ToString()) : null;
                    model.nguoi_tao = ds.Rows[0]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[0]["NGUOI_TAO"].ToString() : null;
                    model.ngay_sua = ds.Rows[0]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_SUA"].ToString()) : null;
                    model.nguoi_sua = ds.Rows[0]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[0]["NGUOI_SUA"].ToString() : null;
                    model.lanthu = ds.Rows[0]["LANTHU"] != DBNull.Value ? int.Parse(ds.Rows[0]["LANTHU"].ToString()) : null;
                    model.trang_thai_ky = ds.Rows[0]["TRANG_THAI_KY"] != DBNull.Value ? int.Parse(ds.Rows[0]["TRANG_THAI_KY"].ToString()) : null;
                    model.nhomky_hientai = ds.Rows[0]["NHOM_KY_HIEN_TAI"] != DBNull.Value ? int.Parse(ds.Rows[0]["NHOM_KY_HIEN_TAI"].ToString()) : null;

                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
