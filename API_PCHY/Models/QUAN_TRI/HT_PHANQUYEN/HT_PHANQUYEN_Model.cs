using System;

namespace APIPCHY_PhanQuyen.Models.QLKC.HT_PHANQUYEN
{
    public class HT_PHANQUYEN_Model
    {

        public int? id { get; set; }
        public string? tieu_de { get; set; }
        public string? ghi_chu { get; set; }
        public int? tt_khoa { get; set; }
        public string? nguoi_khoa { get; set; }
        public int? tt_xoa { get; set; }
        public string? nguoi_xoa { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }
        public int? menu_id { get; set; }
        public int? view { get; set; }
        public int? insert { get; set; }
        public int? edit { get; set; }
        public int? delete { get; set; }
        public int? export { get; set; }
        public int? dong_bo { get; set; }
        public int? hard_edit { get; set; }
        public int? chuyen_buoc { get; set; }
        public int? ma_nhom_tv { get; set; }
        public string? ma_dviqly { get; set; }
    }
}
