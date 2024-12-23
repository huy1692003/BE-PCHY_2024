using APIPCHY.Helpers;
using System.Collections.Generic;
using System.Data;
using System;
using APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN;

namespace APIPCHY_PhanQuyen.Models.QLTN.DM_PHONGBAN
{
    public class DM_PHONGBAN_Manager
    {
        DataHelper helper = new DataHelper();
        public string insert_DM_PHONGBAN(DM_PHONGBAN_Model dM_PHONGBAN)
        {
            try
            {
                Guid id = Guid.NewGuid();
                string str_id = id.ToString();
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.insert_DM_PHONGBAN", "p_Error",
                                                    "p_ID", "p_MA", "p_TEN", "p_TRANG_THAI", "p_SAP_XEP",
                                                    "p_NGUOI_TAO", "p_DM_DONVI_ID", "p_DM_PHONGBAN_ID", "p_DB_MAPHONGBAN", "p_DB_NGAY", "p_MA_DVIQLY",
                                                    str_id, dM_PHONGBAN.ma, dM_PHONGBAN.ten, dM_PHONGBAN.trang_thai, dM_PHONGBAN.sap_xep,
                                                    dM_PHONGBAN.nguoi_tao, dM_PHONGBAN.dm_donvi_id, dM_PHONGBAN.dm_phongban_id, dM_PHONGBAN.db_maphongban, dM_PHONGBAN.db_ngay, dM_PHONGBAN.ma_dviqly
                                                    );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string update_DM_PHONGBAN(DM_PHONGBAN_Model dM_PHONGBAN)
        {
            try
            {
                string str_id = dM_PHONGBAN.id.ToString();
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.update_DM_PHONGBAN", "p_Error",
                                                    "p_ID", "p_MA", "p_TEN", "p_TRANG_THAI", "p_SAP_XEP", "p_NGUOI_TAO", "p_DM_DONVI_ID",
                                                    "p_DM_PHONGBAN_ID", "p_DB_MAPHONGBAN", "p_DB_NGAY", "p_MA_DVIQLY", "p_NGUOI_CAP_NHAT", "p_NGAY_TAO",
                                                    str_id, dM_PHONGBAN.ma, dM_PHONGBAN.ten, dM_PHONGBAN.trang_thai, dM_PHONGBAN.sap_xep, dM_PHONGBAN.nguoi_tao, dM_PHONGBAN.dm_donvi_id,
                                                    dM_PHONGBAN.dm_phongban_id, dM_PHONGBAN.db_maphongban, dM_PHONGBAN.db_ngay, dM_PHONGBAN.ma_dviqly, dM_PHONGBAN.nguoi_cap_nhat, dM_PHONGBAN.ngay_tao
                                                    );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string delete_DM_PHONGBAN(string ID)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.delete_DM_PHONGBAN", "p_Error",
                                                    "p_ID", ID
                                                    );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DM_PHONGBAN_Model> search_DM_PHONGBANByID(int? pageIndex, int? pageSize, string ten, string ma, int? trang_thai, string ma_dviqly, out int totalItems)
        {
            totalItems = 0;
            try
            {

                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.search_DM_PHONGBAN", "p_page_index", "p_page_size", "p_TEN", "p_MA", "p_TRANG_THAI", "p_MA_DVIQLY", pageIndex, pageSize, ten, ma, trang_thai, ma_dviqly);
                var count = ds.Rows.Count;
                if (count > 0)
                {
                    totalItems = int.Parse(ds.Rows[0]["RecordCount"].ToString());
                    List<DM_PHONGBAN_Model> list = new List<DM_PHONGBAN_Model>();
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {

                        DM_PHONGBAN_Model dM_PHONGBAN = new DM_PHONGBAN_Model();
                        dM_PHONGBAN.id = ds.Rows[i]["ID"].ToString();
                        dM_PHONGBAN.ma = ds.Rows[i]["MA"] != DBNull.Value ? ds.Rows[i]["MA"].ToString() : null;
                        dM_PHONGBAN.ten = ds.Rows[i]["TEN"] != DBNull.Value ? ds.Rows[i]["TEN"].ToString() : null;
                        dM_PHONGBAN.trang_thai = ds.Rows[i]["TRANG_THAI"] != DBNull.Value ? int.Parse(ds.Rows[i]["TRANG_THAI"].ToString()) : null;
                        dM_PHONGBAN.sap_xep = ds.Rows[i]["SAP_XEP"] != DBNull.Value ? float.Parse(ds.Rows[i]["SAP_XEP"].ToString()) : null;
                        dM_PHONGBAN.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : null;
                        dM_PHONGBAN.nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null;
                        dM_PHONGBAN.ngay_cap_nhat = ds.Rows[i]["NGAY_CAP_NHAT"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_CAP_NHAT"].ToString()) : null;
                        dM_PHONGBAN.nguoi_cap_nhat = ds.Rows[i]["NGUOI_CAP_NHAT"] != DBNull.Value ? ds.Rows[i]["NGUOI_CAP_NHAT"].ToString() : null;
                        dM_PHONGBAN.dm_donvi_id = ds.Rows[i]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[i]["DM_DONVI_ID"].ToString() : null;
                        dM_PHONGBAN.dm_phongban_id = ds.Rows[i]["DM_PHONGBAN_ID"] != DBNull.Value ? ds.Rows[i]["DM_PHONGBAN_ID"].ToString() : null;
                        dM_PHONGBAN.db_maphongban = ds.Rows[i]["DB_MAPHONGBAN"] != DBNull.Value ? ds.Rows[i]["DB_MAPHONGBAN"].ToString() : null;
                        dM_PHONGBAN.db_ngay = ds.Rows[i]["DB_NGAY"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["DB_NGAY"].ToString()) : null;
                        dM_PHONGBAN.ma_dviqly = ds.Rows[i]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[i]["MA_DVIQLY"].ToString() : null;
                        dM_PHONGBAN.ten_dviqly = ds.Rows[i]["TEN_DVIQLY"] != DBNull.Value ? ds.Rows[i]["TEN_DVIQLY"].ToString() : null;
                        list.Add(dM_PHONGBAN);
                    }



                    return list;
                }
                totalItems = 0;
                return new List<DM_PHONGBAN_Model>();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DM_PHONGBAN_Model get_DM_PHONGBANByID(string ID)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_DM_PHONGBANBYID", "p_ID", ID);

                if (ds != null)
                {
                    DM_PHONGBAN_Model dM_PHONGBAN = new DM_PHONGBAN_Model();

                    dM_PHONGBAN.id = ds.Rows[0]["ID"].ToString();
                    dM_PHONGBAN.ma = ds.Rows[0]["MA"] != DBNull.Value ? ds.Rows[0]["MA"].ToString() : null;
                    dM_PHONGBAN.ten = ds.Rows[0]["TEN"] != DBNull.Value ? ds.Rows[0]["TEN"].ToString() : null;
                    dM_PHONGBAN.trang_thai = ds.Rows[0]["TRANG_THAI"] != DBNull.Value ? int.Parse(ds.Rows[0]["TRANG_THAI"].ToString()) : null;
                    dM_PHONGBAN.sap_xep = ds.Rows[0]["SAP_XEP"] != DBNull.Value ? int.Parse(ds.Rows[0]["SAP_XEP"].ToString()) : null;
                    dM_PHONGBAN.ngay_tao = ds.Rows[0]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_TAO"].ToString()) : null;
                    dM_PHONGBAN.nguoi_tao = ds.Rows[0]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[0]["NGUOI_TAO"].ToString() : null;
                    dM_PHONGBAN.ngay_cap_nhat = ds.Rows[0]["NGAY_CAP_NHAT"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_CAP_NHAT"].ToString()) : null;
                    dM_PHONGBAN.nguoi_cap_nhat = ds.Rows[0]["NGUOI_CAP_NHAT"] != DBNull.Value ? ds.Rows[0]["NGUOI_CAP_NHAT"].ToString() : null;
                    dM_PHONGBAN.dm_donvi_id = ds.Rows[0]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[0]["DM_DONVI_ID"].ToString() : null;
                    dM_PHONGBAN.dm_phongban_id = ds.Rows[0]["DM_PHONGBAN_ID"] != DBNull.Value ? ds.Rows[0]["DM_PHONGBAN_ID"].ToString() : null;
                    dM_PHONGBAN.db_maphongban = ds.Rows[0]["DB_MAPHONGBAN"] != DBNull.Value ? ds.Rows[0]["DB_MAPHONGBAN"].ToString() : null;
                    dM_PHONGBAN.db_ngay = ds.Rows[0]["DB_NGAY"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["DB_NGAY"].ToString()) : null;
                    dM_PHONGBAN.ma_dviqly = ds.Rows[0]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[0]["MA_DVIQLY"].ToString() : null;

                    return dM_PHONGBAN;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}


