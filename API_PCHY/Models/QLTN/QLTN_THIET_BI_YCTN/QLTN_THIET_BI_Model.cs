using System;
using System.Collections.Generic;
using API_PCHY.Models.QLTN.QLTN_CHI_TIET_THI_NGHIEM;

namespace API_PCHY.Models.QLTN.QLTN_THIET_BI_YCTN
{
    public class QLTN_THIET_BI_Model
    {
        public string id { get; set; }
        public string? ma_tbtn { get; set; }
        public string? ma_yctn { get; set; }
        public string? ten_thiet_bi { get; set; }
        public string? ma_loai_tb { get; set; }
        public int? so_luong { get; set; }

        public int? trang_thai { get; set; }

        public DateTime? ngay_tao { get; set; }
         public List<QLTN_CHI_TIET_THI_NGHIEM_Model>? listTN {  get; set; }
        public string? nguoi_tao { get; set; }
        public string? ten_loai_thiet_bi { get; set; }


    }
}
