using System;

namespace APIPCHY_PhanQuyen.Models.QLKC.HT_QUYEN_NGUOIDUNG
{
    public class HT_QUYEN_NGUOIDUNG_Model
    {
        public int ID { get; set; }
        public string MA_NGUOI_DUNG { get; set; }
        public string MA_NHOM_TV { get; set; }

        public string? TenNguoiDung { get; set; }
        public string? TenDonVi { get; set; }
        public string? TenNhom { get; set; }
        public DateTime? NGAY_TAO
        {
            get; set;
        }
    }
}
