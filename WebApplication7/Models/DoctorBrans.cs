using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication7.Models
{
    public class DoctorBrans
    {
        [Key] //primary key  
        public int Id { get; set; }

        [Required(ErrorMessage = "Branş adı boş bırakılamaz.")] //not null
        [MaxLength(25)]
        [DisplayName("Doktorun Branşı:")]
        public string Ad { get; set; }

    }
}
