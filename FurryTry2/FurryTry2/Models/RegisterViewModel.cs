using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurryTry2.Models
{
    public class RegisterViewModel : AppUser
    {
        //unique id
        //display name
        //gender
        //gender seeking
        //picture
        //about me
        //age
        //city
        //country
        //jsonattributes - for my paws etc

        
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter a Display Name.", AllowEmptyStrings = false)]
        public string DisplayName { get; set; }
              
        [Required(ErrorMessage = "Please choose your Gender.")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please choose the Gender you are most attracted to.")]
        public Gender GenderSeeking { get; set; }
        public string Avatar { get; set; }
        [StringLength(2000)]
        [Required(ErrorMessage = "Please tell us about yourself and what you are looking for.", AllowEmptyStrings = false)]
        public string AboutMe { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter your City.", AllowEmptyStrings = false)]
        public string City { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter the Country you live in.", AllowEmptyStrings = false)]
        public string Country { get; set; }
        public string JsonAttributes { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter your First Name.", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Please choose Birth Year.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Please choose Birth Month.")]
        public int Month { get; set; }
        [Required(ErrorMessage = "Please choose Birth Day.")]
        public int Day { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Trans
    }


}