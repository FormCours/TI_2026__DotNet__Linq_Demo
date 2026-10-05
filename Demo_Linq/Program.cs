using Demo_Linq;
using Demo_Linq.Models;
using System.Collections;

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