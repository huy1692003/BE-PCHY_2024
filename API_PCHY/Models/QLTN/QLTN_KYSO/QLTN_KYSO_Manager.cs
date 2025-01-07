using API_PCHY.Models.QLTN.DM_LOAI_YCTN;
using API_PCHY.Models.QLTN.QLTN_YCTN;
using System.Collections.Generic;
using System;
using APIPCHY.Helpers;
using System.Data;
using API_PCHY.Models.QLTN.QLTN_CHI_TIET_THI_NGHIEM;
using API_PCHY.Models.QLTN.QLTN_NGUOI_KY;
using APIPCHY_PhanQuyen.Models.QLTN.DM_DONVI;
using Newtonsoft.Json;
using API_PCHY.Models.SMART_CA;
using Microsoft.AspNetCore.Hosting;

namespace API_PCHY.Models.QLTN.QLTN_KYSO
{
    public class QLTN_KYSO_Manager
    {

       

        DataHelper db = new DataHelper();
        QLTN_NGUOI_KY_Manager nk = new QLTN_NGUOI_KY_Manager();
        DM_DONVI_Manager dv = new DM_DONVI_Manager();
        QLTN_CHI_TIET_THI_NGHIEM_Manager cttn = new QLTN_CHI_TIET_THI_NGHIEM_Manager();
        private List<string> getListTenDonVi(string list)
        {
            // Chuyển chuỗi JSON thành mảng
            string[] result = JsonConvert.DeserializeObject<string[]>(list);

            // Tạo danh sách để lưu kết quả
            List<string> tenDonViList = new List<string>();

            foreach (string item in result)
            {
                // Lấy tên đơn vị từ phương thức dv.get_DM_DONVI_ByID
                string tenDonVi = dv.get_DM_DONVI_ByID(item).ten;

                // Thêm vào danh sách
                tenDonViList.Add(tenDonVi);
            }

            return tenDonViList;
        }

        public List<Document_Model>? SEARCH_VANBAN(Paginage paginate, SearchParamDocument search, out int totalRecord)
        {
            totalRecord = 0;
            try
            {
                // Xử lý ngày bắt đầu và kết thúc nếu không có giá trị
                DateTime startDate = search.NgayBatDau ?? DateTime.Now.AddDays(-30);
                DateTime endDate = search.NgayKetThuc ?? DateTime.Now;

                // Tạo tham số cho stored procedure

                // Thực hiện gọi stored procedure và lấy dữ liệu trả về
                DataTable ds = db.ExcuteReader("PKG_QLTN_HUY.SEARCH_VANBAN", "P_KEYWORD", "P_NGAYBATDAU", "P_NGAYKETTHUC", "P_STATUS_DOCUMENT", "P_TIEN_TRINH_KYSO", "P_USER_ID", "P_DON_VI_THUC_HIEN", "P_ID_LOAI_BIEN_BAN", "P_PAGE", "P_PAGESIZE",
                                                                             search.Keyword, startDate, endDate, search.Status_Document, search.TienTrinhKySo, search.UserId, search.DonViThucHien, search.IdLoaiBienBan, paginate.Page, paginate.PageSize);

                List<Document_Model> documents = new List<Document_Model>();
                //Kiểm tra dữ liệu trả về
                if (ds != null && ds.Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Rows)
                    {

                        Document_Model doc = new Document_Model();
                        doc.ten_yctn = row["TEN_YCTN"]?.ToString();
                        doc.ma_yctn = row["MA_YCTN"]?.ToString();
                        doc.loai_yctn = row["TEN_LOAI_YC"]?.ToString();
                        doc.ten_loai_bb = row["TEN_LOAI_BB"]?.ToString();
                        doc.don_vi_thuc_hien = getListTenDonVi(row["DON_VI_THUC_HIEN"]?.ToString());
                        doc.ma_loaitb = row["MA_LOAI_TB"]?.ToString();
                        doc.ten_thietbi = row["TEN_THIET_BI"]?.ToString();
                        doc.soluong = row["SO_LUONG"] != DBNull.Value ? int.Parse(row["SO_LUONG"].ToString()) : (int?)null;
                        doc.lanthu = row["LANTHU"] != DBNull.Value ? int.Parse(row["LANTHU"].ToString()) : (int?)null;
                        doc.ngaytao = row["NGAY_TAO"] != DBNull.Value ? DateTime.Parse(row["NGAY_TAO"].ToString()) : (DateTime?)null;
                        doc.ma_chitiet_tn = row["MA_CHI_TIET_TN"]?.ToString();
                        doc.file_upload = row["FILE_UPLOAD"]?.ToString();
                        doc.nguoi_tao = row["NGUOI_TAO"]?.ToString();
                        doc.rownum = row["TOTAL_COUNT"] != DBNull.Value ? int.Parse(row["TOTAL_COUNT"].ToString()) : 0;
                        doc.list_NguoiKy = nk.getNguoiKyByMa_CTTN(doc.ma_chitiet_tn);
                        doc.chi_tiet_tn = cttn.get_QLTN_CHITIET_TN_ByMA_CTTN(doc.ma_chitiet_tn);

                        // Cập nhật giá trị tổng số bản ghi
                        totalRecord = doc.rownum ?? 0;
                        documents.Add(doc);
                    }
                }

                return documents;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Log exception nếu cần thiết
                return new List<Document_Model>(); ;
            }
        }


        public bool update_TrangThai_Ky(ReqUpdateKySo req , IWebHostEnvironment web)
        {

            string result = "";
            try
            {
                if (req.TrangThai == -1)
                {
                   result = db.ExcuteNonQuery("PKG_QLTN_HUY.update_TrangThai_Ky", "p_Error",
                        "p_ID", "p_MaCTTN", "p_ID_NGUOI_KY", "p_NHOM_NGUOI_KY", "p_TRANG_THAI", "p_LY_DO_TUCHOI", "p_PATH_FILE ",
                        req.Id, req.MaCTTN, req.IdNguoiKy, req.NhomNguoiKy, req.TrangThai, req.TrangThai == -1 ? req.LyDoTuChoi : DBNull.Value, DBNull.Value
                    );
                }
                else
                {
                    SmartCA769 smartCA769 = new SmartCA769(web);
                    string pathOutput = "";
                    smartCA769._signSmartCA(req.requestSign, out pathOutput);

                    result = db.ExcuteNonQuery("PKG_QLTN_HUY.update_TrangThai_Ky", "p_Error",
                       "p_ID", "p_MaCTTN", "p_ID_NGUOI_KY", "p_NHOM_NGUOI_KY", "p_TRANG_THAI", "p_LY_DO_TUCHOI", "p_PATH_FILE",
                       req.Id, req.MaCTTN, req.IdNguoiKy, req.NhomNguoiKy, req.TrangThai, DBNull.Value , pathOutput
                   );

                }
                return string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật trạng thái ký: {ex.Message}");
            }
        }
    }


}
