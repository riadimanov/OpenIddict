using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OpenIddict.AuthorizationServer.Validations
{
    public class LoginVMMetadata
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adını boş geçmeyiniz...")]
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz...")]
        [DisplayName("Şifre")]
        public string Password { get; set; }
    }
}
