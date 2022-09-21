using System.ComponentModel.DataAnnotations;

namespace NetAzure_DemoApi.Models.Forms
{
    public class AddToDoForm
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
    }
}
