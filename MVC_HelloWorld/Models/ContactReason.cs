using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_HelloWorld.Models
{
    [Table("ContactReason")]
    public class ContactReason
    {
        [Key]
        public int Id { get; set; }

        public String Reason { get; set; }
        public String Message { get; set; }
        
        public List<AppliedPerson> People { get; set; }
    }
}