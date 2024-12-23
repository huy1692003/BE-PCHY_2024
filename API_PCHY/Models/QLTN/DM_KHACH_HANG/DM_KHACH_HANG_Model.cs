    using System;

    namespace API_PCHY.Models.QLTN.DM_KHACH_HANG
    {

        public class DM_KHACH_HANG_Model
        {
            public int? id { get; set; }
            public string ten_kh { get; set; } 
            public string ghi_chu { get; set; }  
            public DateTime ngay_tao { get; set; } 
            public string nguoi_tao { get; set; }  
            public DateTime? ngay_sua { get; set; }  
            public string nguoi_sua { get; set; } 
            public string so_dt { get; set; } 
            public string email { get; set; }  
            public string ma_so_thue { get; set; }  
            public string dia_chi { get; set; }  
        }


        public class DM_KHACH_HANG_Request
        {
            public string searchData { get; set; }  
            public int pageIndex { get; set; }   
            public int pageSize { get; set; }    

     
        }

    }
