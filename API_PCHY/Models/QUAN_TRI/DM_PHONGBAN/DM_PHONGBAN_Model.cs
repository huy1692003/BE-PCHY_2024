using System;

namespace APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN
{
    public class DM_PHONGBAN_Model
    {
        public string? id { get; set; }
        public string? ma { get; set; }
        public string? ten { get; set; }
        public int? trang_thai { get; set; }
        public float? sap_xep { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string? nguoi_cap_nhat { get; set; }
        public string? dm_donvi_id { get; set; }
        public string? dm_phongban_id { get; set; }
        public string? db_maphongban { get; set; }
        public DateTime? db_ngay { get; set; }
        public string? ma_dviqly { get; set; }
        public string? ten_dviqly { get; set; }



    }
}
