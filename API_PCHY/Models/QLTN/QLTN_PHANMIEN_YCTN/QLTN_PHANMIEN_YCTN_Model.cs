using System;

namespace API_PCHY.Models.QLTN.QLTN_PHANMIEN_YCTN
{
    public class QLTN_PHANMIEN_YCTN_Model
    {
        public int id { get; set; }
        public int ma_truong_yctn { get; set; }
        public int ma_loai_yctn { get; set; }
        public DateTime ngay_tao { get; set; }
    }
}