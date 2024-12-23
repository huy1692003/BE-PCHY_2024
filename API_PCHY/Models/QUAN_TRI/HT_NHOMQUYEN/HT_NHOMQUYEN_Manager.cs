using APIPCHY.Helpers;
using APIPCHY_PhanQuyen.Models.QLKC.HT_NHOMQUYEN;
using APIPCHY_PhanQuyen.Models.QLTN.HT_MENU;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace APIPCHY_PhanQuyen.Models.QLTN.HT_NHOMQUYEN
{
    public class HT_NHOMQUYEN_Manager
    {
        DataHelper helper = new DataHelper();

        public string create_HT_NHOMQUYEN(HT_NHOMQUYEN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.create_HT_NHOMQUYEN", "p_Error", "p_NHOM_ID",
                                                    "p_GHI_CHU", "p_NGUOI_TAO", "p_CAP_BAC", "p_SAP_XEP", "p_TEN_NHOM",
                                                    "p_MA_DVIQLY", model.nhom_id, model.ghi_chu, model.nguoi_tao, model.cap_bac,
                                                    model.sap_xep, model.ten_nhom, model.ma_dviqly);
                return result;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string update_HT_NHOMQUYEN(HT_NHOMQUYEN_Model model)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.update_HT_NHOMQUYEN", "p_Error", "p_ID",
                                                    "p_GHI_CHU", "p_NGUOI_SUA", "p_CAP_BAC", "p_SAP_XEP", "p_TEN_NHOM",
                                                    "p_MA_DVIQLY", model.id, model.ghi_chu, model.nguoi_sua, model.cap_bac,
                                                    model.sap_xep, model.ten_nhom, model.ma_dviqly);
                return result;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string delete_HT_NHOMQUYEN(int id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.delete_HT_NHOMQUYEN", "p_Error", "p_ID", id);
                return result;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<HT_NHOMQUYEN_Model> search_HT_NHOMQUYEN(int? pageSize, int? pageIndex, string? ten_nhom, string ma_dviqly, out int totalItems)
        {
            totalItems = 0;
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.search_HT_NHOMQUYEN", "p_page_index", "p_page_size",
                                                "p_TEN_NHOM", "p_MA_DVIQLY", pageIndex, pageSize, ten_nhom, ma_dviqly);
                var count = ds.Rows.Count;

                if (pageSize > 0 && pageIndex > 0 && count > 0)
                {
                    totalItems = int.Parse(ds.Rows[0]["RecordCount"].ToString());
                }
                if (ds == null || ds.Rows.Count == 0)
                {
                    totalItems = 0;
                    return new List<HT_NHOMQUYEN_Model>();
                }


                List<HT_NHOMQUYEN_Model> list = new List<HT_NHOMQUYEN_Model>();
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    HT_NHOMQUYEN_Model model = new HT_NHOMQUYEN_Model();
                    model.id = int.Parse(ds.Rows[i]["ID"].ToString());
                    model.nhom_id = ds.Rows[i]["NHOM_ID"] != DBNull.Value ? ds.Rows[i]["NHOM_ID"].ToString() : null;
                    model.ghi_chu = ds.Rows[i]["GHI_CHU"] != DBNull.Value ? ds.Rows[i]["GHI_CHU"].ToString() : null;
                    model.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : null;
                    model.nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null;
                    model.ngay_sua = ds.Rows[i]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_SUA"].ToString()) : null;
                    model.nguoi_sua = ds.Rows[i]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[i]["NGUOI_SUA"].ToString() : null;
                    model.cap_bac = ds.Rows[i]["CAP_BAC"] != DBNull.Value ? int.Parse(ds.Rows[i]["CAP_BAC"].ToString()) : null;
                    model.sap_xep = ds.Rows[i]["SAP_XEP"] != DBNull.Value ? int.Parse(ds.Rows[i]["SAP_XEP"].ToString()) : null;
                    model.ten_nhom = ds.Rows[i]["TEN_NHOM"] != DBNull.Value ? ds.Rows[i]["TEN_NHOM"].ToString() : null;
                    model.ma_dviqly = ds.Rows[i]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[i]["MA_DVIQLY"].ToString() : null;
                    model.ten = ds.Rows[i]["TEN_DVIQLY"] != DBNull.Value ? ds.Rows[i]["TEN_DVIQLY"].ToString() : null;

                    list.Add(model);
                }

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public HT_NHOMQUYEN_Model get_HT_NHOMQUYEN_By_ID(int id)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_HT_NHOMQUYEN_By_ID", "p_ID", id);
                if (ds != null && ds.Rows.Count > 0)
                {
                    HT_NHOMQUYEN_Model model = new HT_NHOMQUYEN_Model();

                    model.id = int.Parse(ds.Rows[0]["ID"].ToString());
                    model.nhom_id = ds.Rows[0]["NHOM_ID"] != DBNull.Value ? ds.Rows[0]["NHOM_ID"].ToString() : null;
                    model.ghi_chu = ds.Rows[0]["GHI_CHU"] != DBNull.Value ? ds.Rows[0]["GHI_CHU"].ToString() : null;
                    model.ngay_tao = ds.Rows[0]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_TAO"].ToString()) : null;
                    model.nguoi_tao = ds.Rows[0]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[0]["NGUOI_TAO"].ToString() : null;
                    model.ngay_sua = ds.Rows[0]["NGAY_SUA"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_SUA"].ToString()) : null;
                    model.nguoi_sua = ds.Rows[0]["NGUOI_SUA"] != DBNull.Value ? ds.Rows[0]["NGUOI_SUA"].ToString() : null;
                    model.cap_bac = ds.Rows[0]["CAP_BAC"] != DBNull.Value ? int.Parse(ds.Rows[0]["CAP_BAC"].ToString()) : null;
                    model.sap_xep = ds.Rows[0]["SAP_XEP"] != DBNull.Value ? int.Parse(ds.Rows[0]["SAP_XEP"].ToString()) : null;
                    model.ten_nhom = ds.Rows[0]["TEN_NHOM"] != DBNull.Value ? ds.Rows[0]["TEN_NHOM"].ToString() : null;
                    model.ma_dviqly = ds.Rows[0]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[0]["MA_DVIQLY"].ToString() : null;
                    model.ten = ds.Rows[0]["TEN_DVIQLY"] != DBNull.Value ? ds.Rows[0]["TEN_DVIQLY"].ToString() : null;

                    return model;
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
        }




        public List<HT_NHOMQUYEN_Model> GET_NHOMQUYEN_BY_MADV(string maDvi)
        {
            using (OracleConnection cn = new ConnectionOracle().getConnection())
            {
                cn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand
                    {
                        Connection = cn,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "PKG_QLTN_QUANTRI.get_NHOMQUYEN_BY_DVIQLY"
                    };

                    cmd.Parameters.Add("p_ma_dviqly", OracleDbType.Varchar2).Value = maDvi;
                    cmd.Parameters.Add("p_getDB", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter dap = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dap.Fill(ds);

                    List<HT_NHOMQUYEN_Model> results = new List<HT_NHOMQUYEN_Model>();

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            HT_NHOMQUYEN_Model result = new HT_NHOMQUYEN_Model
                            {
                                nhom_id = dr["NHOM_ID"].ToString(),
                                ten_nhom = dr["TEN_NHOM"].ToString()
                            };
                            results.Add(result);
                        }
                    }

                    return results;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
