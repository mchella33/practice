using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWordFinal.Models
{
    public class RegisterViewModel : Participant
    {

        
        [StringLength(50)]
        [Required(ErrorMessage = "First Name required.", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Last Name required.", AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Display Name required.", AllowEmptyStrings = false)]
        [StringLength(50)]
        public string DisplayName { get; set; }
        public string Comment { get; set; }
        
    }
}