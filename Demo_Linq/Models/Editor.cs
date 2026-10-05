using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Linq.Models
{
    public class Editor
    {
        //Editeur (Nom, desc, Pays, Actif)
        public int Id { get; set; }  //ou EditorId ou IdEditor
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }

        public Editor(int id, string name, string? desc, string country, bool isActive)
        {
            this.Id = id;
            this.Name = name;
            this.Description = desc;
            this.Country = country;
            this.IsActive = isActive;
        }
        public override string ToString()
        {
            if (IsActive)
            {
                return $"{Id} - {Name}, {Country}";
            }
            return $"{Id} - {Name}, {Country} (Inactive)";
        }
    }
}
