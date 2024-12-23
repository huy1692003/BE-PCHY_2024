using APIPCHY_PhanQuyen.Models.QLKC.DM_DONVI;
using iTextSharp.text;
using System;
using System.Collections.Generic;

namespace APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG
{
    public class HT_NGUOIDUNG_Model
    {
        public string? id { get; set; }
        public string? dm_donvi_id { get; set; }
        public string? ten_donvi { get; set; }
        public string? dm_phongban_id { get; set; }
        public string? ten_phongban { get; set; }
        public string? dm_kieucanbo_id { get; set; }
        public string? dm_chucvu_id { get; set; }
        public string? ten_dang_nhap { get; set; }
        public string? mat_khau { get; set; }
        public string? ho_ten { get; set; }
        public string? email { get; set; }
        public string? ldap { get; set; }
        public int? trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string? nguoi_cap_nhat { get; set; }
        public string? so_dien_thoai { get; set; }
        public int? gioi_tinh { get; set; }
        public string? so_cmnd { get; set; }
        public int? trang_thai_dong_bo { get; set; }
        public string? db_taikhoandangnhap { get; set; }
        public DateTime? db_ngay { get; set; }
        public string? dm_donvi_lamviec_id { get; set; }
        public string? ht_vaitro_id { get; set; }
        public string? sign_alias { get; set; }
        public string? sign_username { get; set; }
        public string? sign_password { get; set; }
        public int? hrms_type { get; set; }
        public string? sign_image { get; set; }
        public string? anhchukynhay { get; set; }
        public string? roleid { get; set; }
        public string? phong_ban { get; set; }
        public string? anhdaidien { get; set; }
        public string? ma_dviqly { get; set; }
        public string ? value_token {  get; set; }
        public List<DM_DONVI_Model> ds_donvi { get; set; }
        public string token { get; set; }

    }
    public class NguoidungManager
    {
        public string HO_TEN { get; set; }
        public string TEN_DANG_NHAP { get; set; }
        public int? TRANG_THAI { get; set; }
    }

    public class UserFilterRequest : NguoidungManager
    {
        public string DM_DONVI_ID { get; set; }
        public string DM_PHONGBAN_ID { get; set; }
        public string DM_CHUCVU_ID { get; set; }
        public string ma_dviqly { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }


    public class UserResponse : NguoidungManager
    {
        public string ID { get; set; }
        public string TEN_DONVI { get; set; }
        public string TEN_PHONGBAN { get; set; }
        public string TEN_CHUCVU { get; set; }
        public string EMAIL { get; set; }
    }


    public class HTNguoiDungDTO : NguoidungManager
    {
        public string ID { get; set; }
        public string? TEN_DON_VI { get; set; }
        public string? TEN_PHONG_BAN { get; set; }
        public string? TEN_CHUC_VU { get; set; }
        public string DM_DONVI_ID { get; set; }
        public string DM_PHONGBAN_ID { get; set; }
        public string DM_KIEUCANBO_ID { get; set; }
        public string DM_CHUCVU_ID { get; set; }
        public string TEN_DANG_NHAP { get; set; }
        public string MAT_KHAU { get; set; }
        public string HO_TEN { get; set; }
        public string EMAIL { get; set; }
        public string LDAP { get; set; }
        public int? TRANG_THAI { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public string NGUOI_TAO { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        public string NGUOI_CAP_NHAT { get; set; }
        public string SO_DIEN_THOAI { get; set; }
        public int? GIOI_TINH { get; set; }
        public string SO_CMND { get; set; }
        public int? TRANG_THAI_DONG_BO { get; set; }
        public string DB_TAIKHOANDANGNHAP { get; set; }
        public DateTime? DB_NGAY { get; set; }
        public string DM_DONVI_LAMVIEC_ID { get; set; }
        public string HT_VAITRO_ID { get; set; }
        public string SIGN_ALIAS { get; set; }
        public string SIGN_USERNAME { get; set; }
        public string SIGN_PASSWORD { get; set; }
        public int? HRMS_TYPE { get; set; }
        public string SIGN_IMAGE { get; set; }
        public string ANHCHUKYNHAY { get; set; }
        public string ROLEID { get; set; }
        public string PHONG_BAN { get; set; }
        public string ANHDAIDIEN { get; set; }
    }
    public class Password_NguoiDung
    {
        public string ID { get; set; }
        public string MAT_KHAU { get; set; }
    }
}
