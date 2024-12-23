using APIPCHY.Helpers;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System;
using APIPCHY_PhanQuyen.Models.QLKC.HT_QUYEN_NGUOIDUNG;

namespace APIPCHY_PhanQuyen.Models.QLTN.HT_QUYEN_NGUOIDUNG
{
    public class HT_QUYEN_NGUOIDUNG_Manager
    {
        DataHelper helper = new DataHelper();
       
        public HT_QUYEN_NGUOIDUNG_Model Insert_HT_QUYEN_NGUOIDUNG(HT_QUYEN_NGUOIDUNG_Model qnd)
        {
            string strErr = "";
            OracleConnection cn = new ConnectionOracle().getConnection();
            cn.Open();
            if (strErr != null && strErr != "")
            {
                return null;
            }
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            OracleTransaction transaction;
            transaction = cn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"PKG_QLTN_QUANTRI.grant_HT_QUYEN_NGUOIDUNG";
                cmd.Parameters.Add("p_MA_NGUOI_DUNG", qnd.MA_NGUOI_DUNG);
                cmd.Parameters.Add("p_NHOM_QUYEN_ID", qnd.MA_NHOM_TV);
                cmd.Parameters.Add("p_Error", OracleDbType.NVarchar2, 200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return qnd;
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

       

        public void Delete_HT_QUYEN_NGUOIDUNG(int id)
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
                    cmd.CommandText = @"PKG_QLTN_QUANTRI.delete_HT_QUYEN_NGUOIDUNG";

                    cmd.Parameters.Add("p_ID", OracleDbType.Int32).Value = id;
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
        public List<object> Get_QUYEN_NGUOIDUNG_BY_USERID(string maNguoiDung)
        {
            List<object> result = new List<object>();
            OracleConnection cn = new ConnectionOracle().getConnection();
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = @"PKG_QLTN_QUANTRI.get_NHOMQUYEN_BY_NGUOIDUNG_ID"
                };
                cmd.Parameters.Add("p_MA_NGUOI_DUNG", OracleDbType.Varchar2).Value = maNguoiDung;
                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (OracleDataAdapter dap = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    dap.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var items = new
                        {
                            ID=dr.Field<decimal>("ID"),
                            IDNhomQuyen = dr.Field<string>("NHOM_ID"),
                            TenNhomQuyen = dr.Field<string>("TEN_NHOM"),
                            TenDonVi = dr.Field<string>("TEN_DVIQLY"),
                        };
                        result.Add(items);
                    }
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
            return result;
        }

    }

}
