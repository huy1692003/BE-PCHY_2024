using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Org.BouncyCastle.Ocsp;
using SignService.Common.HashSignature.Common;
using SignService.Common.HashSignature.Interface;
using SignService.Common.HashSignature.Pdf;
using SignService.Common.HashSignature.Xml;
using SmartCATHNetCore;
using Microsoft.AspNetCore.Hosting;
using static API_PCHY.Models.SMART_CA.Model_SMART_CA;
using APIPCHY.Helpers;
using APIPCHY_PhanQuyen.Models.QLKC.DM_PHONGBAN;

namespace API_PCHY.Models.SMART_CA
{
    public class SmartCA769
    {

        private static string client_id = "4b0c-638712580414678530.apps.smartcaapi.com";
        private static string client_secret = "ODFlMTAxYjE-NjYxOS00YjBj";
        private readonly IWebHostEnvironment _webHostEnvironment;
        DataHelper helper = new DataHelper();

        public SmartCA769(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //Hàm ký số 
        public bool _signSmartCA(RequestSign req , out string relativePathOutput)
        {
            relativePathOutput = null;
            // Lấy chứng chỉ người dùng
            var userCert = _getAccountCert("https://gwsca.vnpt.vn/sca/sp769/v1/credentials/get_certificate", req.userSign);

            // Kiểm tra nếu không lấy được chứng chỉ
            if (userCert == null)
            {
                return false;
            }

            string certBase64 = userCert.cert_data;
            
            string pathFileInput = req.pathFileIn_Out.StartsWith("/")
            ? Path.Combine(_webHostEnvironment.WebRootPath, req.pathFileIn_Out.TrimStart('/'))
            : Path.Combine(_webHostEnvironment.WebRootPath, req.pathFileIn_Out);
            string pathFileOutput = Path.ChangeExtension(pathFileInput, ".pdf");

            byte[] unsignData = null;
            try
            {
                // Đọc nội dung file đầu vào
                unsignData = File.ReadAllBytes(pathFileInput);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu không đọc được file
                return false;
            }

            string extension = Path.GetExtension(pathFileInput)?.ToLower();

            // Xác định loại file để tạo signer tương ứng
            string signerType = extension switch
            {
                ".pdf" => HashSignerFactory.PDF,
                ".docx" or ".doc" => HashSignerFactory.OFFICE,
                ".xml" => HashSignerFactory.XML,
                _ => throw new NotSupportedException($"Không hỗ trợ định dạng file '{extension}'")
            };

            // Tạo signer từ HashSignerFactory
            IHashSigner signer = HashSignerFactory.GenerateSigner(unsignData, certBase64, null, signerType);

            // Đặt thuật toán hash
            signer.SetHashAlgorithm(MessageDigestAlgorithm.SHA256);

            var hashValue = signer.GetSecondHashAsBase64();

            // Chuyển đổi hashValue thành chuỗi ký
            var data_to_be_sign = BitConverter.ToString(Convert.FromBase64String(hashValue)).Replace("-", "").ToLower();

            var transactionID = Guid.NewGuid().ToString();

            var reqSign = new ReqSign();
            // Gửi yêu cầu ký dữ liệu
            DataSign dataSign = _sign("https://gwsca.vnpt.vn/sca/sp769/v1/signatures/sign", data_to_be_sign, req.userSign, req.descSign, transactionID ,out reqSign);

            var count = 0;
            var isConfirm = false;
            var datasigned = "";
            var mapping = "";
            DataTransaction transactionStatus;

            // Kiểm tra trạng thái ký dữ liệu
            while (count < 30 && !isConfirm)
            {
                transactionStatus = _getStatus(string.Format("https://gwsca.vnpt.vn/sca/sp769/v1/signatures/sign/{0}/status", dataSign.transaction_id));
                if (transactionStatus.signatures != null)
                {
                    datasigned = transactionStatus.signatures[0].signature_value;
                    mapping = transactionStatus.signatures[0].doc_id;
                    isConfirm = true;
                }
                else
                {
                    count++;
                    Thread.Sleep(10000); // Chờ trước khi thử lại
                }
            }

            // Nếu không nhận được chữ ký, trả về thất bại
            if (!isConfirm || string.IsNullOrEmpty(datasigned))
            {
                insert_LOG_KYSO(reqSign, "0", req.idUserApp);
                return false;
            }

            // Kiểm tra tính hợp lệ của chữ ký
            if (!signer.CheckHashSignature(datasigned))
            {
                insert_LOG_KYSO(reqSign, "0", req.idUserApp);
                return false;

            }

            // Package external signature to signed file
            byte[] signed = signer.Sign(datasigned);
            try
            {
                // Ghi file đã ký vào đường dẫn output
                File.WriteAllBytes(pathFileOutput, signed);

                // Tạo đường dẫn tương đối từ `wwwroot`
                relativePathOutput = "/" + Path.GetRelativePath(_webHostEnvironment.WebRootPath, pathFileOutput).Replace("\\", "/");
                insert_LOG_KYSO(reqSign, "1",req.idUserApp );
                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu không ghi được file
                return false;
            }
        }


        /// <summary>
        /// Hàm ghi log ký số vào database
        /// </summary>
        /// <param name="req"></param>
        /// <param name="status"></param>
        /// <param name="userID_app"></param>
        /// <returns></returns>
        public string insert_LOG_KYSO(ReqSign req , string status ,string userID_app)
        {
            try
            {
                string result = helper.ExcuteNonQuery("PKG_QLTN_QUANTRI.insert_LOG_KYSO", "p_Error",
                    "p_TRANSACTION_ID", "p_SP_PASSWORD", "p_USER_ID_SIGN", "p_TRANSACTION_DESC", "p_SERIAL_NUMBER"
                    , "p_SP_ID", "p_STATUS_SIGN", "p_USER_ID_APP",
                    req.transaction_id,req.sp_password,req.user_id,req.transaction_desc,req.serial_number,req.sp_id,status,userID_app
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
        private static DataSign _sign(String uri, string data_to_be_signed, SigningUser user, String descSign, string transactionID , out ReqSign reqS)
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
            var response = Query(req, uri);
            if (response != null)
            {
                ResSign res = JsonConvert.DeserializeObject<ResSign>(response);
                return res.data;
            }
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
