using Org.BouncyCastle.Utilities;
using System;

namespace APIPCHY_PhanQuyen.Models.QLKC.HT_NHOMQUYEN
{
    public class HT_NHOMQUYEN_Model
    {

        public int? id { get; set; }
        public string? nhom_id { get; set; }
        public string? ghi_chu { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }
        public int? cap_bac { get; set; }
        public int? sap_xep { get; set; }
        public string? ten_nhom { get; set; }
        public string? ma_dviqly { get; set; }
        public string? ten { get; set; }
    }
}
