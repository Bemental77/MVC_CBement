using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_HelloWorld.Models
{
    [Table("AppliedPerson")]
    public class AppliedPerson
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [ForeignKey("ContactReason")]
        public int ContactReasonId { get; set; }
        public virtual ContactReason ContactReason { get; set; }
    }
}