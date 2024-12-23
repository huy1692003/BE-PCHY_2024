using API_PCHY.Models.QLTN.DM_KHACH_HANG;
using API_PCHY.Models.QLTN.DM_LOAI_YCTN;
using APIPCHY.Helpers;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API_PCHY.Models.QLTN.DM_LOAI_BIENBAN
{
    public class DM_LOAI_BIENBAN_Manager
    {
        DataHelper _helper = new DataHelper();

        //insert 
        public bool insert_DM_LOAI_BIENBAN(DM_LOAI_BIENBAN_Model model) {
            try {
                string result = _helper.ExcuteNonQuery("PKG_QLTN_TANH.insert_DM_LOAI_BIENBAN", "p_Error",
                    "p_TEN_LOAI_BB", "p_NGUOI_TAO", "p_GHI_CHU",
                    model.ten_loai_bb, model.nguoi_tao, model.ghi_chu);
                return string.IsNullOrEmpty(result);
            } 
            catch (Exception) {
                return false;
            }
        }


        public bool update_DM_LOAI_BIENBAN(DM_LOAI_BIENBAN_Model model)
        {
            try
            {
                string result = _helper.ExcuteNonQuery("PKG_QLTN_TANH.update_DM_LOAI_BIENBAN", "p_Error",
                    "p_ID", "p_TEN_LOAI_BB", "p_NGUOI_SUA", "p_GHI_CHU",
                    model.id, model.ten_loai_bb, model.nguoi_sua, model.ghi_chu);

                if (!string.IsNullOrEmpty(result))
                {
                    throw new Exception($"Error from DB: {result}");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

        public bool delete_DM_LOAI_BIENBAN(int id)
        {
            try
            {
                string result = _helper.ExcuteNonQuery("PKG_QLTN_TANH.delete_DM_LOAI_BIENBAN", "p_Error",
                    "p_ID", id);
                return string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<DM_LOAI_BIENBAN_Model> search_DM_LOAI_BIENBAN(DM_LOAI_BIENBAN_Request request, out int totalRecords, out int totalPages)
        {
            OracleConnection cn = new ConnectionOracle().getConnection();
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"PKG_QLTN_TANH.search_DM_LOAI_BIENBAN"; 

                cmd.Parameters.Add("p_search", OracleDbType.Varchar2).Value = request.searchData ?? string.Empty; 
                cmd.Parameters.Add("p_page", OracleDbType.Int32).Value = request.pageIndex;  
                cmd.Parameters.Add("p_pageSize", OracleDbType.Int32).Value = request.pageSize;

                cmd.Parameters.Add("p_totalRecords", OracleDbType.Decimal).Direction = ParameterDirection.Output; 
                cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output; 

                OracleDataAdapter dap = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);

                totalRecords = 0;
                if (cmd.Parameters["p_totalRecords"].Value != DBNull.Value)
                {
                    var oracleDecimalValue = (Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters["p_totalRecords"].Value;
                    totalRecords = oracleDecimalValue.ToInt32();
                }

                totalPages = (int)Math.Ceiling((double)totalRecords / request.pageSize);

                List<DM_LOAI_BIENBAN_Model> results = new List<DM_LOAI_BIENBAN_Model>();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var result = new DM_LOAI_BIENBAN_Model
                        {
                            id = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : (int?)null,
                            ten_loai_bb = dr["TEN_LOAI_BB"] != DBNull.Value ? dr["TEN_LOAI_BB"].ToString() : null,
                            ngay_tao = dr["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(dr["NGAY_TAO"]) : (DateTime?)null,
                            nguoi_tao = dr["NGUOI_TAO"] != DBNull.Value ? dr["NGUOI_TAO"].ToString() : null,
                            ngay_sua = dr["NGAY_SUA"] != DBNull.Value ? Convert.ToDateTime(dr["NGAY_SUA"]) : (DateTime?)null,
                            nguoi_sua = dr["NGUOI_SUA"] != DBNull.Value ? dr["NGUOI_SUA"].ToString() : null,
                            ghi_chu = dr["GHI_CHU"] != DBNull.Value ? dr["GHI_CHU"].ToString() : null
                        };
                        results.Add(result);
                    }
                }
                else
                {
                    results = new List<DM_LOAI_BIENBAN_Model>();
                }

                return results;
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

        public List<DM_LOAI_BIENBAN_Model> get_All_DM_LOAI_BIENBAN()
        {
            try
            {
                DataTable ds = _helper.ExcuteReader("PKG_QLTN_TANH.get_all_DM_LOAI_BIENBAN");

                List<DM_LOAI_BIENBAN_Model> list = new List<DM_LOAI_BIENBAN_Model>();

                if (ds != null && ds.Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        var model = new DM_LOAI_BIENBAN_Model
                        {
                            id = ds.Rows[i]["ID"] != DBNull.Value ? Convert.ToInt32(ds.Rows[i]["ID"]) : (int?)null,
                            ten_loai_bb = ds.Rows[i]["TEN_LOAI_BB"] != DBNull.Value ? ds.Rows[i]["TEN_LOAI_BB"].ToString() : null,
                            ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? Convert.ToDateTime(ds.Rows[i]["NGAY_TAO"]) : (DateTime?)null,
                            nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null,
                            ngay_sua = ds.Rows[i]["NGAY_SUA"] != DBNull.Value ? Convert.ToDateTime(ds.Rows[i]["NGAY_SUA"]) : (DateTime?)null,
                            nguoi_sua = ds.Rows[i]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[i]["NGUOI_SUA"].ToString() : null,
                            ghi_chu = ds.Rows[i]["GHI_CHU"] != DBNull.Value ? ds.Rows[i]["GHI_CHU"].ToString() : null
                        };

                        list.Add(model);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}