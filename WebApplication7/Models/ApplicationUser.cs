﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class ApplicationUser
    {
        [Required]
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
