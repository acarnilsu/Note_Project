using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Note.Web.Areas.Manager.ViewModels
{
    public class PasswordChangeVM
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Eski Şifreniz")]
        public string PasswordOld { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Yeni Şifreniz")]

        public string PasswordNew { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Yeni Şifreniz Tekrar")]
        [Compare("PasswordNew", ErrorMessage = "Şifreler Eşlemiyor")]
        public string PasswordConfirm { get; set; }
    }
}
