using System.ComponentModel.DataAnnotations;

namespace Note.Web.ViewModels

{
    public class MessageVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bu alan Boş geçilemez")]
        public string Messages { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
