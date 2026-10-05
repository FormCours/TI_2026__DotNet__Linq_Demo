using Demo_Linq;
using Demo_Linq.Models;

Console.WriteLine("Demo LinQ");

/*
// Exemple d'intro à Linq : Obtenir une liste avec les noms qui contient la lettre "a"
List<string> names = ["Della", "Riri", "Fifi", "Zaza", "Donald", "Balthazar", "Gontrant", "Loulou"];

// - Sans
List<string> result1 = [];
foreach (string name in names)
{
    if (name.ToLower().Contains("a"))
    {
        result1.Add(name);
    }
}

// - Avec 
IEnumerable<string> result2 = names.Where(n =>
{;
    return n.ToLower().Contains("a");
});

foreach(string r in result2)
{
    Console.WriteLine(r);
}
*/

// Demo Linq
DataContext da = new DataContext();

List<object> demo01 = [];
demo01.AddRange(da.Editors);
demo01.AddRange(da.Authors);
demo01.AddRange(da.Shelves);
Console.WriteLine($"Nombre d'élément dans demo01 : {demo01.Count}");


IEnumerable<Author> authors01 = demo01.Where(o => o.GetType() == typeof(Author)).Cast<Author>();
IEnumerable<Book> books02 = demo01.OfType<Book>();
Console.WriteLine($"Nombre d'auteurs dans demo01 : {authors01.Count()}");
Console.WriteLine($"Nombre de livre dans demo01 : {books02.Count()}");
Console.WriteLine();

// Liste des auteurs vivant, afficher le prénom et le nom 
var r1 = da.Authors.Where(a => a.DeathDate == null)
                   .Select(a => new { Nom = a.LastName, Prenom = a.FirstName });

Console.WriteLine("Liste des personnes vivants : ");
foreach (var a in r1)
{
    Console.WriteLine(a);
}
Console.WriteLine();

// Liste des maisons d'édition active, afficher le nom
// -> La requete DOIT être sous forme d'expression
var r2 = from editor in da.Editors
         where editor.IsActive
         select new { Name = editor.Name };  // Autre solution : select editor.Name; 

Console.WriteLine("Liste des maisons d'édition active : ");
foreach (var edi in r2)
{
    Console.WriteLine(edi);
}
Console.WriteLine();

// Liste des différentes couleurs de theme de bibliotheque
var r3 = (from s in da.Shelves
          select new { s.Color }).Distinct();

Console.WriteLine("Liste des couleur: ");
foreach (var couleur in r3)
{
    Console.WriteLine(couleur);
}
Console.WriteLine();

// Afficher l'auteur vivant le plus vieux avec son nom et prénom
var r4 = (from a in da.Authors
          where a.DeathDate is null
          orderby a.BirthDate
          select new { a.FirstName, a.LastName }).First();

Console.WriteLine("l'auteur vivant le plus vieux: " + r4);
Console.WriteLine();

// Afficher l'age de l'auteur le plus agé (vivant ou mort)
var r5 = (from a in da.Authors
          select (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year).Max();

var r5_2 = da.Authors.Max(a => (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year);

Console.WriteLine($"Âge de l'auteur le plus âgé: {r5}");
Console.WriteLine();

// Affiche le nom et prénom de l'auteur le plus jeune
int age = da.Authors.Min(a => (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year);
var r6 = from a in da.Authors
         where (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year == age
         select a;

foreach (var a in r6)
{
    Console.WriteLine(a);
}

// - Version bonus
var temp = da.Authors.Select(a => new
{
    a.FirstName,
    a.LastName,
    age = (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year
});
var r7 = temp.Where(a => a.age == temp.Min(p => p.age));