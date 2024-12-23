using System;

namespace APIPCHY_PhanQuyen.Models.QLKC.DM_DONVI
{
    public class DM_DONVI_Model
    {
        public string? id { get; set; }
        public string? dm_donvi_id { get; set; }
        public int? loai_don_vi { get; set; }
        public string? ma { get; set; }
        public string? ten { get; set; }
        public int? trang_thai { get; set; }
        public int? sap_xep { get; set; }
        public string? ghi_chu { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string? nguoi_cap_nhat { get; set; }
        public string? cap_so { get; set; }
        public string? cap_ma { get; set; }
        public string? dm_tinhthanh_id { get; set; }
        public string? dm_quanhuyen_id { get; set; }
        public string? dm_donvi_chuquan_id { get; set; }
        public string? ma_fmis { get; set; }
        public string? db_madonvi { get; set; }
        public string? db_madonvi_fmis { get; set; }
        public DateTime? db_ngay { get; set; }
        public int? type_donvi { get; set; }
        public int? group_donvi { get; set; }
        public int? do_duthao { get; set; }
        public int? su_dung { get; set; }
        public string? ma_dviqly { get; set; }
    }

}
