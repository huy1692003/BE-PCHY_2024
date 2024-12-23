using System;
using APIPCHY.Helpers;
using System.Data;
using System.Drawing;
using iTextSharp.text;
using System.Collections.Generic;
using APIPCHY_PhanQuyen.Models.QLKC.DM_DONVI;

namespace APIPCHY_PhanQuyen.Models.QLTN.DM_DONVI
{
    public class DM_DONVI_Manager
    {
        DataHelper helper = new DataHelper();

        public string insert_DM_DONVI(DM_DONVI_Model model)
        {
            try
            {
                Guid id = Guid.NewGuid();
                string str_id = id.ToString();

                Guid dm_donvi_id = Guid.NewGuid();
                string str_dm_donvi_id = dm_donvi_id.ToString();



                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.insert_DM_DONVI", "p_Error",
                                                    "p_ID", "p_DM_DONVI_ID", "p_LOAI_DON_VI", "p_MA", "p_TEN",
                                                    "p_TRANG_THAI", "p_SAP_XEP", "p_GHI_CHU", "p_NGUOI_TAO", "p_CAP_SO",
                                                    "p_CAP_MA", "p_DM_TINHTHANH_ID", "p_DM_QUANHUYEN_ID", "p_DM_DONVI_CHUQUAN_ID",
                                                    "p_MA_FMIS", "p_DB_MADONVI", "p_DB_MADONVI_FMIS", "p_DB_NGAY",
                                                    "p_TYPE_DONVI", "p_GROUP_DONVI", "p_DO_DUTHAO", "p_SU_DUNG", "p_MA_DVIQLY",
                                                    str_id, str_dm_donvi_id, model.loai_don_vi, model.ma, model.ten, model.trang_thai,
                                                    model.sap_xep, model.ghi_chu, model.nguoi_tao, model.cap_so, model.cap_ma, model.dm_tinhthanh_id,
                                                    model.dm_quanhuyen_id, model.dm_donvi_chuquan_id, model.ma_fmis, model.db_madonvi, model.db_madonvi_fmis,
                                                    model.db_ngay, model.type_donvi, model.group_donvi, model.do_duthao, model.su_dung, model.ma_dviqly);

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string update_DM_DONVI(DM_DONVI_Model model)
        {
            try
            {
                //Guid id = Guid.NewGuid();               

                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.update_DM_DONVI", "p_Error",
                                                    "p_ID", "p_DM_DONVI_ID", "p_LOAI_DON_VI", "p_MA", "p_TEN",
                                                    "p_TRANG_THAI", "p_SAP_XEP", "p_GHI_CHU", "p_NGAY_CAP_NHAT", "p_NGUOI_CAP_NHAT",
                                                    "p_CAP_SO", "p_CAP_MA", "p_DM_TINHTHANH_ID", "p_DM_QUANHUYEN_ID",
                                                    "p_DM_DONVI_CHUQUAN_ID", "p_MA_FMIS", "p_DB_MADONVI", "p_DB_MADONVI_FMIS",
                                                    "p_DB_NGAY", "p_TYPE_DONVI", "p_GROUP_DONVI", "p_DO_DUTHAO", "p_SU_DUNG", "p_MA_DVIQLY",
                                                    model.id, model.dm_donvi_id, model.loai_don_vi, model.ma, model.ten, model.trang_thai,
                                                    model.sap_xep, model.ghi_chu, model.ngay_cap_nhat ?? DateTime.Now, model.nguoi_cap_nhat ?? "Trống", model.cap_so, model.cap_ma, model.dm_tinhthanh_id,
                                                    model.dm_quanhuyen_id, model.dm_donvi_chuquan_id, model.ma_fmis, model.db_madonvi, model.db_madonvi_fmis,
                                                    model.db_ngay, model.type_donvi, model.group_donvi, model.do_duthao, model.su_dung, model.ma_dviqly);

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string delete_DM_DONVI(string id)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.delete_DM_DONVI", "p_Error",
                                                    "p_ID", id);

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DM_DONVI_Model> search_DM_DONVI(int? pageIndex, int? pageSize, string ten, string ma, int? trang_thai, string ma_dviqly, out int totalItems)
        {
            totalItems = 0;
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.search_DM_DONVI", "p_page_index", "p_page_size",
                                                    "p_TEN", "p_MA", "p_TRANG_THAI", "p_DVIQLY", pageIndex, pageSize, ten, ma, trang_thai==-1?DBNull.Value:trang_thai, ma==null?ma_dviqly:DBNull.Value);
                var count = ds.Rows.Count;
                totalItems = int.Parse(ds.Rows[0]["RECORDCOUNT"].ToString());
                List<DM_DONVI_Model> list = new List<DM_DONVI_Model>();
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    DM_DONVI_Model model = new DM_DONVI_Model();
                    model.id = ds.Rows[i]["ID"].ToString();
                    model.dm_donvi_id = ds.Rows[i]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[i]["DM_DONVI_ID"].ToString() : null;
                    model.loai_don_vi = ds.Rows[i]["LOAI_DON_VI"] != DBNull.Value ? int.Parse(ds.Rows[i]["LOAI_DON_VI"].ToString()) : null;
                    model.ma = ds.Rows[i]["MA"] != DBNull.Value ? ds.Rows[i]["MA"].ToString() : null;
                    model.ten = ds.Rows[i]["TEN"] != DBNull.Value ? ds.Rows[i]["TEN"].ToString() : null;
                    model.trang_thai = ds.Rows[i]["TRANG_THAI"] != DBNull.Value ? int.Parse(ds.Rows[i]["TRANG_THAI"].ToString()) : null;
                    model.sap_xep = ds.Rows[i]["SAP_XEP"] != DBNull.Value ? int.Parse(ds.Rows[i]["SAP_XEP"].ToString()) : null;
                    model.ghi_chu = ds.Rows[i]["GHI_CHU"] != DBNull.Value ? ds.Rows[i]["GHI_CHU"].ToString() : null;
                    model.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : null;
                    model.nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null;
                    model.ngay_cap_nhat = ds.Rows[i]["NGAY_CAP_NHAT"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_CAP_NHAT"].ToString()) : null;
                    model.nguoi_cap_nhat = ds.Rows[i]["NGUOI_CAP_NHAT"] != DBNull.Value ? ds.Rows[i]["NGUOI_CAP_NHAT"].ToString() : null;
                    model.cap_so = ds.Rows[i]["CAP_SO"] != DBNull.Value ? ds.Rows[i]["CAP_SO"].ToString() : null;
                    model.cap_ma = ds.Rows[i]["CAP_MA"] != DBNull.Value ? ds.Rows[i]["CAP_MA"].ToString() : null;
                    model.dm_tinhthanh_id = ds.Rows[i]["DM_TINHTHANH_ID"] != DBNull.Value ? ds.Rows[i]["DM_TINHTHANH_ID"].ToString() : null;
                    model.dm_quanhuyen_id = ds.Rows[i]["DM_QUANHUYEN_ID"] != DBNull.Value ? ds.Rows[i]["DM_QUANHUYEN_ID"].ToString() : null;
                    model.dm_donvi_chuquan_id = ds.Rows[i]["DM_DONVI_CHUQUAN_ID"] != DBNull.Value ? ds.Rows[i]["DM_DONVI_CHUQUAN_ID"].ToString() : null;
                    model.ma_fmis = ds.Rows[i]["MA_FMIS"] != DBNull.Value ? ds.Rows[i]["MA_FMIS"].ToString() : null;
                    model.db_madonvi_fmis = ds.Rows[i]["DB_MADONVI_FMIS"] != DBNull.Value ? ds.Rows[i]["DB_MADONVI_FMIS"].ToString() : null;
                    model.db_ngay = ds.Rows[i]["DB_NGAY"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["DB_NGAY"].ToString()) : null;
                    model.type_donvi = ds.Rows[i]["TYPE_DONVI"] != DBNull.Value ? int.Parse(ds.Rows[i]["TYPE_DONVI"].ToString()) : null;
                    model.group_donvi = ds.Rows[i]["GROUP_DONVI"] != DBNull.Value ? int.Parse(ds.Rows[i]["GROUP_DONVI"].ToString()) : null;
                    model.do_duthao = ds.Rows[i]["DO_DUTHAO"] != DBNull.Value ? int.Parse(ds.Rows[i]["DO_DUTHAO"].ToString()) : null;
                    model.su_dung = ds.Rows[i]["SU_DUNG"] != DBNull.Value ? int.Parse(ds.Rows[i]["SU_DUNG"].ToString()) : null;
                    model.ma_dviqly = ds.Rows[i]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[i]["MA_DVIQLY"].ToString() : null;

                    list.Add(model);
                }

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<DM_DONVI_Model> get_All_DM_DONVI(string ma_dviqly)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_All_DM_DONVI", "p_MA_DVIQLY", ma_dviqly);
                List<DM_DONVI_Model> list = new List<DM_DONVI_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        DM_DONVI_Model model = new DM_DONVI_Model();
                        model.id = ds.Rows[i]["ID"].ToString();
                        model.dm_donvi_id = ds.Rows[i]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[i]["DM_DONVI_ID"].ToString() : null;
                        model.loai_don_vi = ds.Rows[i]["LOAI_DON_VI"] != DBNull.Value ? int.Parse(ds.Rows[i]["LOAI_DON_VI"].ToString()) : null;
                        model.ma = ds.Rows[i]["MA"] != DBNull.Value ? ds.Rows[i]["MA"].ToString() : null;
                        model.ten = ds.Rows[i]["TEN"] != DBNull.Value ? ds.Rows[i]["TEN"].ToString() : null;
                        model.trang_thai = ds.Rows[i]["TRANG_THAI"] != DBNull.Value ? int.Parse(ds.Rows[i]["TRANG_THAI"].ToString()) : null;
                        model.sap_xep = ds.Rows[i]["SAP_XEP"] != DBNull.Value ? int.Parse(ds.Rows[i]["SAP_XEP"].ToString()) : null;
                        model.ghi_chu = ds.Rows[i]["GHI_CHU"] != DBNull.Value ? ds.Rows[i]["GHI_CHU"].ToString() : null;
                        model.ngay_tao = ds.Rows[i]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_TAO"].ToString()) : null;
                        model.nguoi_tao = ds.Rows[i]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[i]["NGUOI_TAO"].ToString() : null;
                        model.ngay_cap_nhat = ds.Rows[i]["NGAY_CAP_NHAT"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["NGAY_CAP_NHAT"].ToString()) : null;
                        model.nguoi_cap_nhat = ds.Rows[i]["NGUOI_CAP_NHAT"] != DBNull.Value ? ds.Rows[i]["NGUOI_CAP_NHAT"].ToString() : null;
                        model.cap_so = ds.Rows[i]["CAP_SO"] != DBNull.Value ? ds.Rows[i]["CAP_SO"].ToString() : null;
                        model.cap_ma = ds.Rows[i]["CAP_MA"] != DBNull.Value ? ds.Rows[i]["CAP_MA"].ToString() : null;
                        model.dm_tinhthanh_id = ds.Rows[i]["DM_TINHTHANH_ID"] != DBNull.Value ? ds.Rows[i]["DM_TINHTHANH_ID"].ToString() : null;
                        model.dm_quanhuyen_id = ds.Rows[i]["DM_QUANHUYEN_ID"] != DBNull.Value ? ds.Rows[i]["DM_QUANHUYEN_ID"].ToString() : null;
                        model.dm_donvi_chuquan_id = ds.Rows[i]["DM_DONVI_CHUQUAN_ID"] != DBNull.Value ? ds.Rows[i]["DM_DONVI_CHUQUAN_ID"].ToString() : null;
                        model.ma_fmis = ds.Rows[i]["MA_FMIS"] != DBNull.Value ? ds.Rows[i]["MA_FMIS"].ToString() : null;
                        model.db_madonvi_fmis = ds.Rows[i]["DB_MADONVI_FMIS"] != DBNull.Value ? ds.Rows[i]["DB_MADONVI_FMIS"].ToString() : null;
                        model.db_ngay = ds.Rows[i]["DB_NGAY"] != DBNull.Value ? DateTime.Parse(ds.Rows[i]["DB_NGAY"].ToString()) : null;
                        model.type_donvi = ds.Rows[i]["TYPE_DONVI"] != DBNull.Value ? int.Parse(ds.Rows[i]["TYPE_DONVI"].ToString()) : null;
                        model.group_donvi = ds.Rows[i]["GROUP_DONVI"] != DBNull.Value ? int.Parse(ds.Rows[i]["GROUP_DONVI"].ToString()) : null;
                        model.do_duthao = ds.Rows[i]["DO_DUTHAO"] != DBNull.Value ? int.Parse(ds.Rows[i]["DO_DUTHAO"].ToString()) : null;
                        model.su_dung = ds.Rows[i]["SU_DUNG"] != DBNull.Value ? int.Parse(ds.Rows[i]["SU_DUNG"].ToString()) : null;
                        model.ma_dviqly = ds.Rows[i]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[i]["MA_DVIQLY"].ToString() : null;

                        list.Add(model);
                    }
                }
                else list = null;

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<DM_DONVI_Model> get_All_DM_DONVI_ByMADVIQLY(string MaDVIQLY)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_DM_DONVI_By_MADVIQLY", "@p_MA_DVIQLY", MaDVIQLY);
                List<DM_DONVI_Model> list = new List<DM_DONVI_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        DM_DONVI_Model model = new DM_DONVI_Model();
                        model.id = ds.Rows[i]["ID"].ToString();
                        model.dm_donvi_id = ds.Rows[i]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[i]["DM_DONVI_ID"].ToString() : null;
                        model.loai_don_vi = ds.Rows[i]["LOAI_DON_VI"] != DBNull.Value ? int.Parse(ds.Rows[i]["LOAI_DON_VI"].ToString()) : null;
                        model.ma = ds.Rows[i]["MA"] != DBNull.Value ? ds.Rows[i]["MA"].ToString() : null;
                        model.ten = ds.Rows[i]["TEN"] != DBNull.Value ? ds.Rows[i]["TEN"].ToString() : null;
                        model.ma_dviqly = ds.Rows[i]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[i]["MA_DVIQLY"].ToString() : null;

                        list.Add(model);
                    }
                }
                else list = null;

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DM_DONVI_Model get_DM_DONVI_ByID(string ID)
        {
            try
            {
                DataTable ds = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_DM_DONVI_By_ID", "p_ID", ID);
                if (ds != null && ds.Rows.Count > 0)
                {
                    DM_DONVI_Model model = new DM_DONVI_Model();

                    model.id = ds.Rows[0]["ID"].ToString();
                    model.dm_donvi_id = ds.Rows[0]["DM_DONVI_ID"] != DBNull.Value ? ds.Rows[0]["DM_DONVI_ID"].ToString() : null;
                    model.loai_don_vi = ds.Rows[0]["LOAI_DON_VI"] != DBNull.Value ? int.Parse(ds.Rows[0]["LOAI_DON_VI"].ToString()) : null;
                    model.ma = ds.Rows[0]["MA"] != DBNull.Value ? ds.Rows[0]["MA"].ToString() : null;
                    model.ten = ds.Rows[0]["TEN"] != DBNull.Value ? ds.Rows[0]["TEN"].ToString() : null;
                    model.trang_thai = ds.Rows[0]["TRANG_THAI"] != DBNull.Value ? int.Parse(ds.Rows[0]["TRANG_THAI"].ToString()) : null;
                    model.sap_xep = ds.Rows[0]["SAP_XEP"] != DBNull.Value ? int.Parse(ds.Rows[0]["SAP_XEP"].ToString()) : null;
                    model.ghi_chu = ds.Rows[0]["GHI_CHU"] != DBNull.Value ? ds.Rows[0]["GHI_CHU"].ToString() : null;
                    model.ngay_tao = ds.Rows[0]["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_TAO"].ToString()) : null;
                    model.nguoi_tao = ds.Rows[0]["NGUOI_TAO"] != DBNull.Value ? ds.Rows[0]["NGUOI_TAO"].ToString() : null;
                    model.ngay_cap_nhat = ds.Rows[0]["NGAY_CAP_NHAT"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["NGAY_CAP_NHAT"].ToString()) : null;
                    model.nguoi_cap_nhat = ds.Rows[0]["NGUOI_CAP_NHAT"] != DBNull.Value ? ds.Rows[0]["NGUOI_CAP_NHAT"].ToString() : null;
                    model.cap_so = ds.Rows[0]["CAP_SO"] != DBNull.Value ? ds.Rows[0]["CAP_SO"].ToString() : null;
                    model.cap_ma = ds.Rows[0]["CAP_MA"] != DBNull.Value ? ds.Rows[0]["CAP_MA"].ToString() : null;
                    model.dm_tinhthanh_id = ds.Rows[0]["DM_TINHTHANH_ID"] != DBNull.Value ? ds.Rows[0]["DM_TINHTHANH_ID"].ToString() : null;
                    model.dm_quanhuyen_id = ds.Rows[0]["DM_QUANHUYEN_ID"] != DBNull.Value ? ds.Rows[0]["DM_QUANHUYEN_ID"].ToString() : null;
                    model.dm_donvi_chuquan_id = ds.Rows[0]["DM_DONVI_CHUQUAN_ID"] != DBNull.Value ? ds.Rows[0]["DM_DONVI_CHUQUAN_ID"].ToString() : null;
                    model.ma_fmis = ds.Rows[0]["MA_FMIS"] != DBNull.Value ? ds.Rows[0]["MA_FMIS"].ToString() : null;
                    model.db_madonvi = ds.Rows[0]["DB_MADONVI"] != DBNull.Value ? ds.Rows[0]["DB_MADONVI"].ToString() : null;
                    model.db_madonvi_fmis = ds.Rows[0]["DB_MADONVI_FMIS"] != DBNull.Value ? ds.Rows[0]["DB_MADONVI_FMIS"].ToString() : null;
                    model.db_ngay = ds.Rows[0]["DB_NGAY"] != DBNull.Value ? DateTime.Parse(ds.Rows[0]["DB_NGAY"].ToString()) : null;
                    model.type_donvi = ds.Rows[0]["TYPE_DONVI"] != DBNull.Value ? int.Parse(ds.Rows[0]["TYPE_DONVI"].ToString()) : null;
                    model.group_donvi = ds.Rows[0]["GROUP_DONVI"] != DBNull.Value ? int.Parse(ds.Rows[0]["GROUP_DONVI"].ToString()) : null;
                    model.do_duthao = ds.Rows[0]["DO_DUTHAO"] != DBNull.Value ? int.Parse(ds.Rows[0]["DO_DUTHAO"].ToString()) : null;
                    model.su_dung = ds.Rows[0]["SU_DUNG"] != DBNull.Value ? int.Parse(ds.Rows[0]["SU_DUNG"].ToString()) : null;
                    model.ma_dviqly = ds.Rows[0]["MA_DVIQLY"] != DBNull.Value ? ds.Rows[0]["MA_DVIQLY"].ToString() : null;
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

    }
}
