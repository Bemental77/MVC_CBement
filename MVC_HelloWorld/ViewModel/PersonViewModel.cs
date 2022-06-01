using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HelloWorld.ViewModel
{
    public class PersonViewModel
    {

        [Display(Name = "Person Name")]
        [Required(ErrorMessage = "Must provide a name")]
        public String Name { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Must provide an email address")]
        public String EmailAddress { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }
        public int ContactReasonId { get; set; }
    }
}