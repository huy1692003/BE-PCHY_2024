namespace API_PCHY.Models.QLTN.BAO_CAO
{
    public class BAO_CAO_Model
    {
        public class SOLUONG_YCTN_Model
        {
            public string ten_kh { get; set; }
            public string ten_don_vi { get; set; }
            public string? kehoachthinghiem { get; set; }
            public string? hopdong { get; set; }
            public string? xulysuco { get; set; }
            public string? taomoi { get; set; }
            public string? giaonhiemvu { get; set; }

            public string? nhapkhoiluong { get; set; }
            public string? khaosat { get; set; }
            public string? thinghiem { get; set; }
            public string? bangiao { get; set; }
            public string? tongso { get; set; }

        }

        public class CHU_KY_THEO_DONVI
        {
            public string? id_dv { get; set; } // ID của đơn vị
            public string? ten_dv { get; set; } // Tên của đơn vị
            public int? total_trans { get; set; } // Tổng số giao dịch
            public int? total_trans_success { get; set; } // Tổng số giao dịch thành công
            public int? total_trans_fail { get; set; } // Tổng số giao dịch thất bại
        }

        public class dashboard_model
        {
            public int total_kyso_waiting { get; set; }         
            public int total_YCTN { get; set; } 
            public int total_kyso_fail { get; set; } 
            public int total_kyso_success { get; set; } 
        }




    }
}
