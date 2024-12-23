using System.ComponentModel.DataAnnotations;

namespace APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AppSettings
    {
        public string Secret { get; set; }

    }
}
