using System.Collections.Generic;
using System;
using APIPCHY.Helpers;
using System.Data;
using static API_PCHY.Models.QLTN.BAO_CAO.BAO_CAO_Model;
using API_PCHY.Models.QLTN.QLTN_YCTN;

namespace API_PCHY.Models.QLTN.BAO_CAO
{
    public class BAO_CAO_Manager
    {
        DataHelper dataHelper = new DataHelper();
        public List<SOLUONG_YCTN_Model> get_SOLUONG_YCTN(DateTime str_date, DateTime end_date)
        {
            try
            {
                DataTable ds = dataHelper.ExcuteReader("PKG_QLTN_VINH.SL_THINGHIEM_THEO_DONVITHUCHIEN",
                    "p_startDate", "p_endDate",
                    str_date, end_date);
                List<SOLUONG_YCTN_Model> list = new List<SOLUONG_YCTN_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        SOLUONG_YCTN_Model model = new SOLUONG_YCTN_Model();
                        model.ten_don_vi = ds.Rows[i]["TEN"] != DBNull.Value ? ds.Rows[i]["TEN"].ToString() : null;
                        model.kehoachthinghiem = ds.Rows[i]["KEHOACHTHINGHIEM"] != DBNull.Value ? ds.Rows[i]["KEHOACHTHINGHIEM"].ToString() : null;
                        model.hopdong = ds.Rows[i]["HOPDONG"] != DBNull.Value ? ds.Rows[i]["HOPDONG"].ToString() : null;
                        model.xulysuco = ds.Rows[i]["XULYSUCO"] != DBNull.Value ? ds.Rows[i]["XULYSUCO"].ToString() : null;
                        model.taomoi = ds.Rows[i]["TAOMOI"] != DBNull.Value ? ds.Rows[i]["TAOMOI"].ToString() : null;
                        model.giaonhiemvu = ds.Rows[i]["GIAONHIEMVU"] != DBNull.Value ? ds.Rows[i]["GIAONHIEMVU"].ToString() : null;
                        model.nhapkhoiluong = ds.Rows[i]["NHAPKHOILUONG"] != DBNull.Value ? ds.Rows[i]["NHAPKHOILUONG"].ToString() : null;
                        model.khaosat = ds.Rows[i]["KHAOSAT"] != DBNull.Value ? ds.Rows[i]["KHAOSAT"].ToString() : null;
                        model.thinghiem = ds.Rows[i]["THINGHIEM"] != DBNull.Value ? ds.Rows[i]["THINGHIEM"].ToString() : null;
                        model.bangiao = ds.Rows[i]["BANGIAO"] != DBNull.Value ? ds.Rows[i]["BANGIAO"].ToString() : null;
                        model.tongso = ds.Rows[i]["TONGSO"] != DBNull.Value ? ds.Rows[i]["TONGSO"].ToString() : null;
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
        public List<SOLUONG_YCTN_Model> get_SL_QLTN_THEOKHACHHANG(DateTime str_date, DateTime end_date)
        {
            try
            {
                DataTable ds = dataHelper.ExcuteReader("PKG_QLTN_VINH.SL_THINGHIEM_THEO_KHACHHANG",
                    "p_startDate", "p_endDate",
                    str_date, end_date);
                List<SOLUONG_YCTN_Model> list = new List<SOLUONG_YCTN_Model>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        SOLUONG_YCTN_Model model = new SOLUONG_YCTN_Model();
                        model.ten_kh = ds.Rows[i]["TEN_KH"] != DBNull.Value ? ds.Rows[i]["TEN_KH"].ToString() : null;
                        model.kehoachthinghiem = ds.Rows[i]["KEHOACHTHINGHIEM"] != DBNull.Value ? ds.Rows[i]["KEHOACHTHINGHIEM"].ToString() : null;
                        model.hopdong = ds.Rows[i]["HOPDONG"] != DBNull.Value ? ds.Rows[i]["HOPDONG"].ToString() : null;
                        model.xulysuco = ds.Rows[i]["XULYSUCO"] != DBNull.Value ? ds.Rows[i]["XULYSUCO"].ToString() : null;
                        model.taomoi = ds.Rows[i]["TAOMOI"] != DBNull.Value ? ds.Rows[i]["TAOMOI"].ToString() : null;
                        model.giaonhiemvu = ds.Rows[i]["GIAONHIEMVU"] != DBNull.Value ? ds.Rows[i]["GIAONHIEMVU"].ToString() : null;
                        model.nhapkhoiluong = ds.Rows[i]["NHAPKHOILUONG"] != DBNull.Value ? ds.Rows[i]["NHAPKHOILUONG"].ToString() : null;
                        model.khaosat = ds.Rows[i]["KHAOSAT"] != DBNull.Value ? ds.Rows[i]["KHAOSAT"].ToString() : null;
                        model.thinghiem = ds.Rows[i]["THINGHIEM"] != DBNull.Value ? ds.Rows[i]["THINGHIEM"].ToString() : null;
                        model.bangiao = ds.Rows[i]["BANGIAO"] != DBNull.Value ? ds.Rows[i]["BANGIAO"].ToString() : null;
                        model.tongso = ds.Rows[i]["TONGSO"] != DBNull.Value ? ds.Rows[i]["TONGSO"].ToString() : null;
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

        public List<CHU_KY_THEO_DONVI> thongke_chukyso(DateTime str_date, DateTime end_date)
        {
            try
            {
                DataTable ds = dataHelper.ExcuteReader("PKG_QLTN_HUY.thongke_chukyso",
                    "P_NGAYBATDAU", "P_NGAYKETTHUC",
                    str_date, end_date);
                List<CHU_KY_THEO_DONVI> list = new List<CHU_KY_THEO_DONVI>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        CHU_KY_THEO_DONVI model = new CHU_KY_THEO_DONVI();
                        model.id_dv = ds.Rows[i]["id_dv"] != DBNull.Value ? ds.Rows[i]["id_dv"].ToString() : null;
                        model.ten_dv = ds.Rows[i]["ten_dv"] != DBNull.Value ? ds.Rows[i]["ten_dv"].ToString() : null;
                        model.total_trans = ds.Rows[i]["total_trans"] != DBNull.Value ? int.Parse(ds.Rows[i]["total_trans"].ToString()) : null;
                        model.total_trans_success = ds.Rows[i]["total_trans_success"] != DBNull.Value ? int.Parse(ds.Rows[i]["total_trans_success"].ToString()) : null;
                        model.total_trans_fail = ds.Rows[i]["total_trans_fail"] != DBNull.Value ? int.Parse(ds.Rows[i]["total_trans_fail"].ToString()) : null;                       
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

        public dashboard_model? getDashboard(string userID)
        {
            try
            {
                DataTable ds = dataHelper.ExcuteReader("PKG_QLTN_HUY.getDashboard",
                    "p_UserID",userID);
                if (ds != null && ds.Rows.Count > 0)
                {
                    dashboard_model data = new dashboard_model();

                    data.total_kyso_waiting = ds.Rows[0]["total_kyso_waiting"] != DBNull.Value ? int.Parse(ds.Rows[0]["total_kyso_waiting"].ToString()) : 0;
                    data.total_YCTN = ds.Rows[0]["total_YCTN"] != DBNull.Value ? int.Parse(ds.Rows[0]["total_YCTN"].ToString()) : 0;
                    data.total_kyso_fail = ds.Rows[0]["total_kyso_fail"] != DBNull.Value ? int.Parse(ds.Rows[0]["total_kyso_fail"].ToString()) : 0;
                    data.total_kyso_success = ds.Rows[0]["total_kyso_success"] != DBNull.Value ? int.Parse(ds.Rows[0]["total_kyso_success"].ToString()) : 0;
                    return data;
                  
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
