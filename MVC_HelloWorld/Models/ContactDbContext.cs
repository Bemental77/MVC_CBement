using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_HelloWorld.Models
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<AppliedPerson> AppliedPeople { get; set; }

        public System.Data.Entity.DbSet<MVC_HelloWorld.Models.ContactReason> ContactReasons { get; set; }
    }
}