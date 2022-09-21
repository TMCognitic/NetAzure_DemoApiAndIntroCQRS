using System.ComponentModel.DataAnnotations;

namespace NetAzure_DemoApi.Models.Forms
{
    public class RegisterForm
    {
        [Required]
        [EmailAddress]
        [MaxLength(384)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string Passwd { get; set; }
    }
}
