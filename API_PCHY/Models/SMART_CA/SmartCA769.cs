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

namespace API_PCHY.Models.SMART_CA
{
    public class SmartCA769
    {

        private static string client_id = "4b0c-638712580414678530.apps.smartcaapi.com";
        private static string client_secret = "ODFlMTAxYjE-NjYxOS00YjBj";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SmartCA769(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void _signSmartCA(RequestSign req)
        {
            var userCert = _getAccountCert("https://gwsca.vnpt.vn/sca/sp769/v1/credentials/get_certificate",req.userSign);

            if (userCert == null)
            {
                return;
            }
            String certBase64 = userCert.cert_data;

              /// Đường dẫn đến file cần ký và sẽ lưu trực tiếp lại vào đường dẫn cũ              
            string pathFileInput=Path.Combine(_webHostEnvironment.WebRootPath, req.pathFileIn_Out);
            string pathFileOutput = Path.ChangeExtension(pathFileInput, ".pdf");


            byte[] unsignData = null;
            try
            {
                unsignData = File.ReadAllBytes(pathFileInput);
            }
            catch (Exception ex)
            {
                return;
            }

            string extension = Path.GetExtension(pathFileInput)?.ToLower();

            string signerType = extension switch
            {
                ".pdf" => HashSignerFactory.PDF,
                ".docx" or ".doc" => HashSignerFactory.OFFICE,
                ".xml" => HashSignerFactory.XML,
                _ => throw new NotSupportedException($"Không hỗ trợ định dạng file '{extension}'")
            };

            // Tạo signer từ HashSignerFactory
            IHashSigner signer = HashSignerFactory.GenerateSigner(unsignData, certBase64, null, signerType);

            signer.SetHashAlgorithm(MessageDigestAlgorithm.SHA256);


            var hashValue = signer.GetSecondHashAsBase64();

            var data_to_be_sign = BitConverter.ToString(Convert.FromBase64String(hashValue)).Replace("-", "").ToLower();

            var transactionID = Guid.NewGuid().ToString();

            //DataSign dataSign = _sign("https://rmgateway.vnptit.vn/sca/sp769/v1/signatures/sign", data_to_be_sign, userCert.serial_number);
            DataSign dataSign = _sign("https://gwsca.vnpt.vn/sca/sp769/v1/signatures/sign", data_to_be_sign,req.userSign,req.descSign,transactionID);

            //Console.ReadKey();

            var count = 0;
            var isConfirm = false;
            var datasigned = "";
            var mapping = "";
            DataTransaction transactionStatus;

            while (count < 30 && !isConfirm)
            {
                //transactionStatus = _getStatus(string.Format("https://rmgateway.vnptit.vn/sca/sp769/v1/signatures/sign/{0}/status", dataSign.transaction_id));
                transactionStatus = _getStatus(string.Format("https://gwsca.vnpt.vn/sca/sp769/v1/signatures/sign/{0}/status", dataSign.transaction_id));
                if (transactionStatus.signatures != null)
                {
                    datasigned = transactionStatus.signatures[0].signature_value;
                    mapping = transactionStatus.signatures[0].doc_id;
                    isConfirm = true;
                }
                else
                {
                    count = count + 1;
                    Thread.Sleep(10000);
                }
            }
            if (!isConfirm)
            {
                return;
            }

            if (string.IsNullOrEmpty(datasigned))
            {
                return;
            }
            if (!signer.CheckHashSignature(datasigned))
            {
                return;
            }
            // ------------------------------------------------------------------------------------------

            // 3. Package external signature to signed file
            byte[] signed = signer.Sign(datasigned);
            File.WriteAllBytes(pathFileOutput, signed);

        }

       
      
        private static UserCertificate _getAccountCert(String uri, SigningUser inforUser )
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

        private static DataSign _sign(String uri, string data_to_be_signed, SigningUser user , String descSign, string transactionID)
        {


            var sign_files = new List<SignFile>();
            var sign_file = new SignFile();
            sign_file.data_to_be_signed = data_to_be_signed;
            sign_file.doc_id = data_to_be_signed;
            sign_file.file_type = "pdf";
            sign_file.sign_type = "hash";
            sign_files.Add(sign_file);
            var response = Query(new ReqSign
            {
                sp_id = client_id,
                sp_password = client_secret,
                user_id = user.userID,
                transaction_id = transactionID,
                transaction_desc = descSign,
                sign_files = sign_files,
                serial_number = user.serial_number,

            }, uri);
            if (response != null)
            {
                ResSign req = JsonConvert.DeserializeObject<ResSign>(response);
                return req.data;
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
    public class ResStatus
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public DataTransaction data { get; set; }
    }

    public class DataTransaction
    {
        public string transaction_id { get; set; }
        public List<Signature> signatures { get; set; }
    }

    public class Signature
    {
        public string doc_id { get; set; }
        public string signature_value { get; set; }
        public object timestamp_signature { get; set; }
    }

    public class ReqGetCert
    {
        public string sp_id { get; set; }
        public string sp_password { get; set; }
        public string user_id { get; set; }
        public string serial_number { get; set; }
        public string transaction_id { get; set; }
    }

    public class GetCertData
    {
        public List<UserCertificate> user_certificates { get; set; }
    }
    public class UserCertificate
    {
        public string service_type { get; set; }
        public string service_name { get; set; }
        public string cert_id { get; set; }
        public string cert_status { get; set; }
        public string serial_number { get; set; }
        public string cert_subject { get; set; }
        public DateTime cert_valid_from { get; set; }
        public DateTime cert_valid_to { get; set; }
        public string cert_data { get; set; }
        public ChainData chain_data { get; set; }
        public string transaction_id { get; set; }
    }
    public class ChainData
    {
        public string ca_cert { get; set; }
        public object root_cert { get; set; }
    }

    public class ResGetCert
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public GetCertData data { get; set; }
    }

    public class DataSign
    {
        public string transaction_id { get; set; }
        public string tran_code { get; set; }
    }
    public class SignFile
    {
        public string data_to_be_signed { get; set; }
        public string doc_id { get; set; }
        public string file_type { get; set; }
        public string sign_type { get; set; }
    }

    public class ReqSign
    {
        public string sp_id { get; set; }
        public string sp_password { get; set; }
        public string user_id { get; set; }
        public string transaction_desc { get; set; }
        public string transaction_id { get; set; }
        public List<SignFile> sign_files { get; set; }
        public string serial_number { get; set; }
    }

    public class ResSign
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public DataSign data { get; set; }
    }

    public class SigningUser
    {
        public string userID { get; set; }  
        public string serial_number { get; set; }  
    }

}
