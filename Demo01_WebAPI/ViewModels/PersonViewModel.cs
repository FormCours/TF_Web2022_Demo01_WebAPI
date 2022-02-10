using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo01_WebAPI.ViewModels
{
   // ViewModel utilisé par l'API pour l'ajout de donnée (POST)
   public class PersonAddViewModel
   {
      public string Firstname { get; set; }

      [Required]
      [RegularExpression("[A-Za-z]{2,}")]
      public string Lastname { get; set; }

      [Required(AllowEmptyStrings = false)]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      public DateTime Birthdate { get; set; }
   }


   // ViewModel utiliser lors d'un update
   public class PersonUpdateViewModel
   {
      public string Firstname { get; set; }

      [Required]
      [RegularExpression("[A-Za-z]{2,}")]
      public string Lastname { get; set; }

      [Required]
      public DateTime Birthdate { get; set; }
   }


   // ViewModel utiliser lors d'un update
   public class PersonPartialUpdateViewModel
   {
      public string Firstname { get; set; }

      [RegularExpression("[A-Za-z]{2,}")]
      public string Lastname { get; set; }

      public DateTime? Birthdate { get; set; }
   }
}
