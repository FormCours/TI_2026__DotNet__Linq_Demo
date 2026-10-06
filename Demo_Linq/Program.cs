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
DataContext context = new DataContext();

List<object> demo01 = [];
demo01.AddRange(context.Editors);
demo01.AddRange(context.Authors);
demo01.AddRange(context.Shelves);
Console.WriteLine($"Nombre d'élément dans demo01 : {demo01.Count}");


IEnumerable<Author> authors01 = demo01.Where(o => o.GetType() == typeof(Author)).Cast<Author>();
IEnumerable<Book> books02 = demo01.OfType<Book>();
Console.WriteLine($"Nombre d'auteurs dans demo01 : {authors01.Count()}");
Console.WriteLine($"Nombre de livre dans demo01 : {books02.Count()}");
Console.WriteLine();

// Liste des auteurs vivant, afficher le prénom et le nom 
var r1 = context.Authors.Where(a => a.DeathDate == null)
                   .Select(a => new { Nom = a.LastName, Prenom = a.FirstName });

Console.WriteLine("Liste des personnes vivants : ");
foreach (var a in r1)
{
    Console.WriteLine(a);
}
Console.WriteLine();

// Liste des maisons d'édition active, afficher le nom
// -> La requete DOIT être sous forme d'expression
var r2 = from editor in context.Editors
         where editor.IsActive
         select new { Name = editor.Name };  // Autre solution : select editor.Name; 

Console.WriteLine("Liste des maisons d'édition active : ");
foreach (var edi in r2)
{
    Console.WriteLine(edi);
}
Console.WriteLine();

// Liste des différentes couleurs de theme de bibliotheque
var r3 = (from s in context.Shelves
          select new { s.Color }).Distinct();

Console.WriteLine("Liste des couleur: ");
foreach (var couleur in r3)
{
    Console.WriteLine(couleur);
}
Console.WriteLine();

// Afficher l'auteur vivant le plus vieux avec son nom et prénom
var r4 = (from a in context.Authors
          where a.DeathDate is null
          orderby a.BirthDate
          select new { a.FirstName, a.LastName }).First();

Console.WriteLine("l'auteur vivant le plus vieux: " + r4);
Console.WriteLine();

// Afficher l'age de l'auteur le plus agé (vivant ou mort)
var r5 = (from a in context.Authors
          select (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year).Max();

var r5_2 = context.Authors.Max(a => (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year);

Console.WriteLine($"Âge de l'auteur le plus âgé: {r5}");
Console.WriteLine();

// Affiche le nom et prénom de l'auteur le plus jeune
int age = context.Authors.Min(a => (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year);
var r6 = from a in context.Authors
         where (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year == age
         select a;

foreach (var a in r6)
{
    Console.WriteLine(a);
}

// - Version bonus
var temp = context.Authors.Select(a => new
{
    a.FirstName,
    a.LastName,
    age = (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year
});
var r7 = temp.Where(a => a.age == temp.Min(p => p.age));


Console.Clear();
Console.WriteLine("Group");

// Liste des étageres groupé par couleurs (GroupBy)
var r8 = context.Shelves.GroupBy(s => s.Color);

foreach (var elem in r8)
{
    Console.WriteLine($"Couleur : {elem.Key} - Nombre d'élément : {elem.Count()}");

    foreach (var item in elem)
    {
        Console.WriteLine($" - {item.Code} {item.CDU}");
    }
}

// Liste des auteurs groupé par l'initial de leur nom de famille
// Afficher : Initial, Le nombre d'auteur, l'age moyen
var r9_v1 = context.Authors.Select(a => new
{
    Initial = a.LastName[0],
    Age = (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year
})
                        .GroupBy(a => a.Initial)
                        .Select(g => new
                        {
                            g.Key,
                            Count = g.Count(),
                            Average_Age = g.Average(item => item.Age)
                        });

var r9_v2 = from g in (
                 from a in context.Authors
                 group a by a.LastName[0]
             )
            select new
            {
                g.Key,
                Count = g.Count(),
                Average_Age = g.Average(a => (a.DeathDate ?? DateTime.Today).Year - a.BirthDate.Year)
            };



foreach (var elem in r9_v1)
{
    Console.WriteLine(elem);
}


Console.Clear();
Console.WriteLine("Join");
// Obtenir la liste des livres avec leurs categories

var r10 = context.Books.Join(
        // Deuxieme source de donnée
        context.Categories,
        // Clef du coté de la source de données initial (Books)
        b => b.CategoryId,
        // Clef du coté de la deuxieme source de données (Categories)
        c => c.Id,
        // Fonction pour représenter les données après la fusion
        (b, c) => new
        {
            b.Title,
            b.Isbn,
            Categorie = c.Name,
        }
    );

foreach (var elem in r10)
{
    Console.WriteLine(elem);
}

// Afficher chaque éditeur, le nombre de livre par categorie qu'il édite
// Ordonnée les résultats par l'éditeur le plus prolifique (Le plus de livre)
Console.Clear();
Console.WriteLine();

// - Student version
var r11 = context.Books
                 .Join(context.Editors,
                        b => b.EditorId,
                        e => e.Id,
                        (b, e) => new
                        {
                            Editeur = e.Id,
                            Name = e.Name,
                            Categorie = b.CategoryId
                        }
                        ).GroupBy(e => e.Name)
                        .Select(e => new
                        {
                            Editor = e.Key,
                            BookCount = e.Count(),
                            Cate = e.GroupBy(ed => ed.Categorie)
                                    .Select(ed1 => new
                                    {
                                        IdCate = ed1.Key,
                                        NbElem = ed1.Count()
                                    })
                        })
                        .OrderByDescending(edi => edi.BookCount);

foreach (var r in r11)
{
    Console.WriteLine($"{r.Editor} ({r.BookCount})");
    foreach (var r_1 in r.Cate)
    {
        Console.WriteLine($" - {r_1.IdCate} {r_1.NbElem}");
    }
}

// - Correction version
var r11_v2 = context.Books.Join(context.Editors,
                    b => b.EditorId,
                    e => e.Id,
                    (b, e) => new
                    {
                        EditorId = e.Id,
                        EditorName = e.Name,
                        BookCatId = b.CategoryId,
                    })
                    .Join(context.Categories,
                    be => be.BookCatId,
                    c => c.Id,
                    (be, c) => new
                    {
                        be.EditorId,
                        be.EditorName,
                        CatId = c.Id,
                        CatName = c.Name
                    })
                    .GroupBy(be => new { be.EditorId, be.EditorName })
                    .Select(be => new
                    {
                        Editor = be.Key,
                        BooksCount = be.Count(),
                        BooksCat = be.GroupBy(bei => new { bei.CatId, bei.CatName })
                                        .Select(bei => new { Categorie = bei.Key, Count = bei.Count() })
                    })
                    .OrderByDescending(bei => bei.BooksCount);

Console.ReadLine();
Console.Clear();
Console.WriteLine();
foreach (var elem in r11_v2)
{
    Console.WriteLine($"Editeur : {elem.Editor.EditorName} ({elem.BooksCount})");

    foreach (var elemCount in elem.BooksCat)
    {
        Console.WriteLine($" - {elemCount.Categorie.CatName} : {elemCount.Count}");
    }
}


// - Correction avec un GroupJoin 

var r11_v3 = context.Editors.GroupJoin(
                            context.Books,
                            e => e.Id,
                            b => b.EditorId,
                            (editor, books) => new
                            {
                                EditorId = editor.Id,
                                EditorName = editor.Name,
                                BooksCount = books.Count(),
                                Cat = context.Categories.GroupJoin(
                                                        books,
                                                        c => c.Id,
                                                        b => b.CategoryId,
                                                        (cat, books) => new
                                                        {
                                                            CategoryName = cat.Name,
                                                            CategoryId = cat.Id,
                                                            Count = books.Count()
                                                        }).Where(cb => cb.Count > 0)
                            })
                            .OrderByDescending(ebc => ebc.BooksCount);

Console.ReadLine();
Console.Clear();
Console.WriteLine();
foreach (var elem in r11_v3)
{
    Console.WriteLine($"Editeur : {elem.EditorName} ({elem.BooksCount})");

    foreach (var elemCount in elem.Cat)
    {
        Console.WriteLine($" - {elemCount.CategoryName} : {elemCount.Count}");
    }
}