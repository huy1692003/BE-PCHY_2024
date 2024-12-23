using System;

namespace API_PCHY.Models.QLTN.DM_LOAI_YCTN
{
    public class DM_LOAI_YCTN_Model
    {
        public int? id { get; set; }
        public string? ma_loai_yctn { get; set; }
        public string? ten_loai_yc { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }
        public string? key_word { get; set; }
    }
}
