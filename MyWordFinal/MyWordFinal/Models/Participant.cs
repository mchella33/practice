using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWordFinal.Models
{
    public class Participant
    {
        public System.Guid ParticipantId { get; set; }
        [Required(ErrorMessage = "Email required.", AllowEmptyStrings = false)]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public bool RememberMe { get; set; }
    }
}