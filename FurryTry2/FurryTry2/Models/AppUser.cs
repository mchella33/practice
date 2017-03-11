using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace FurryTry2.Models
{
    public class AppUser
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public bool RememberMe { get; set; }

    }
}