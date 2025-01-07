using System.Collections.Generic;
using System;

namespace API_PCHY.Models.SMART_CA
{
    public class Model_SMART_CA
    {
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

        public class RequestSign
        {
            public SigningUser userSign { get; set; }
            public string pathFileIn_Out { get; set; }
            public string descSign { get; set; }
            public string fullNameUser { get; set; }
            //idUser của app phục vụ cho việc tạo báo cáo 
            public string idUserApp { get; set; }
        }

    }
}
