using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Contact
    {
        [Required]
        public string name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
    ErrorMessage = "Invalid email format.")]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Must be exactly 10 digits.")]
        public string mobile { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid  number. Must be exactly 10 digits.")]
        public string whatsapp { get; set; }

        [Required]
        public string message { get; set; }
        public string CaptchaResponse { get; set; }
    }
}