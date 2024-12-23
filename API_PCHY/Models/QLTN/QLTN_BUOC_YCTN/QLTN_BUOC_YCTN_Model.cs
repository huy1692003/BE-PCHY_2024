using System;

namespace API_PCHY.Models.QLTN.QLTN_BUOC_YCTN
{
    public class QLTN_BUOC_YCTN_Model
    {
        public int? id { get; set; }
        public string ten_buoc_yctn { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }

        public string? ghi_chu { get; set; }
        public int? buoc { get; set; }


    }
}
