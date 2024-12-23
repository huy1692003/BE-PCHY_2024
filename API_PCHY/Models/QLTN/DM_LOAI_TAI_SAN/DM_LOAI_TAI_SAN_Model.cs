using System;

namespace API_PCHY.Models.QLTN.DM_LOAI_TAI_SAN
{
    public class DM_LOAI_TAI_SAN_Model
    {
        public int? id { get; set; }
        public string ten_lts { get; set; }
        public string ghi_chu { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public string nguoi_sua { get; set; }
    }

    public class DM_LOAI_TAI_SAN_Request
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
