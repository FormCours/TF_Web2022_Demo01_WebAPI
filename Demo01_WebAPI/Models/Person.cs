using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo01_WebAPI.Models
{
   public class Person
   {
      public Guid PersonId { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public string Email { get; set; }
      public DateTime Birthdate { get; set; }

      public Person(Guid personId, string firstname, string lastname, string email, DateTime birthdate)
      {
         this.PersonId = personId;
         this.Firstname = firstname;
         this.Lastname = lastname;
         this.Email = email;
         this.Birthdate = birthdate;
      }
   }
}
