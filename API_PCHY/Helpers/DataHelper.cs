using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.SqlClient;

namespace APIPCHY.Helpers
{
    public class DataHelper
    {
        //string connectionString = "User Id=QLKC;Password=qlkc;Data Source=117.0.33.2:1522/QLKC";
        OracleConnection cn;

        /// <summary>
        /// Khởi tạo DataHelper với chuỗi kết nối tùy chỉnh
        /// </summary>
        /// <param name="conn">Chuỗi kết nối đến Oracle database</param>
        public DataHelper(string conn)
        {
            cn = new OracleConnection(conn);

        }

        /// <summary>
        /// Khởi tạo DataHelper với chuỗi kết nối mặc định
        /// </summary>
        public DataHelper()
        {
            cn = new ConnectionOracle().getConnection();
        }

        /// <summary>
        /// Mở kết nối đến database
        /// </summary>
        /// <returns>true nếu mở kết nối thành công, false nếu thất bại</returns>
        public bool Open()
        {
            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Đóng kết nối database
        /// </summary>
        public void Close()
        {
            if (cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }
        }

        /// <summary>
        /// Thực thi stored procedure không trả về dữ liệu
        /// </summary>
        /// <param name="procedureName">Tên stored procedure</param>
        /// <param name="paramOut">Tên tham số output</param>
        /// <param name="param_list">Danh sách các tham số đầu vào theo cặp (tên tham số, giá trị)</param>
        /// <returns>Chuỗi rỗng nếu thành công, chuỗi bao gồm lỗi nếu thất bại</returns>
        public string ExcuteNonQuery(string procedureName, string paramOut, params object[] param_list)
        {

            OracleCommand cmd = new OracleCommand { CommandText = procedureName, CommandType = CommandType.StoredProcedure, Connection = cn };

            OracleTransaction transaction;
            Open();
            string strErr = "";
            transaction = cn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                int paramInput = (param_list.Length) / 2;
                cmd.Parameters.Clear();
                for (int i = 0; i < paramInput; i++)
                {
                    string paramKey = Convert.ToString(param_list);
                    object paramValue = param_list[i + paramInput];
                    // Kiểm tra nếu tham số là null, thay thế bằng DBNull.Value
                    if (paramValue == null)
                    {
                        paramValue = DBNull.Value;
                    }

                    // Kiểm tra nếu tham số là kiểu CLOB hoặc BLOB, cần thêm OracleDbType tương ứng
                    if (paramValue is string)
                    {
                        cmd.Parameters.Add(new OracleParameter(paramKey, OracleDbType.NClob) { Value = paramValue });
                    }
                    else
                    {
                        cmd.Parameters.Add(new OracleParameter(paramKey, paramValue));
                    }
                }
                cmd.Parameters.Add(paramOut, OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception err)
            {
                strErr = err.ToString();
                transaction.Rollback();
            }
            finally
            {
                Close();
            }

            return strErr;
        }

        /// <summary>
        /// Thực thi stored procedure và trả về dữ liệu dạng DataTable
        /// </summary>
        /// <param name="ProcedureName">Tên stored procedure</param>
        /// <param name="param_list">Danh sách các tham số đầu vào theo cặp (tên tham số, giá trị)</param>
        /// <returns>DataTable chứa dữ liệu kết quả, null nếu có lỗi hoặc không có dữ liệu</returns>
        //public DataTable ExcuteReaders(string ProcedureName, params object[] param_list)
        //{
        //    DataTable tb = new DataTable();
        //    try
        //    {
        //        OracleCommand cmd = new OracleCommand { CommandType = CommandType.StoredProcedure, CommandText = ProcedureName, Connection = cn };
        //        Open();
        //        int paramterInput = param_list.Length / 2;

        //        for (int i = 0; i < paramterInput; i++)
        //        {
        //            string paramName = Convert.ToString(param_list[i * 2]);
        //            object paramValue = param_list[i * 2 + 1];

        //            // Kiểm tra tham số JSON và Clob
        //            if (paramName.ToLower().Contains("json"))
        //            {
        //                cmd.Parameters.Add(new OracleParameter
        //                {
        //                    ParameterName = paramName,
        //                    OracleDbType = OracleDbType.NVarchar2, // Dữ liệu kiểu JSON
        //                    Value = paramValue ?? DBNull.Value
        //                });
        //            }
        //            else if (paramName.ToLower().Contains("nclob"))
        //            {
        //                cmd.Parameters.Add(new OracleParameter
        //                {
        //                    ParameterName = paramName,
        //                    OracleDbType = OracleDbType.Clob, // Dữ liệu kiểu CLOB
        //                    Value = paramValue ?? DBNull.Value
        //                });
        //            }
        //            else
        //            {
        //                cmd.Parameters.Add(new OracleParameter(paramName, paramValue ?? DBNull.Value));
        //            }
        //        }

        //        // Thêm tham số RefCursor để nhận kết quả
        //        OracleParameter refCursor = new OracleParameter
        //        {
        //            ParameterName = "p_getDB",
        //            OracleDbType = OracleDbType.RefCursor,
        //            Direction = ParameterDirection.Output
        //        };
        //        cmd.Parameters.Add(refCursor);

        //        // Thực thi và lấy kết quả
        //        OracleDataAdapter ad = new OracleDataAdapter(cmd);
        //        ad.Fill(tb);
        //        ad.Dispose();
        //        cmd.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        return tb;
        //        // Log lỗi nếu cần
        //    }
        //    finally
        //    {
        //        Close();
        //    }

        //    return tb; // Trả về DataTable chứa dữ liệu


        //}



        public DataTable ExcuteReader(string ProcedureName, params object[] param_list)
        {
            DataTable tb = new DataTable();
            try
            {
                OracleCommand cmd = new OracleCommand { CommandType = CommandType.StoredProcedure, CommandText = ProcedureName, Connection = cn };
                Open();
                int paramterInput = (param_list.Length) / 2;
                for (int i = 0; i < paramterInput; i++)
                {
                    string paramName = Convert.ToString(param_list[i]);
                    object paramValue = param_list[i + paramterInput];
                    if (paramName.ToLower().Contains("json"))
                    {
                        cmd.Parameters.Add(new OracleParameter
                        {
                            ParameterName = paramName,
                            OracleDbType = OracleDbType.NVarchar2,
                            Value = paramValue ?? DBNull.Value
                        });
                    }
                    else if (paramName.ToLower().Contains("nclob"))
                    {
                        cmd.Parameters.Add(new OracleParameter
                        {
                            ParameterName = paramName,
                            OracleDbType = OracleDbType.Clob, // Dữ liệu kiểu CLOB
                            Value = paramValue ?? DBNull.Value
                        });
                    }
                    else
                    {
                        cmd.Parameters.Add(new OracleParameter(paramName, paramValue ?? DBNull.Value));
                    }
                }

                OracleParameter refCursor = new OracleParameter
                {
                    ParameterName = "p_getDB",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(refCursor);


                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                ad.Fill(tb);
                ad.Dispose();
                cmd.Dispose();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                tb = null;
                // Log lỗi nếu cần
            }
            finally
            {
                Close();
            }
            return tb;
        }
    }
}
    
