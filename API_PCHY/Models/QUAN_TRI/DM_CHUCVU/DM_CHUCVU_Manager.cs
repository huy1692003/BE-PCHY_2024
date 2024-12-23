using APIPCHY.Helpers;
using System.Collections.Generic;
using System;
using System.Data;
using API_PCHY.Models.QUAN_TRI.DM_CHUC_VU;

namespace API_PCHY.Models.QUAN_TRI.DM_CHUCVU
{
    public class DM_CHUCVU_Manager
    {
        DataHelper helper = new DataHelper();
        public List<DM_CHUCVU_Model> getAll_DM_CHUCVU()
        {
            try
            {
                DataTable tb = helper.ExcuteReader("PKG_QLTN_QUANTRI.get_DM_CHUCVU");
                List<DM_CHUCVU_Model> result = new List<DM_CHUCVU_Model>();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        DM_CHUCVU_Model model = new DM_CHUCVU_Model();
                        model.id = tb.Rows[i]["ID"] != DBNull.Value ? tb.Rows[i]["ID"].ToString() : null;
                        model.ma = tb.Rows[i]["MA"] != DBNull.Value ? tb.Rows[i]["MA"].ToString() : null;
                        model.ten = tb.Rows[i]["TEN"] != DBNull.Value ? tb.Rows[i]["TEN"].ToString() : null;                       
                        model.dm_donvi_id = tb.Rows[i]["DM_DONVI_ID"] != DBNull.Value ? tb.Rows[i]["DM_DONVI_ID"].ToString() : null;                       
                        result.Add(model);

                    }
                }
                else result = null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
