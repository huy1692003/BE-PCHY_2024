using System;


namespace API_PCHY.Models.QLTN.DM_LOAI_BIENBAN
{
    public class DM_LOAI_BIENBAN_Model
    {
        public int? id { get; set; }

        public string ten_loai_bb { get; set; }

        public DateTime? ngay_tao { get; set; }

        public string nguoi_tao { get; set; }

        public DateTime? ngay_sua { get; set; }

        public string nguoi_sua { get; set; }

        public string ghi_chu { get; set; }
    }

    public class DM_LOAI_BIENBAN_Request
    {
        public string? searchData { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }

    }


}
