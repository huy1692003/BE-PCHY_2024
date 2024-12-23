using APIPCHY.Helpers;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System;
using System.Diagnostics;
using APIPCHY_PhanQuyen.Models.QLKC.HT_MENU;

namespace API_PCHY.Models.QLTN.DM_LOAI_TAI_SAN
{
    public class DM_LOAI_TAI_SAN_Manager
    {
        DataHelper _dbHelper = new DataHelper();

        
        /***
        ** TIM KIEM: LOAI TAI SAN 
        */
        public List<DM_LOAI_TAI_SAN_Model> SearchDMLoaiTaiSan(string search, int page, int pageSize, out int totalCount)
        {
            totalCount = 0;
            OracleConnection cn = new ConnectionOracle().getConnection();
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = @"PKG_QLTN_TANH.search_DM_LOAI_TAI_SAN"
                };

                cmd.Parameters.Add("p_search", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(search) ? (object)DBNull.Value : search;
                cmd.Parameters.Add("p_page", OracleDbType.Int32).Value = page;
                cmd.Parameters.Add("p_page_size", OracleDbType.Int32).Value = pageSize;

                cmd.Parameters.Add("p_total_count", OracleDbType.Decimal).Direction = ParameterDirection.Output; 
                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                // Thực hiện truy vấn
                OracleDataAdapter dap = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);

                if (cmd.Parameters["p_total_count"].Value != DBNull.Value)
                {
                    var oracleDecimalValue = (Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters["p_total_count"].Value;
                    totalCount = oracleDecimalValue.ToInt32();
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    List<DM_LOAI_TAI_SAN_Model> results = new List<DM_LOAI_TAI_SAN_Model>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DM_LOAI_TAI_SAN_Model item = new DM_LOAI_TAI_SAN_Model
                        {
                            id = dr["ID"] != DBNull.Value && int.TryParse(dr["ID"].ToString(), out int idValue) ? (int?)idValue : null,
                            ten_lts = dr["TEN_LTS"] != DBNull.Value ? dr["TEN_LTS"].ToString() : null,
                            ngay_tao = dr["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(dr["NGAY_TAO"]) : (DateTime?)null,
                            nguoi_tao = dr["NGUOI_TAO"] != DBNull.Value ? dr["NGUOI_TAO"].ToString() : null,
                            ghi_chu = dr["GHI_CHU"] != DBNull.Value ? dr["GHI_CHU"].ToString() : null
                        };
                        results.Add(item);
                    }
                    return results;
                }
                else
                {
                    totalCount = 0;
                    return new List<DM_LOAI_TAI_SAN_Model>();
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

        /***
         * GET LOAI TAI SAN BY ID
         */
        public DM_LOAI_TAI_SAN_Model get_DM_LOAI_TAI_SAN_BYID()
        {
            OracleConnection cn = new ConnectionOracle().getConnection();
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = @"PKG_QLTN_TANH.get_DM_LOAI_TAI_SAN"
                };

                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter dap = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    return new DM_LOAI_TAI_SAN_Model
                    {
                        id = dr["ID"] != DBNull.Value ? int.TryParse(dr["ID"].ToString(), out var idValue) ? (int?)idValue : null : null,
                        ten_lts = dr["TEN_LTS"] != DBNull.Value ? dr["TEN_LTS"].ToString() : null,
                        ngay_tao = dr["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(dr["NGAY_TAO"]) : (DateTime?)null,
                        nguoi_tao = dr["NGUOI_TAO"] != DBNull.Value ? dr["NGUOI_TAO"].ToString() : null,
                        ghi_chu = dr["GHI_CHU"] != DBNull.Value ? dr["GHI_CHU"].ToString() : null
                    };
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


        /***
         * INSERT LOAI TAI SAN
         */
        //public string insert_DM_LOAI_TAI_SAN(DM_LOAI_TAI_SAN_Model model)
        //{
        //    try
        //    {
        //        string result = _dbHelper.ExcuteNonQuery(
        //                                                "PKG_QLTN_TANH.insert_DM_LOAI_TAI_SAN", "p_Error",
        //                                                "p_TEN_LTS", model.ten_lts,
        //                                                "p_NGUOI_TAO", model.nguoi_tao ,
        //                                                "p_GHI_CHU", model.ghi_chu
        //        );

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi trong quá trình thêm mới loại tài sản: " + ex.Message, ex);
        //    }
        //}
        public string Insert_DM_LOAI_TAI_SAN(DM_LOAI_TAI_SAN_Model model)
        {
            using (OracleConnection cn = new ConnectionOracle().getConnection())
            {
                cn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PKG_QLTN_TANH.insert_DM_LOAI_TAI_SAN";

                    // Thêm tham số
                    cmd.Parameters.Add("p_TEN_LTS", OracleDbType.Varchar2).Value = model.ten_lts ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_NGUOI_TAO", OracleDbType.Varchar2).Value = model.nguoi_tao ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_GHI_CHU", OracleDbType.Varchar2).Value = model.ghi_chu ?? (object)DBNull.Value;

                    // Tham số đầu ra
                    cmd.Parameters.Add("p_Error", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.ExecuteNonQuery();

                        return "Them thanh cong";
                      
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Lỗi khi thực thi: {ex.Message}", ex);
                    }
                }
            }
        }





        /***
         * UPDATE DM LOAI TAI SAN
         */
        //public string update_DM_LOAI_TAI_SAN(DM_LOAI_TAI_SAN_Model model)
        //{
        //    try
        //    {
        //        if (model.id <= 0)
        //        {
        //            throw new Exception("ID phải là số nguyên dương.");
        //        }

        //        string result = _dbHelper.ExcuteNonQuery(
        //            "PKG_QLTN_TANH.update_DM_LOAI_TAI_SAN",
        //            "p_Error",
        //            "p_ID", model.id,
        //            "p_TEN_LTS", string.IsNullOrEmpty(model.ten_lts) ? DBNull.Value : model.ten_lts,
        //            "p_GHI_CHU", string.IsNullOrEmpty(model.ghi_chu) ? DBNull.Value : model.ghi_chu,
        //            "p_NGUOI_SUA", string.IsNullOrEmpty(model.nguoi_sua) ? DBNull.Value : model.nguoi_sua
        //        );

        //        if (!string.IsNullOrEmpty(result))
        //        {
        //            return $"Sửa thất bại: {result}";
        //        }

        //        return "Cập nhật thành công.";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw new Exception("Lỗi trong quá trình cập nhật loại tài sản: " + ex.Message, ex);
        //    }
        //}

        public DM_LOAI_TAI_SAN_Model Update_DM_LOAI_TAI_SAN(DM_LOAI_TAI_SAN_Model data)
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
                            cmd.CommandText = @"PKG_QLTN_TANH.update_DM_LOAI_TAI_SAN";

                            cmd.Parameters.Add("p_ID", OracleDbType.Int32).Value = data.id;
                            cmd.Parameters.Add("p_TEN_LTS", OracleDbType.Varchar2).Value = !string.IsNullOrEmpty(data.ten_lts) ? data.ten_lts : DBNull.Value;
                            cmd.Parameters.Add("p_GHI_CHU", OracleDbType.Varchar2).Value = !string.IsNullOrEmpty(data.ghi_chu) ? data.ghi_chu : DBNull.Value;
                            cmd.Parameters.Add("p_NGUOI_SUA", OracleDbType.Varchar2).Value = !string.IsNullOrEmpty(data.nguoi_sua) ? data.nguoi_sua : DBNull.Value;

                            cmd.Parameters.Add("p_Error", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

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




        /***
         * XOA DM LOAI TAI SAN
         */

        public string delete_DM_LOAI_TAI_SAN_ByID(int id)
        {
            try
            {
                string result = _dbHelper.ExcuteNonQuery("PKG_QLTN_TANH.delete_DM_LOAI_TAI_SAN_ByID", "p_Error",
                       "p_ID",
                       id
                        );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}
