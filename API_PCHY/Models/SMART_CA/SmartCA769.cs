using Newtonsoft.Json;
using RestSharp;
using SmartCANetCore;
using System.Collections.Generic;
using System.Net.Security;
using System.Net;
using System;
using System.Linq;
using System.Threading;
using VnptHashSignatures.Common;
using VnptHashSignatures.Interface;
using System.IO;

namespace API_PCHY.Models.SMART_CA
{
    public class SmartCA769
    {
        public SmartCA769()
        {
            
        }
        private static string client_id = "45bc-638708935424865482.apps.smartcaapi.com";
        private static string client_secret = "NjM3M2UxM2M-NTQ4NC00NWJj";
        private static string uid = "002087000080";


        //private static string _pdfInput = @"C:\Users\accca\Desktop\test.pdf";
        //private static string _pdfSignedPath = @"C:\Users\accca\Desktop\test_signed.pdf";

        //private static string _xmlInput = @"C:\Users\accca\Desktop\test.xml";
        //private static string _xmlSignedPath = @"C:\Users\accca\Desktop\test_signed.xml";

        private static string _officeInput = @"D:\Thực tập 2024\PCHY\BE-PCHY-2024\API_PCHY\wwwroot\fileBBTN\testkyso.docx";
        private static string _officeSignedPath = @"C:\Users\huy16\Downloads\Output";

        //private static string _cmsInput = @"C:\Users\accca\Desktop\test.txt";
        //private static string _cmsSignedPath = @"C:\Users\accca\Desktop\test_signed.txt";









        public  void _signSmartCAOFFICE()
        {
            var userCert = _getAccountCert("https://rmgateway.vnptit.vn/sca/sp769/v1/credentials/get_certificate");
            //var userCert = _getAccountCert("https://gwsca.vnpt.vn/sca/sp769/v1/credentials/get_certificate");
            if (userCert == null)
            {
                Console.WriteLine("not found cert");
                return;
            }
            String certBase64 = userCert.cert_data;


            byte[] unsignData = null;
            try
            {
                unsignData = File.ReadAllBytes(_officeInput);
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
                return;
            }
            IHashSigner signer = HashSignerFactory.GenerateSigner(unsignData, certBase64, null, HashSignerFactory.OFFICE);
            signer.SetHashAlgorithm(MessageDigestAlgorithm.SHA256);


            var hashValue = signer.GetSecondHashAsBase64();

            var data_to_be_sign = BitConverter.ToString(Convert.FromBase64String(hashValue)).Replace("-", "").ToLower();

            DataSign dataSign = _sign("https://rmgateway.vnptit.vn/sca/sp769/v1/signatures/sign", data_to_be_sign, userCert.serial_number);

            Console.WriteLine(string.Format("Wait for user confirm: Transaction_id = {0}", dataSign.transaction_id));
            //Console.ReadKey();

            var count = 0;
            var isConfirm = false;
            var datasigned = "";
            DataTransaction transactionStatus;

            while (count < 30 && !isConfirm)
            {
                transactionStatus = _getStatus(string.Format("https://rmgateway.vnptit.vn/sca/sp769/v1/signatures/sign/{0}/status", dataSign.transaction_id));
                if (transactionStatus.signatures != null)
                {
                    datasigned = transactionStatus.signatures[0].signature_value;
                    isConfirm = true;
                }
                else
                {
                    count = count + 1;
                    Console.WriteLine(string.Format("Wait for user confirm count : {0}", count));
                    Thread.Sleep(10000);
                }
            }
            if (!isConfirm)
            {
                Console.WriteLine(string.Format("Signer not confirm from App"));
                return;
            }

            if (string.IsNullOrEmpty(datasigned))
            {
                Console.WriteLine("Sign error");
                return;
            }

            // ------------------------------------------------------------------------------------------

            // 3. Package external signature to signed file
            byte[] signed = signer.Sign(datasigned);
            File.WriteAllBytes(_officeSignedPath, signed);

        }


        private static UserCertificate _getAccountCert(String uri)
        {
            var response = Query(new ReqGetCert
            {
                sp_id = "425b-638709086741425341.apps.smartcaapi.com",
                sp_password = "NTlkZWJlNzY-MzdkNi00MjVi",
                user_id = "002087000080",
                serial_number = "540101016946c14b754c1f902c13be8e",
                transaction_id = "3424fe2"
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
                    for (int i = 0; i < res.data.user_certificates.Count(); i++)
                    {
                        Console.WriteLine("--------------");
                        Console.WriteLine("Certificate index : " + i);
                        Console.WriteLine("service_type : " + res.data.user_certificates[i].service_type);
                        Console.WriteLine("service_name : " + res.data.user_certificates[i].service_name);
                        Console.WriteLine("serial_number : " + res.data.user_certificates[i].serial_number);
                        Console.WriteLine("cert_subject : " + res.data.user_certificates[i].cert_subject);
                        Console.WriteLine("cert_valid_from : " + res.data.user_certificates[i].cert_valid_from);
                        Console.WriteLine("cert_valid_to : " + res.data.user_certificates[i].cert_valid_to);
                    }
                    Console.WriteLine("Choose Certificate index :");
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

        private static DataSign _sign(String uri, string data_to_be_signed, String serialNumber)
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
                user_id = uid,
                transaction_id = Guid.NewGuid().ToString(),
                transaction_desc = "Ký Test từ NgoQuangDat",
                sign_files = sign_files,
                serial_number = serialNumber,

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
}

