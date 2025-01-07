using APIPCHY.Helpers;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;
using APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG;
using APIPCHY_PhanQuyen.Models.QLTN.DM_DONVI;
using APIPCHY_PhanQuyen.Models.QLTN.HT_MENU;
using APIPCHY_PhanQuyen.Services;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace APIPCHY_PhanQuyen.Models.QLTN.HT_NGUOIDUNG
{
    public class HT_NGUOIDUNG_Manager
    {

        private readonly IConfiguration _configuration; // Thêm biến cấu hình

        public HT_NGUOIDUNG_Manager(IConfiguration configuration)
        {
            _configuration = configuration; // Khởi tạo biến cấu hình
        }

        DataHelper helper = new DataHelper();

        private string secret;

        public HT_NGUOIDUNG_Model login_HT_NGUOIDUNG(string username, string password)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.login_HT_NGUOIDUNG", "p_TEN_DANG_NHAP", username);
                if (ds == null || ds.Rows.Count == 0)
                {

                    return null;

                }
                else
                {
                    HT_NGUOIDUNG_Model user = new HT_NGUOIDUNG_Model();
                    user.mat_khau = ds.Rows[0]["MAT_KHAU"] != DBNull.Value ? ds.Rows[0]["MAT_KHAU"].ToString() : null;

                    if (!PasswordHasher.VerifyHashedString(user.mat_khau, password))
                    {
                        return null;
                    }
                    else
                    {



                        user.id = ds.Rows[0]["ID"] != DBNull.Value ? ds.Rows[0]["ID"].ToString() : null;
                        user.dm_donvi_id = ds.Rows[0]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[0]["DM_DONVI_ID"].ToString() : null;
                        user.ten_donvi = ds.Rows[0]["TEN"] != DBNull.Value ? ds.Rows[0]["TEN"].ToString() : null;
                        user.dm_phongban_id = ds.Rows[0]["DM_PHONGBAN_ID"] != DBNull.Value ? ds.Rows[0]["DM_PHONGBAN_ID"].ToString() : null;
                        user.ten_phongban = ds.Rows[0]["TEN_PB"] != DBNull.Value ? ds.Rows[0]["TEN_PB"].ToString() : null;
                        user.dm_kieucanbo_id = ds.Rows[0]["DM_KIEUCANBO_ID"] != DBNull.Value ? ds.Rows[0]["DM_KIEUCANBO_ID"].ToString() : null;
                        user.dm_chucvu_id = ds.Rows[0]["DM_CHUCVU_ID"] != DBNull.Value ? ds.Rows[0]["DM_CHUCVU_ID"].ToString() : null;
                        user.ten_dang_nhap = ds.Rows[0]["TEN_DANG_NHAP"] != DBNull.Value ? ds.Rows[0]["TEN_DANG_NHAP"].ToString() : null;
                        user.mat_khau = null;
                        user.ho_ten = ds.Rows[0]["HO_TEN"] != DBNull.Value ? ds.Rows[0]["HO_TEN"].ToString() : null;
                        user.email = ds.Rows[0]["EMAIL"] != DBNull.Value ? ds.Rows[0]["EMAIL"].ToString() : null;
                        user.ldap = ds.Rows[0]["LDAP"] != DBNull.Value ? ds.Rows[0]["LDAP"].ToString() : null;
                        user.trang_thai = ds.Rows[0]["TRANG_THAI"] != DBNull.Value ? int.Parse(ds.Rows[0]["TRANG_THAI"].ToString()) : null;
                        user.so_dien_thoai = ds.Rows[0]["SO_DIEN_THOAI"] != DBNull.Value ? ds.Rows[0]["SO_DIEN_THOAI"].ToString() : null;
                        user.gioi_tinh = ds.Rows[0]["GIOI_TINH"] != DBNull.Value ? int.Parse(ds.Rows[0]["GIOI_TINH"].ToString()) : null;
                        user.so_cmnd = ds.Rows[0]["SO_CMND"] != DBNull.Value ? ds.Rows[0]["SO_CMND"].ToString() : null;
                        user.dm_donvi_lamviec_id = ds.Rows[0]["DM_DONVI_LAMVIEC_ID"] != DBNull.Value ? ds.Rows[0]["DM_DONVI_LAMVIEC_ID"].ToString() : null;
                        user.ht_vaitro_id = ds.Rows[0]["HT_VAITRO_ID"] != DBNull.Value ? ds.Rows[0]["HT_VAITRO_ID"].ToString() : null;
                        user.hrms_type = ds.Rows[0]["HRMS_TYPE"] != DBNull.Value ? int.Parse(ds.Rows[0]["HRMS_TYPE"].ToString()) : null;
                        user.sign_image = ds.Rows[0]["SIGN_IMAGE"] != DBNull.Value ? ds.Rows[0]["SIGN_IMAGE"].ToString() : null;
                        user.anhchukynhay = ds.Rows[0]["ANHCHUKYNHAY"] != DBNull.Value ? ds.Rows[0]["ANHCHUKYNHAY"].ToString() : null;
                        user.roleid = ds.Rows[0]["ROLEID"] != DBNull.Value ? ds.Rows[0]["ROLEID"].ToString() : null;
                        user.phong_ban = ds.Rows[0]["PHONG_BAN"] != DBNull.Value ? ds.Rows[0]["PHONG_BAN"].ToString() : null;
                        user.anhdaidien = ds.Rows[0]["ANHDAIDIEN"] != DBNull.Value ? ds.Rows[0]["ANHDAIDIEN"].ToString() : null;
                        user.ma_dviqly = ds.Rows[0]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[0]["MA_DVIQLY"].ToString() : null;
                        user.value_token = ds.Rows[0]["VALUE_TOKEN"] != DBNull.Value ? ds.Rows[0]["VALUE_TOKEN"].ToString() : null;
                        user.ds_donvi = new DM_DONVI_Manager().get_All_DM_DONVI_ByMADVIQLY(user.ma_dviqly);
                        TokenService tokenService = new TokenService(_configuration);
                        string token = tokenService.GenerateJwtToken(user);
                        user.token = token;
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<HTNguoiDungDTO> GET_HT_NGUOIDUNG(int pageIndex, int pageSize, out long total)
        {
            List<HTNguoiDungDTO> result = new List<HTNguoiDungDTO>();

            using (OracleConnection cn = new ConnectionOracle().getConnection())
            {
                cn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"PKG_QLTN_TANH.get_ALL_NGUOIDUNG";

                    cmd.Parameters.Add("p_pageIndex", OracleDbType.Int32).Value = pageIndex;
                    cmd.Parameters.Add("p_pageSize", OracleDbType.Int32).Value = pageSize;

                    cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_totalRecords", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    OracleDataAdapter dap = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dap.Fill(ds);

                    // Xử lý dữ liệu phân trang
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            HTNguoiDungDTO user = new HTNguoiDungDTO
                            {
                                ID = dr["ID"].ToString(),
                                //DM_DONVI_ID = dr["DM_DONVI_ID"].ToString(),
                                //DM_PHONGBAN_ID = dr["DM_PHONGBAN_ID"].ToString(),
                                //DM_CHUCVU_ID = dr["DM_CHUCVU_ID"].ToString(),
                                TEN_DANG_NHAP = dr["TEN_DANG_NHAP"].ToString(),
                                //MAT_KHAU = dr["MAT_KHAU"].ToString(),
                                HO_TEN = dr["HO_TEN"].ToString(),
                                EMAIL = dr["EMAIL"].ToString(),
                                LDAP = dr["LDAP"].ToString(),
                                TRANG_THAI = dr["TRANG_THAI"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["TRANG_THAI"]),
                                NGAY_TAO = dr["NGAY_TAO"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["NGAY_TAO"]),
                                NGUOI_TAO = dr["NGUOI_TAO"].ToString(),
                                NGAY_CAP_NHAT = dr["NGAY_CAP_NHAT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["NGAY_CAP_NHAT"]),
                                NGUOI_CAP_NHAT = dr["NGUOI_CAP_NHAT"].ToString(),
                                SO_DIEN_THOAI = dr["SO_DIEN_THOAI"].ToString(),
                                GIOI_TINH = dr["GIOI_TINH"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["GIOI_TINH"]),
                                //SO_CMND = dr["SO_CMND"].ToString(),
                                TRANG_THAI_DONG_BO = dr["TRANG_THAI_DONG_BO"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["TRANG_THAI_DONG_BO"]),
                                ROLEID = dr["ROLEID"].ToString(),
                                PHONG_BAN = dr["PHONG_BAN"].ToString(),
                                ANHDAIDIEN = dr["ANHDAIDIEN"].ToString()
                            };

                            result.Add(user);
                        }
                    }

                    // Lấy tổng số bản ghi
                    var totalRecord = Convert.ToInt64(cmd.Parameters["p_totalRecords"].Value.ToString());
                    total = (long)Math.Ceiling((double)totalRecord / pageSize);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while fetching data.", ex);
                }
            }

            return result;
        }


        //them nguoi dung
        public string Insert_QLTN_NGUOI_DUNG(HT_NGUOIDUNG_Model data)
        {
            try
            {
                // Hash mật khẩu người dùng
                string hashPassword = PasswordHasher.HashString(data.mat_khau);

                // Sử dụng helper để thực thi stored procedure
                string result = helper.ExcuteNonQuery(
                    "PKG_QLTN_QUANTRI.insert_HT_NGUOIDUNG", "p_Error",
                    "p_ID", "p_DM_DONVI_ID", "p_DM_PHONGBAN_ID", "p_DM_KIEUCANBO_ID", "p_DM_CHUCVU_ID",
                    "p_TEN_DANG_NHAP", "p_MAT_KHAU", "p_HO_TEN", "p_EMAIL", "p_LDAP", "p_TRANG_THAI",
                    "p_NGAY_TAO", "p_NGUOI_TAO", "p_NGAY_CAP_NHAT", "p_NGUOI_CAP_NHAT", "p_SO_DIEN_THOAI",
                    "p_GIOI_TINH", "p_SO_CMND", "p_TRANG_THAI_DONG_BO", "p_DB_TAIKHOANDANGNHAP", "p_DB_NGAY",
                    "p_DM_DONVI_LAMVIEC_ID", "p_HT_VAITRO_ID", "p_SIGN_ALIAS", "p_SIGN_USERNAME", "p_SIGN_PASSWORD",
                    "p_HRMS_TYPE", "p_SIGN_IMAGE", "p_ANHCHUKYNHAY", "p_ROLEID", "p_PHONG_BAN", "p_ANHDAIDIEN",
                    "p_VALUE_TOKEN",
                    data.id ?? Guid.NewGuid().ToString(),
                    data.dm_donvi_id,
                    data.dm_phongban_id,
                    data.dm_kieucanbo_id,
                    data.dm_chucvu_id,
                    data.ten_dang_nhap,
                    hashPassword,
                    data.ho_ten,
                    data.email,
                    data.ldap,
                    data.trang_thai,
                    data.ngay_tao != DateTime.MinValue ? (object)data.ngay_tao : DBNull.Value,
                    data.nguoi_tao,
                    data.ngay_cap_nhat != DateTime.MinValue ? (object)data.ngay_cap_nhat : DBNull.Value,
                    data.nguoi_cap_nhat,
                    data.so_dien_thoai,
                    data.gioi_tinh,
                    data.so_cmnd,
                    data.trang_thai_dong_bo,
                    data.db_taikhoandangnhap,
                    data.db_ngay != DateTime.MinValue ? (object)data.db_ngay : DBNull.Value,
                    data.dm_donvi_lamviec_id,
                    data.ht_vaitro_id,
                    data.sign_alias,
                    data.sign_username,
                    data.sign_password,
                    data.hrms_type,
                    data.sign_image,
                    data.anhchukynhay,
                    data.roleid,
                    data.phong_ban,
                    data.anhdaidien,
                    data.value_token
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi stored procedure: " + ex.Message);
            }
        }


        //get by id nguoi dung
        public HTNguoiDungDTO GETDATA_DM_NGUOIDUNG_byID(string Id)
        {
            OracleConnection cn = new ConnectionOracle().getConnection();
            cn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                OracleDataAdapter dap = new OracleDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"PKG_QLTN_QUANTRI.get_HT_NGUOIDUNG_byID";
                cmd.Parameters.Add("p_ID", Id);
                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                dap.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dap.Fill(ds);
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    HTNguoiDungDTO result = new HTNguoiDungDTO
                    {
                        ID = dr["ID"].ToString(),
                        TEN_DON_VI = dr["TEN_DON_VI"].ToString(),
                        TEN_PHONG_BAN = dr["TEN_PHONG_BAN"].ToString(),
                        TEN_CHUC_VU = dr["TEN_CHUC_VU"].ToString(),
                        TEN_DANG_NHAP = dr["TEN_DANG_NHAP"].ToString(),
                        HO_TEN = dr["HO_TEN"].ToString(),
                        EMAIL = dr["EMAIL"].ToString(),
                        LDAP = dr["LDAP"].ToString(),
                        TRANG_THAI = dr["TRANG_THAI"] != DBNull.Value ? Convert.ToInt32(dr["TRANG_THAI"]) : (int?)null,
                        NGAY_TAO = dr["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(dr["NGAY_TAO"]) : (DateTime?)null,
                        NGUOI_TAO = dr["NGUOI_TAO"].ToString(),
                        NGAY_CAP_NHAT = dr["NGAY_CAP_NHAT"] != DBNull.Value ? Convert.ToDateTime(dr["NGAY_CAP_NHAT"]) : (DateTime?)null,
                        NGUOI_CAP_NHAT = dr["NGUOI_CAP_NHAT"].ToString(),
                        SO_DIEN_THOAI = dr["SO_DIEN_THOAI"].ToString(),
                        GIOI_TINH = dr["GIOI_TINH"] != DBNull.Value ? Convert.ToInt32(dr["GIOI_TINH"]) : (int?)null,
                        TRANG_THAI_DONG_BO = dr["TRANG_THAI_DONG_BO"] != DBNull.Value ? Convert.ToInt32(dr["TRANG_THAI_DONG_BO"]) : (int?)null,
                        DB_TAIKHOANDANGNHAP = dr["DB_TAIKHOANDANGNHAP"].ToString(),
                        DB_NGAY = dr["DB_NGAY"] != DBNull.Value ? Convert.ToDateTime(dr["DB_NGAY"]) : (DateTime?)null,
                        DM_DONVI_LAMVIEC_ID = dr["DM_DONVI_LAMVIEC_ID"].ToString(),
                        HT_VAITRO_ID = dr["HT_VAITRO_ID"].ToString(),
                    };

                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }
        //Lấy chi tiết 
        public HT_NGUOIDUNG_Model GET_DM_NGUOIDUNG_byID(string Id)
        {
            OracleConnection cn = new ConnectionOracle().getConnection();
            cn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                OracleDataAdapter dap = new OracleDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"PKG_QLTN_QUANTRI.get_HT_NGUOIDUNGByID";
                cmd.Parameters.Add("p_ID", Id);
                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                dap.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dap.Fill(ds);

                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    HT_NGUOIDUNG_Model result = new HT_NGUOIDUNG_Model()
                    {
                        id = dr["ID"].ToString(),
                        dm_donvi_id = dr["DM_DONVI_ID"].ToString(),
                        dm_phongban_id = dr["DM_PHONGBAN_ID"].ToString(),
                        dm_kieucanbo_id = dr["DM_KIEUCANBO_ID"].ToString(),
                        dm_chucvu_id = dr["DM_CHUCVU_ID"].ToString(),
                        ten_dang_nhap = dr["TEN_DANG_NHAP"].ToString(),
                        mat_khau = null,
                        ho_ten = dr["HO_TEN"].ToString(),
                        email = dr["EMAIL"].ToString(),
                        ldap = dr["LDAP"].ToString(),
                        trang_thai = int.TryParse(dr["TRANG_THAI"].ToString(), out int trangThai) ? trangThai : (int?)null,  // Trả về null nếu không thể parse
                        ngay_tao = dr["NGAY_TAO"] as DateTime?,  // Nếu không có giá trị thì trả về null
                        nguoi_tao = dr["NGUOI_TAO"].ToString(),
                        ngay_cap_nhat = dr["NGAY_CAP_NHAT"] as DateTime?,  // Nếu không có giá trị thì trả về null
                        nguoi_cap_nhat = dr["NGUOI_CAP_NHAT"].ToString(),
                        so_dien_thoai = dr["SO_DIEN_THOAI"].ToString(),
                        gioi_tinh = int.TryParse(dr["GIOI_TINH"].ToString(), out int gioiTinh) ? gioiTinh : (int?)null,  // Trả về null nếu không thể parse
                        so_cmnd = dr["SO_CMND"].ToString(),
                        trang_thai_dong_bo = dr["TRANG_THAI_DONG_BO"] as int?,  // Trả về null nếu không có giá trị
                        db_taikhoandangnhap = dr["DB_TAIKHOANDANGNHAP"].ToString(),
                        db_ngay = dr["DB_NGAY"] as DateTime?,  // Trả về null nếu không có giá trị
                        dm_donvi_lamviec_id = dr["DM_DONVI_LAMVIEC_ID"].ToString(),
                        ht_vaitro_id = dr["HT_VAITRO_ID"].ToString(),
                        sign_alias = dr["SIGN_ALIAS"].ToString(),
                        sign_username = dr["SIGN_USERNAME"].ToString(),
                        sign_password = null,
                        hrms_type = int.TryParse(dr["SMART"].ToString(), out int hrmsType) ? hrmsType : (int?)null,  // Trả về null nếu không thể parse
                        sign_image = dr["SIGN_IMAGE"].ToString(),
                        anhchukynhay = dr["ANHCHUKYNHAY"].ToString(),
                        roleid = dr["ROLEID"].ToString(),
                        phong_ban = dr["PHONG_BAN"].ToString(),
                        anhdaidien = dr["ANHDAIDIEN"].ToString(),
                        value_token = dr["VALUE_TOKEN"].ToString()


                    };

                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }



        public HT_NGUOIDUNG_Model Update_HT_NGUOIDUNG(HT_NGUOIDUNG_Model data)
        {
            using (OracleConnection cn = new ConnectionOracle().getConnection())
            {
                cn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    using (OracleTransaction transaction = cn.BeginTransaction())
                    {
                        cmd.Connection = cn;
                        cmd.Transaction = transaction;

                        try
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = @"PKG_QLTN_QUANTRI.update_HT_NGUOIDUNG";
                            cmd.Parameters.Add("p_ID", OracleDbType.Varchar2).Value = data.id;
                            cmd.Parameters.Add("p_DM_DONVI_ID", OracleDbType.Varchar2).Value = data.dm_donvi_id;
                            cmd.Parameters.Add("p_DM_PHONGBAN_ID", OracleDbType.Varchar2).Value = data.dm_phongban_id;
                            cmd.Parameters.Add("p_DM_KIEUCANBO_ID", OracleDbType.Varchar2).Value = data.dm_kieucanbo_id;
                            cmd.Parameters.Add("p_DM_CHUCVU_ID", OracleDbType.Varchar2).Value = data.dm_chucvu_id;
                            cmd.Parameters.Add("p_TEN_DANG_NHAP", OracleDbType.Varchar2).Value = data.ten_dang_nhap;
                            cmd.Parameters.Add("p_MAT_KHAU", OracleDbType.Varchar2).Value = null;
                            cmd.Parameters.Add("p_HO_TEN", OracleDbType.Varchar2).Value = data.ho_ten;
                            cmd.Parameters.Add("p_EMAIL", OracleDbType.Varchar2).Value = data.email;
                            cmd.Parameters.Add("p_LDAP", OracleDbType.Varchar2).Value = data.ldap;
                            cmd.Parameters.Add("p_TRANG_THAI", OracleDbType.Int32).Value = data.trang_thai;
                            cmd.Parameters.Add("p_NGAY_TAO", OracleDbType.Date).Value = data.ngay_tao != DateTime.MinValue ? (object)data.ngay_tao : DBNull.Value;
                            cmd.Parameters.Add("p_NGUOI_TAO", OracleDbType.Varchar2).Value = data.nguoi_tao;
                            cmd.Parameters.Add("p_NGAY_CAP_NHAT", OracleDbType.Date).Value = data.ngay_cap_nhat != DateTime.MinValue ? (object)data.ngay_cap_nhat : DBNull.Value;
                            cmd.Parameters.Add("p_NGUOI_CAP_NHAT", OracleDbType.Varchar2).Value = data.nguoi_cap_nhat;
                            cmd.Parameters.Add("p_SO_DIEN_THOAI", OracleDbType.Varchar2).Value = data.so_dien_thoai;
                            cmd.Parameters.Add("p_GIOI_TINH", OracleDbType.Int32).Value = data.gioi_tinh;
                            cmd.Parameters.Add("p_SO_CMND", OracleDbType.Varchar2).Value = data.so_cmnd;
                            cmd.Parameters.Add("p_TRANG_THAI_DONG_BO", OracleDbType.Int32).Value = data.trang_thai_dong_bo;
                            cmd.Parameters.Add("p_DB_TAIKHOANDANGNHAP", OracleDbType.Varchar2).Value = data.db_taikhoandangnhap;
                            cmd.Parameters.Add("p_DB_NGAY", OracleDbType.Date).Value = data.db_ngay != DateTime.MinValue ? (object)data.db_ngay : DBNull.Value;
                            cmd.Parameters.Add("p_DM_DONVI_LAMVIEC_ID", OracleDbType.Varchar2).Value = data.dm_donvi_lamviec_id;
                            cmd.Parameters.Add("p_HT_VAITRO_ID", OracleDbType.Varchar2).Value = data.ht_vaitro_id;
                            cmd.Parameters.Add("p_SIGN_ALIAS", OracleDbType.Varchar2).Value = data.sign_alias;
                            cmd.Parameters.Add("p_SIGN_USERNAME", OracleDbType.Varchar2).Value = data.sign_username;
                            cmd.Parameters.Add("p_SIGN_PASSWORD", OracleDbType.Varchar2).Value = data.sign_password;
                            cmd.Parameters.Add("p_HRMS_TYPE", OracleDbType.Int32).Value = data.hrms_type; // Đảm bảo giá trị là số nguyên
                            cmd.Parameters.Add("p_SIGN_IMAGE", OracleDbType.Varchar2).Value = data.sign_image;
                            cmd.Parameters.Add("p_ANHCHUKYNHAY", OracleDbType.Varchar2).Value = data.anhchukynhay;
                            cmd.Parameters.Add("p_ROLEID", OracleDbType.Varchar2).Value = data.roleid;
                            cmd.Parameters.Add("p_PHONG_BAN", OracleDbType.Varchar2).Value = data.phong_ban;
                            cmd.Parameters.Add("p_ANHDAIDIEN", OracleDbType.Varchar2).Value = data.anhdaidien;
                            cmd.Parameters.Add("p_VALUE_TOKEN", OracleDbType.Varchar2).Value = data.value_token;



                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            return data;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }


        public List<UserResponse> FILTER_HT_NGUOIDUNG(UserFilterRequest request, out int totalRecords)
        {
            OracleConnection cn = new ConnectionOracle().getConnection();
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"PKG_QLTN_QUANTRI.search_HT_NGUOIDUNG";

                cmd.Parameters.Add("p_HO_TEN", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(request.HO_TEN) ? (object)DBNull.Value : request.HO_TEN;
                cmd.Parameters.Add("p_TEN_DANG_NHAP", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(request.TEN_DANG_NHAP) ? (object)DBNull.Value : request.TEN_DANG_NHAP;
                cmd.Parameters.Add("p_TRANG_THAI", OracleDbType.Int32).Value = request.TRANG_THAI != -1 ? (object)request.TRANG_THAI.Value : DBNull.Value;
                cmd.Parameters.Add("p_DM_DONVI_ID", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(request.DM_DONVI_ID) ? (object)DBNull.Value : request.DM_DONVI_ID;
                cmd.Parameters.Add("p_DM_PHONGBAN_ID", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(request.DM_PHONGBAN_ID) ? (object)DBNull.Value : request.DM_PHONGBAN_ID;
                cmd.Parameters.Add("p_DM_CHUCVU_ID", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(request.DM_CHUCVU_ID) ? (object)DBNull.Value : request.DM_CHUCVU_ID;

                cmd.Parameters.Add("p_pageNumber", OracleDbType.Int32).Value = request.PageIndex;
                cmd.Parameters.Add("p_pageSize", OracleDbType.Int32).Value = request.PageSize;
                cmd.Parameters.Add("p_MA_DVIQLY", OracleDbType.Varchar2).Value = request.ma_dviqly;
                cmd.Parameters.Add("p_totalRecords", OracleDbType.Decimal).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                //bat dau thuc hien truy van
                OracleDataAdapter dap = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);

                // Lấy tổng số bản ghi từ tham số out
                if (cmd.Parameters["p_totalRecords"].Value != DBNull.Value)
                {
                    var oracleDecimalValue = (Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters["p_totalRecords"].Value;
                    totalRecords = oracleDecimalValue.ToInt32();
                }
                else
                {
                    totalRecords = 0;
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    List<UserResponse> results = new List<UserResponse>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserResponse result = new UserResponse
                        {
                            ID = dr["ID"] != DBNull.Value ? dr["ID"].ToString() : null,
                            HO_TEN = dr["HO_TEN"] != DBNull.Value ? dr["HO_TEN"].ToString() : null,
                            TEN_DANG_NHAP = dr["TEN_DANG_NHAP"] != DBNull.Value ? dr["TEN_DANG_NHAP"].ToString() : null,
                            TRANG_THAI = dr["TRANG_THAI"] != DBNull.Value ? Convert.ToInt32(dr["TRANG_THAI"]) : 0,
                            TEN_DONVI = dr["TEN_DONVI"] != DBNull.Value ? dr["TEN_DONVI"].ToString() : null,
                            TEN_PHONGBAN = dr["TEN_PHONGBAN"] != DBNull.Value ? dr["TEN_PHONGBAN"].ToString() : null,
                            TEN_CHUCVU = dr["TEN_CHUCVU"] != DBNull.Value ? dr["TEN_CHUCVU"].ToString() : null,
                            EMAIL = dr["EMAIL"] != DBNull.Value ? dr["EMAIL"].ToString() : null,
                        };
                        results.Add(result);
                    }
                    return results;
                }
                else
                {
                    totalRecords = 0;
                    return new List<UserResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            finally
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }



        //Đổi mật khẩu người dùng 
        public string Reset_Password_HT_NGUOIDUNG(string ID, string currentPassword, string newPassword)
        {
            OracleConnection cn = new ConnectionOracle().getConnection();
            cn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            OracleTransaction transaction = cn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                string hashedPasswordFromDB = Get_HT_NGUOIDUNG_Password(ID);
                if (!PasswordHasher.VerifyHashedString(hashedPasswordFromDB, currentPassword))
                {
                    return "Mật khẩu hiện tại không chính xác.";
                }

                string hashedNewPassword = PasswordHasher.HashString(newPassword);
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"PKG_QLTN_QUANTRI.update_password_HT_NGUOIDUNG";
                cmd.Parameters.Add("p_USER_ID", ID);
                cmd.Parameters.Add("p_NEW_PASSWORD", hashedNewPassword);
                cmd.Parameters.Add("p_Error", OracleDbType.NVarchar2, 200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return "Cập nhật mật khẩu thành công.";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Có lỗi xảy ra: {ex.Message}";
            }
            finally
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }


        public string Get_HT_NGUOIDUNG_Password(string ID)
        {
            string password = null;
            string strErr = "";
            OracleConnection cn = new ConnectionOracle().getConnection();
            cn.Open();
            if (strErr != null && strErr != "")
            {
                return null;
            }
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                OracleDataAdapter dap = new OracleDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = @"PKG_QLTN_QUANTRI.get_HT_NGUOIDUNG_byID";
                cmd.Parameters.Add("p_ID", ID);
                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                dap.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dap.Fill(ds);
                Console.WriteLine(ds);
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    password = ds.Tables[0].Rows[0]["MAT_KHAU"].ToString();
                }
                return password;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }


        //delete nguoi dung
        public void Delete_HT_NGUOIDUNG(string id)
        {
            string strErr = "";
            using (OracleConnection cn = new ConnectionOracle().getConnection())
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                OracleTransaction transaction = cn.BeginTransaction();
                cmd.Transaction = transaction;

                try
                {
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"PKG_QLTN_QUANTRI.delete_HT_NGUOIDUNG";

                    cmd.Parameters.Add("p_ID", OracleDbType.Varchar2).Value = id;
                    cmd.Parameters.Add("p_Error", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex; // Hoặc xử lý lỗi theo cách bạn muốn
                }
                finally
                {
                    if (cn.State != ConnectionState.Closed)
                    {
                        cn.Close();
                    }
                }
            }
        }
        public void updateTrangThai_NguoiDung(string id, int trangthainew)
        {
            string strErr = "";
            using (OracleConnection cn = new ConnectionOracle().getConnection())
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                OracleTransaction transaction = cn.BeginTransaction();
                cmd.Transaction = transaction;

                try
                {
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"PKG_QLTN_QUANTRI.update_TT_NGUOIDUNG";

                    cmd.Parameters.Add("p_ID", OracleDbType.Varchar2).Value = id;
                    cmd.Parameters.Add("p_TRANG_THAI", OracleDbType.Int32).Value = trangthainew;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex; // Hoặc xử lý lỗi theo cách bạn muốn
                }
                finally
                {
                    if (cn.State != ConnectionState.Closed)
                    {
                        cn.Close();
                    }
                }
            }
        }



        public List<HT_MENU_Model> get_HT_MENU_ByIDUser(string userId)
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_HT_MENUByUserId", "p_UserID", userId);
                List<HT_MENU_Model> results = new List<HT_MENU_Model>();

                if (tb != null)
                {
                    // Tạo danh sách tất cả các menu
                    foreach (DataRow row in tb.Rows)
                    {
                        HT_MENU_Model model = new HT_MENU_Model
                        {
                            id = row["ID"] != DBNull.Value ? int.Parse(row["ID"].ToString()) : (int?)null,
                            ten_menu = row["TEN_MENU"] != DBNull.Value ? row["TEN_MENU"].ToString() : null,
                            ghi_chu = row["GHI_CHU"] != DBNull.Value ? row["GHI_CHU"].ToString() : null,
                            ngay_tao = row["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(row["NGAY_TAO"].ToString()) : (DateTime?)null,
                            nguoi_tao = row["NGUOI_TAO"] != DBNull.Value ? row["NGUOI_TAO"].ToString() : null,
                            ngay_sua = row["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(row["NGAY_SUA"].ToString()) : (DateTime?)null,
                            nguoi_sua = row["NGUOI_SUA"] != DBNull.Value ? row["NGUOI_SUA"].ToString() : null,
                            duong_dan = row["DUONG_DAN"] != DBNull.Value ? row["DUONG_DAN"].ToString() : null,
                            parent_id = row["PARENT_ID"] != DBNull.Value ? int.Parse(row["PARENT_ID"].ToString()) : (int?)null,
                            icon = row["ICON"] != DBNull.Value ? row["ICON"].ToString() : null,
                            sap_xep = row["SAP_XEP"] != DBNull.Value ? int.Parse(row["SAP_XEP"].ToString()) : (int?)null,

                        };
                        results.Add(model);
                    }
                }

                return BuildMenuTree(results);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }
        public List<HT_MENU_Model> BuildMenuTree(List<HT_MENU_Model> menus, int? parentId = null)
        {
            if (menus == null || !menus.Any()) // Kiểm tra danh sách null hoặc rỗng
                return new List<HT_MENU_Model>();

            return menus
                .Where(m => m.parent_id == parentId)
                .Select(m => new HT_MENU_Model
                {
                    id = m.id,
                    ten_menu = m.ten_menu,
                    ghi_chu = m.ghi_chu,
                    ngay_tao = m.ngay_tao,
                    nguoi_tao = m.nguoi_tao,
                    ngay_sua = m.ngay_sua,
                    nguoi_sua = m.nguoi_sua,
                    duong_dan = m.duong_dan,
                    parent_id = m.parent_id,
                    icon = m.icon,
                    sap_xep = m.sap_xep,
                    children = BuildMenuTree(menus, m.id) ?? new List<HT_MENU_Model>() // Đảm bảo không trả về null
                })
                .ToList();
        }





    }


}


