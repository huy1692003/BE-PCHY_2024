using System;
using System.Collections.Generic;

namespace APIPCHY_PhanQuyen.Models.QLKC.HT_MENU
{
    public class HT_MENU_Model
    {

        public int? id { get; set; }
        public string? ten_menu { get; set; }
        public string? ghi_chu { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_sua { get; set; }
        public string? duong_dan { get; set; }
        public int? parent_id { get; set; }
        public string? icon { get; set; }
        public int? sap_xep { get; set; }
        public List<HT_MENU_Model> children { get; set; }
    }
}
