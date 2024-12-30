using System;
namespace API_PCHY.Models.QLTN.QLTN_NGUOI_KY
{
    public class QLTN_NGUOI_KY_Model
    {
        public int? id { get; set; }
        public int? trang_thai_ky { get; set; }
        public int? nhom_nguoi_ky { get; set; }
        public string? ly_do_tu_choi { get; set; }
        public string? ma_chi_tiet_tn { get; set; }
        public string? id_nguoi_ky { get; set; }
        public DateTime? thoi_gian_ky { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }
        public string? ten_dang_nhap { get; set; }
        public string? ho_ten { get; set; }
        public DateTime? ngay_tu_choi { get; set; }
    }
}
