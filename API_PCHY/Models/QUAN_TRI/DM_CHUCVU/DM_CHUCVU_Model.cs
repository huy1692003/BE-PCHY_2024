using System;

namespace API_PCHY.Models.QUAN_TRI.DM_CHUC_VU
{
    public class DM_CHUCVU_Model
    {
        public string id { get; set; }
        public string? ma { get; set; }
        public string? ten { get; set; }
        public int? trang_thai { get; set; }
        public int? sap_xep { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string? nguoi_cap_nhat { get; set; }
        public string? dm_donvi_id { get; set; }
        public string? db_machucvu { get; set; }
        public DateTime? db_ngay { get; set; }
    }
}
