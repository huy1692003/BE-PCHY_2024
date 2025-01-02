using APIPCHY.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
namespace API_PCHY.Models.QLTN.BAO_CAO
{
    public class SL_QLTN_THEOKHACHHANG_Manager
    {
        DataHelper dataHelper = new DataHelper();
        public List<SOLUONG_YCTN_Model> getall(DateTime str_date, DateTime end_date)
        {
            try
            {
                DataTable ds = dataHelper.ExcuteReader("PKG_QLTN_VINH.SL_THINGHIEM_THEO_KHACHHANG", 
                    "p_startDate", "p_endDate",
                    str_date, end_date);
                List<SOLUONG_YCTN_Model> list = new List<SOLUONG_YCTN_Model> ();
                if(ds != null)
                {
                    for(int i = 0; i < ds.Rows.Count; i++)
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
                        model.tongso = ds.Rows[i]["TONGSO"] != DBNull.Value ? ds.Rows[i]["TONGSO"].ToString(): null;
                        list.Add(model);
                    }
                }
                return list;
            }
            catch(Exception ex) {
                throw ex;
            }
        }
    }
}
