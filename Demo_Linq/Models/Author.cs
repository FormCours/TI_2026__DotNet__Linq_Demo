using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Linq.Models
{
    //Des Auteurs (Nom, Prenom, Date de naissance et mort)

    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public Author(int id, string firstName, string lastName, DateTime birthDate, DateTime? deathDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DeathDate = deathDate;
        }

        public override string ToString()
        {
            return $"{Id} - {FirstName} {LastName}";
        }
    }
}
