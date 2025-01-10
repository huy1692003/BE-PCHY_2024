using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using SignService.Common.HashSignature.Common;
using SignService.Common.HashSignature.Interface;
using SmartCATHNetCore;
using Microsoft.AspNetCore.Hosting;
using static API_PCHY.Models.SMART_CA.Model_SMART_CA;
using APIPCHY.Helpers;
using QRCoder;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using PdfReader = iTextSharp.text.pdf.PdfReader;
using System.Threading;



namespace API_PCHY.Models.SMART_CA
{
    public class SmartCA769
    {

        private static string client_id = "4b0c-638712580414678530.apps.smartcaapi.com";
        private static string client_secret = "ODFlMTAxYjE-NjYxOS00YjBj";
        private static string URIInsertSignatureToFile = "http://localhost:9999/api/add-sign/json";
        private readonly IWebHostEnvironment _webHostEnvironment;
        DataHelper helper = new DataHelper();


        public SmartCA769(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //Hàm ký số 
        public async Task<string> _signSmartCA(RequestSign req, string baseURL)
        {

            // Lấy chứng chỉ người dùng
            var userCert = _getAccountCert("https://gwsca.vnpt.vn/sca/sp769/v1/credentials/get_certificate", req.userSign);

            if (userCert == null)
            {
                return "";
            }

            string certBase64 = userCert.cert_data;

            string pathFileInput = req.pathFileIn_Out.StartsWith("/")
                ? Path.Combine(_webHostEnvironment.WebRootPath, req.pathFileIn_Out.TrimStart('/'))
                : Path.Combine(_webHostEnvironment.WebRootPath, req.pathFileIn_Out); // File cần ký
            string pathImageSignUser = Path.Combine(_webHostEnvironment.WebRootPath, req.pathImageSign.TrimStart('/')); //Ảnh chữ ký người dùng


            byte[] unsignData = null;
            try
            {
                byte[] fileBytesImage = File.ReadAllBytes(pathImageSignUser);
                byte[] fileBytes = File.ReadAllBytes(pathFileInput);
                RequestInsertSignature req_ = new RequestInsertSignature
                {
                    dataBase64 = Convert.ToBase64String(fileBytes),
                    signImgBase64 = Convert.ToBase64String(fileBytesImage),
                    signName = req.fullNameUser,
                    signType = req.signType
                };
                ResponseInsertSignature res = await InsertSignatureToFile(URIInsertSignatureToFile, req_);
                if (!res.Success)
                {
                    return "";
                }
                unsignData = Convert.FromBase64String(res.Data);

            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu không đọc được file
                return "";
            }

            // Tạo signer từ HashSignerFactory
            IHashSigner signer = HashSignerFactory.GenerateSigner(unsignData, certBase64, null, HashSignerFactory.PDF);

            // Đặt thuật toán hash
            signer.SetHashAlgorithm(MessageDigestAlgorithm.SHA256);


            var hashValue = signer.GetSecondHashAsBase64();

            // Chuyển đổi hashValue thành chuỗi ký
            var data_to_be_sign = BitConverter.ToString(Convert.FromBase64String(hashValue)).Replace("-", "").ToLower();

            var transactionID = Guid.NewGuid().ToString();

            var reqSign = new ReqSign();
            // Gửi yêu cầu ký dữ liệu
            DataSign dataSign = _sign("https://gwsca.vnpt.vn/sca/sp769/v1/signatures/sign", data_to_be_sign, req.userSign, req.descSign, transactionID, out reqSign);

            var count = 0;
            var isConfirm = false;
            //var isConfirm = true;
            var datasigned = "";
            var mapping = "";
            DataTransaction transactionStatus;

            var startTime = DateTime.Now; // Ghi lại thời gian bắt đầu

            //Kiểm tra trạng thái ký dữ liệu
            //while (!isConfirm && (DateTime.Now - startTime).TotalSeconds < 300)  // Chờ tối đa 5 phút
            //{
            //    transactionStatus = _getStatus(string.Format("https://gwsca.vnpt.vn/sca/sp769/v1/signatures/sign/{0}/status", dataSign.transaction_id));

            //    if (transactionStatus.signatures != null)
            //    {
            //        datasigned = transactionStatus.signatures[0].signature_value;
            //        mapping = transactionStatus.signatures[0].doc_id;
            //        isConfirm = true;
            //    }
            //    else
            //    {
            //        count++;
            //        Thread.Sleep(10000); // Chờ 10 giây trước khi thử lại
            //    }
            //}

            string base64String = Convert.ToBase64String(unsignData);
            byte[] byteArray = Convert.FromBase64String(base64String);
            // Nếu không nhận được chữ ký, trả về thất bại
            //if (!isConfirm || string.IsNullOrEmpty(datasigned))
            //{
            //    insert_LOG_KYSO(reqSign, "0", req.idUserApp);
            //    return "";
            //}

            //// Kiểm tra tính hợp lệ của chữ ký
            //if (!signer.CheckHashSignature(datasigned))
            //{
            //    insert_LOG_KYSO(reqSign, "0", req.idUserApp);
            //    return "";
            //}

            //Package external signature to signed file
            //byte[] signed = signer.Sign(datasigned);
            string pathFileOutput = Path.ChangeExtension(pathFileInput, ".pdf");
            try
            {
                // Xóa file
                File.Delete(pathFileInput);

                // Ghi file đã ký vào đường dẫn output
                File.WriteAllBytes(pathFileOutput, byteArray);

                // Tạo đường dẫn tương đối từ `wwwroot`
                string pathFileOutputDB = "/" + Path.GetRelativePath(_webHostEnvironment.WebRootPath, pathFileOutput).Replace("\\", "/");

                insert_LOG_KYSO(reqSign, "1", req.idUserApp);

                // Tạo QR code từ liên kết
                string qrCodeLink = baseURL + pathFileOutput;
                byte[] qrCodeImageBytes = GenerateQrCode(qrCodeLink); // Generate QR code as byte[]

                // Chèn QR code vào file PDF đã ký
                AddQrCodeToPdf(pathFileOutput, qrCodeImageBytes);

                return pathFileOutputDB;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu không ghi được file
                return "";
            }
        }

        public async Task<ResponseInsertSignature> InsertSignatureToFile(string url, RequestInsertSignature req)
        {
            try
            {
                // Tạo client và request
                RestClient client = new RestClient(url);
                RestRequest request = new RestRequest(Method.POST);
                // Thêm header Content-Type
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(req);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<ResponseInsertSignature>(response.Content);
                }
                else
                {
                    // Trả về đối tượng ResponseInsertSignature với thông tin lỗi
                    return new ResponseInsertSignature
                    {
                        Success = false,
                        Message = $"Error: {response.StatusCode}, {response.Content}"
                    };
                }
            }
            catch (Exception ex)
            {
                // Trả về đối tượng ResponseInsertSignature với thông báo lỗi
                return new ResponseInsertSignature
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }


        // Hàm tạo QR code
        public static byte[] GenerateQrCode(string link)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                // Tạo dữ liệu mã QR từ chuỗi liên kết
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);

                // Tạo mã QR dưới dạng mảng byte PNG
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    // Trả về mảng byte của QR code (PNG)
                    return qrCode.GetGraphic(20);  // Trả về byte[] trực tiếp, không cần Bitmap
                }
            }
        }



        public static void AddQrCodeToPdf(string inputPdfPath, byte[] qrCodeImageBytes)
        {
            string tempFilePath = Path.Combine(Path.GetDirectoryName(inputPdfPath), Path.GetFileNameWithoutExtension(inputPdfPath) + "_temp.pdf");

            try
            {
                // Tạo một đối tượng Image từ mảng byte
                using (MemoryStream ms = new MemoryStream(qrCodeImageBytes))
                {
                    Image qrCodeImage = Image.GetInstance(ms.ToArray());

                    // Mở file PDF gốc và tạo file PDF tạm thời
                    using (PdfReader reader = new PdfReader(inputPdfPath))
                    using (FileStream fs = new FileStream(tempFilePath, FileMode.Create))
                    using (PdfStamper stamper = new PdfStamper(reader, fs))
                    {
                        int totalPages = reader.NumberOfPages;

                        // Kích thước QR Code (1cm = 28.35pt)
                        float qrCodeSize = 33.35f;

                        // Lặp qua tất cả các trang của file PDF và thêm hình ảnh
                        for (int pageNum = 1; pageNum <= totalPages; pageNum++)
                        {
                            // Lấy trang hiện tại
                            PdfContentByte canvas = stamper.GetOverContent(pageNum);

                            // Vị trí thêm QR Code vào góc dưới bên trái của trang
                            float x = 20;
                            float y = reader.GetPageSize(pageNum).GetBottom(0) + 20; // Đặt vị trí đúng cho tất cả các trang

                            canvas.SetColorFill(BaseColor.WHITE); // Màu trắng để bôi vùng cũ
                            canvas.Rectangle(x, y, qrCodeSize, qrCodeSize); // Vùng hình chữ nhật
                            canvas.Fill();
                            // Điều chỉnh kích thước QR Code
                            qrCodeImage.SetAbsolutePosition(x, y);
                            qrCodeImage.ScaleToFit(qrCodeSize, qrCodeSize); // Điều chỉnh kích thước QR Code

                            // Thêm hình ảnh vào trang
                            canvas.AddImage(qrCodeImage);
                        }
                    }
                }

                // Sau khi thêm QR Code, thay thế file PDF gốc bằng file tạm thời
                File.Replace(tempFilePath, inputPdfPath, null);
                Console.WriteLine("QR Code đã được thêm vào tất cả các trang PDF.");
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có vấn đề
                throw new Exception("Lỗi khi thêm QR code vào PDF: " + ex.Message, ex);
            }
        }



        /// <summary>
        /// Hàm ghi log ký số vào database
        /// </summary>
        /// <param name="req"></param>
        /// <param name="status"></param>
        /// <param name="userID_app"></param>
        /// <returns></returns>
        public string insert_LOG_KYSO(ReqSign req, string status, string userID_app)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_HUY.insert_LOG_KYSO", "p_Error",
                    "p_TRANSACTION_ID", "p_SP_PASSWORD", "p_USER_ID_SIGN", "p_TRANSACTION_DESC", "p_SERIAL_NUMBER"
                    , "p_SP_ID", "p_STATUS_SIGN", "p_USER_ID_APP",
                    req.transaction_id, req.sp_password, req.user_id, req.transaction_desc, req.serial_number, req.sp_id, status, userID_app
                );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lấy thông tin chứng thư của người dùng
        private static UserCertificate _getAccountCert(String uri, SigningUser inforUser)
        {
            var response = Query(new ReqGetCert
            {
                sp_id = client_id,
                sp_password = client_secret,
                user_id = inforUser.userID,
                serial_number = inforUser.serial_number,
                transaction_id = Guid.NewGuid().ToString()
            }, uri);
            if (response != null)
            {
                ResGetCert res = JsonConvert.DeserializeObject<ResGetCert>(response);

                if (res.data.user_certificates.Count() == 1)
                {
                    return res.data.user_certificates[0];
                }
                else if (res.data.user_certificates.Count() > 1)
                {

                    String certIndex = Console.ReadLine();
                    int certIn;
                    bool isNumber = int.TryParse(certIndex, out certIn);
                    if (!isNumber)
                    {
                        return null; ;
                    }
                    if (certIn < 0 || certIn >= res.data.user_certificates.Count())
                    {
                        return null;
                    }
                    return res.data.user_certificates[certIn];

                }
                else
                {
                    return null;
                }

            }
            return null;

        }

        //Gửi request ký số
        private static DataSign _sign(String uri, string data_to_be_signed, SigningUser user, String descSign, string transactionID, out ReqSign reqS)
        {


            var sign_files = new List<SignFile>();
            var sign_file = new SignFile();
            sign_file.data_to_be_signed = data_to_be_signed;
            sign_file.doc_id = data_to_be_signed;
            sign_file.file_type = "pdf";
            sign_file.sign_type = "hash";
            sign_files.Add(sign_file);
            var req = new ReqSign
            {
                sp_id = client_id,
                sp_password = client_secret,
                user_id = user.userID,
                transaction_desc = descSign,
                transaction_id = transactionID,
                sign_files = sign_files,
                serial_number = user.serial_number
            };
            reqS = req;
            //var response = Query(req, uri);
            //if (response != null)
            //{
            //    ResSign res = JsonConvert.DeserializeObject<ResSign>(response);
            //    return res.data;
            //}
            return null;
        }


        private static String Query(object req, string serviceUri)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback
                += new RemoteCertificateValidationCallback(SslHelper.ValidateRemoteCertificate);

            RestClient client = new RestClient(serviceUri);
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(req);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = null;
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                //_log.Error($"Connect gateway error: {ex.Message}", ex);
                return null;
            }

            if (response == null || response.ErrorException != null)
            {
                //_log.Error("Service return null response");
                return null;
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //_log.Error($"Status code={response.StatusCode}. Status content: {response.Content}");
                return null;
            }

            return response.Content;
        }

        private static DataTransaction _getStatus(String uri)
        {
            var response = Query(new Object
            {
            }, uri);
            if (response != null)
            {
                ResStatus res = JsonConvert.DeserializeObject<ResStatus>(response);
                return res.data;
            }
            return null;
        }
    }


}
