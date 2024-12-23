using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using API_PCHY.Models.QLTN.DM_LOAI_YCTN;

namespace API_PCHY.Models.QLTN.QLTN_YCTN
{
    public class QLTN_YCTN_Model
    {
        public int? id { get; set; }
        public string? ten_yctn { get; set; }
        public string? ma_yctn { get; set; }
        public string? ma_loai_yctn { get; set; }
        public decimal? tong_gia_tri { get; set; }
        public int? id_khach_hang { get; set; }
        public string? ma_khach_hang { get; set; }
        public int? loai_tai_san { get; set; }
        public int? crr_step { get; set; }
        public int? next_step { get; set; }
        public int? nam_thuc_hien { get; set; }
        public string? noi_dung { get; set; }
        public decimal? gtdt_truoc_thue { get; set; }
        public decimal? gtdt_thue { get; set; }
        public decimal? gtdt_sau_thue { get; set; }
        public decimal? gtdt_chiet_giam { get; set; }
        public decimal? gtdt_sau_chiet_giam { get; set; }
        public DateTime? ngay_ky_hop_dong { get; set; }
        public DateTime? ngay_xay_ra_su_co { get; set; }
        public string? file_upload { get; set; }
        public DateTime? ngay_giao_nv { get; set; }
        public string? file_dinh_kem_giao_nv { get; set; }
        public string? nguoi_giao_nhiem_vu { get; set; }
        public string? file_pa_thi_cong { get; set; }
        public string? nguoi_th_ks_lap_pa_thi_cong { get; set; }
        public DateTime? ngay_nhap_kl_thuc_hien { get; set; }
        public string? nguoi_nhap_kl_thuc_hien { get; set; }
        public string? nguoi_ban_giao { get; set; }
        public string? don_vi_nhan_ban_giao { get; set; }
        public DateTime? ngay_ban_giao { get; set; }
        public string? ghi_chu_ban_giao { get; set; }
        public decimal? phan_tram_thue { get; set; }
        public decimal? phan_tram_chiet_giam { get; set; }
        public DateTime? ngay_ks_lap_pa_thi_cong { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public List<String>? don_vi_thuc_hien { get; set; }
        public DM_LOAI_YCTN_Model loai_yctn_model { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }       
        public List<QLTN_YCTN_LOG_Model>? qltn_yctn_log { get; set; } 
    }

    public class QLTN_YCTN_LOG_Model : QLTN_YCTN_Model
    {
        public QLTN_YCTN_LOG_Model()
        {
            // Gán null cho các thuộc tính khi khởi tạo lớp con           
            this.qltn_yctn_log = null;
        }
    }
}
