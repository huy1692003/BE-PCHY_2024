using System;
using System.Collections.Generic;
using API_PCHY.Models.QLTN.QLTN_CHI_TIET_THI_NGHIEM;
using API_PCHY.Models.QLTN.QLTN_NGUOI_KY;
using iTextSharp.text;

namespace API_PCHY.Models.QLTN.QLTN_KYSO
{
    public class Document_Model
    {
        public string? ten_yctn { get; set; }          // YCTN.TEN_YCTN
        public string? ma_yctn { get; set; }          // YCTN.MA_YCTN
        public string? loai_yctn { get; set; }          // YCTN.MA_YCTN
        public string? ma_loaitb { get; set; }        // TB.MA_LOAI_TB
        public string? ten_thietbi { get; set; }      // TB.TEN_THIET_BI
        public int? soluong { get; set; }            // CT.SO_LUONG
        public int? lanthu { get; set; }             // CT.LANTHU
        public List<string>? don_vi_thuc_hien { get; set; }   // YCTN.DON_VI_THUC_HIEN
        public string? ten_loai_bb { get; set; }       // lbb.TEN_LOAI_BB
        public DateTime? ngaytao { get; set; }       // CT.NGAY_TAO
        public string? ma_chitiet_tn { get; set; }     // CT.MA_CHI_TIET_TN
        public string? file_upload { get; set; }      // CT.FILE_UPLOAD
        public string? nguoi_tao { get; set; }        // CT.NGUOI_TAO
        public int? rownum { get; set; }             // ROW_NUMBER()
        public List<QLTN_NGUOI_KY_Model>? list_NguoiKy { get; set; }
        public QLTN_CHI_TIET_THI_NGHIEM_Model? chi_tiet_tn { get; set; }
    }

    public class SearchParamDocument
    {
        public string? Keyword { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? Status_Document { get; set; }
        public int? TienTrinhKySo { get; set; }
        public string? UserId { get; set; }
        public string? DonViThucHien { get; set; }
        public int? IdLoaiBienBan { get; set; }

    }


    public class Paginage
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Paginage()
        {
            this.Page=1;
            this.PageSize=10;
        }
    }

    public class ReqUpdateKySo
    {
        public int Id { get; set; }

        // Mã chi tiết thí nghiệm
        public string MaCTTN{ get; set; }

        // ID người ký
        public string IdNguoiKy { get; set; }

        // Nhóm người ký
        public int NhomNguoiKy { get; set; }

        // Trạng thái ký (1: đồng ý ký, -1: từ chối ký)
        public int TrangThai { get; set; }

        // Lý do từ chối (nếu trạng thái = -1)
        public string LyDoTuChoi { get; set; }
    }
}
