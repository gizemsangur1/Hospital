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

        [ForeignKey(nameof(DoctorId))]
        [ValidateNever]
        public Doctor? Doctor { get; set; }
    }
}
