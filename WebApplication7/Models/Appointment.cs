using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PatientName { get; set; } 

        [ValidateNever]
        public int? DoctorId { get; set; }
        [ValidateNever]
        public int? WorkingTimes { get; set; }

        [Required(ErrorMessage = "Doktor seçimi zorunludur.")]
        [ForeignKey(nameof(DoctorId))]
        [ValidateNever]
        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "Branş seçimi zorunludur.")]
        [Display(Name = "Branş")]
        public int? BransId { get; set; } // Yeni eklenen özellik
        [ForeignKey(nameof(BransId))]
        [ValidateNever]
        public DoctorBrans? Brans { get; set; }
    }
}
