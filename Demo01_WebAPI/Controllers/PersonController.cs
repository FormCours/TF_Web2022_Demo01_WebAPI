using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo01_WebAPI.Models;
using System.Net;
using Demo01_WebAPI.ViewModels;

namespace Demo01_WebAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PersonController : ControllerBase
   {
      // Liste de données statique (Simulation d'acces a des données)
      private static List<Person> peopleDB = new List<Person>()
        {
            new Person(Guid.NewGuid(), "Riri", "Duck", "r.duck@gmail.com", new DateTime(2012, 5, 13)),
            new Person(Guid.NewGuid(), "Zaza", "Vanderquack", "z.vanderquack@outlook.be", new DateTime(2011, 12, 1)),
            new Person(Guid.NewGuid(), "Archibald", "Gripsou", "a.gripsou@caramail.com", new DateTime(1970, 1, 1)),
            new Person(Guid.NewGuid(), "Balthazar", "Picsou", "b.picsou@money.org", new DateTime(1965, 3, 13)),
            new Person(Guid.NewGuid(), "Della", "Duck", "della.duck@hotmail.com", new DateTime(1982, 4, 5))
        };

      // La méthode est implicitement de type « GET »
      public IActionResult GetAll()
      {
         IEnumerable<Person> people = PersonController.peopleDB.ToList();

         // ↓ La méthode « Ok » permet d'envoyer un resultat avec le status 200 et les données
         return Ok(people);
      }

      [HttpGet] // Définition explicite du type « GET »
      [Route("{personId}")] // Ajout d'un sufixe sur la route
      public IActionResult GetById(Guid personId)
      {
         Person person = PersonController.peopleDB.SingleOrDefault(p => p.PersonId == personId);

         if (person is null)
         {
            //return StatusCode(404);
            //return StatusCode((int) HttpStatusCode.NotFound);
            return NotFound();
         }

         return Ok(person);
      }


      [HttpGet]
      [Route("search")]
      public IActionResult GetByLastname([FromQuery]string lastname)
      {
         if(string.IsNullOrWhiteSpace(lastname))
         {
            return BadRequest();
         }

         IEnumerable<Person> people = peopleDB.Where(p => p.Lastname.Contains(lastname, StringComparison.InvariantCultureIgnoreCase));

         return Ok(people);
      }


      [HttpPost]
      public IActionResult AddPerson(PersonAddViewModel person)
      {
         // Vérification de l'email → Si non-unique, envoie d'une erreur 400
         if (peopleDB.Exists(p => p.Email.ToLower() == person.Email.ToLower()))
         {
            return BadRequest(new
            {
               Error = "L'email est déjà utilisé"
            });
         }

         // Mapping « PersonAddViewModel » => « Person »
         Person newPerson = new Person(
             Guid.NewGuid(),
             person.Firstname,
             person.Lastname,
             person.Email,
             person.Birthdate
         );

         // Ajout des données
         PersonController.peopleDB.Add(newPerson);

         // Reponse à l'utilisateur
         return Ok(newPerson);
      }


      [HttpPut]
      [Route("{personId}")]
      public IActionResult UpdatePerson(Guid personId, PersonUpdateViewModel person)
      {
         // Test de garde pour check si l'element existe
         if (!peopleDB.Exists(p => p.PersonId == personId))
         {
            return NotFound();
         }

         // Mise à jours des données
         Person target = peopleDB.Where(p => p.PersonId == personId).Single();
         target.Firstname = person.Firstname;
         target.Lastname = person.Lastname;
         target.Birthdate = person.Birthdate;

         // Reponse à l'utilisateur
         return Ok(target);
      }


      [HttpPatch]
      [Route("{personId}")]
      public IActionResult PartialUpdatePerson(Guid personId, PersonPartialUpdateViewModel person)
      {
         Person target = peopleDB.Where(p => p.PersonId == personId).SingleOrDefault();

         // Check si l'element existe → Sinon, envoi d'une erreur 404
         if (target is null)
         {
            return NotFound();
         }

         // Mise à jours des données
         target.Firstname = person.Firstname != null ? person.Firstname : target.Firstname;
         target.Lastname = person.Lastname ?? target.Lastname;
         target.Birthdate = person.Birthdate ?? target.Birthdate;

         // Reponse à l'utilisateur
         return Ok(target);
      }


      [HttpDelete]
      [Route("{personId}")]
      public IActionResult DeletePerson(Guid personId)
      {
         Person target = peopleDB.Where(p => p.PersonId == personId).SingleOrDefault();

         // Envoi d'une erreur 400 si l'element n'existe pas :(
         if(target is null)
         {
            return BadRequest(new
            {
               Error = "L'id n'est pas valide"
            });
         }

         // Suppression des données
         peopleDB.Remove(target);

         // Response à l'utilisateur
         return NoContent();
      }

   }
}
