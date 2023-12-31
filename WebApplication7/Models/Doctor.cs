using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Doktor Adı ve Soyadı")]
        public string DoctorName { get; set; }

        [DisplayName("Ana Bilim Dalı")]
        [ValidateNever]
        public int? DoctorBransId { get; set; }

        [ForeignKey("DoctorBransId")]
        [ValidateNever]
        public DoctorBrans? DoctorBrans { get; set; }

        [DisplayName("Poliklinik")]
        public string? Polyclinic { get; set; }

     
        [DisplayName("Çalışma Gün ve Saatleri")]
        public DateTime WorkingTimes { get; set; }

        

    }
}
