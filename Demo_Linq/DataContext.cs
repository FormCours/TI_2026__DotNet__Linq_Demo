using Demo_Linq.CustomEnums;
using Demo_Linq.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Linq
{
    internal sealed class DataContext
    {
        private List<BookAuthor> bookAuthors = [];
        private List<Shelf> shelves = [];
        private List<Book> books = [];
        private List<Author> authors = [];
        private List<Category> categories = [];
        private List<Editor> editors = [];

        public IEnumerable<BookAuthor> BookAuthors { get { return bookAuthors.AsReadOnly(); } }
        public IEnumerable<Shelf> Shelves { get { return shelves.AsReadOnly(); } }
        public IEnumerable<Book> Books { get { return books.AsReadOnly(); } }
        public IEnumerable<Author> Authors { get { return authors.AsReadOnly(); } }
        public IEnumerable<Category> Categories { get { return categories.AsReadOnly(); } }
        public IEnumerable<Editor> Editors { get { return editors.AsReadOnly(); } }

        public DataContext()
        {
            SeedCategories();
            SeedShelves();
            SeedEditors();
            SeedAuthors();
            SeedBooks();
            SeedBookAuthors();
        }

        /// <summary>
        /// Les categories reprennent les classes principales de la CDU.
        /// L'Id correspond au premier chiffre du code d'une etagere (Shelf.CDU).
        /// La classe 4 est vacante dans la CDU : c'est pour cela que Shelf.Code la refuse.
        /// </summary>
        private void SeedCategories()
        {
            categories.AddRange(
            [
                new Category(0, "Generalites", "Informatique, encyclopedies, journalisme"),
                new Category(1, "Philosophie et psychologie", "Pensee, morale, psychanalyse"),
                new Category(2, "Religion et mythologie", "Religions, mythes, esoterisme"),
                new Category(3, "Sciences sociales", "Sociologie, politique, economie, education"),
                new Category(5, "Sciences pures", "Mathematiques, astronomie, physique, biologie"),
                new Category(6, "Sciences appliquees", "Medecine, technique, cuisine, jardinage"),
                new Category(7, "Arts, sports et loisirs", "BD, manga, peinture, musique, cinema"),
                new Category(8, "Litterature", "Romans, poesie, theatre, policier, SF et fantasy"),
                new Category(9, "Geographie et histoire", "Voyages, biographies, histoire"),
            ]);
        }

        /// <summary>
        /// Code = 3 chiffres : le 1er est la categorie principale (CDU),
        /// les 2 suivants precisent le rayon. Le 1er chiffre ne peut jamais valoir 4.
        /// </summary>
        private void SeedShelves()
        {
            shelves.AddRange(
            [
                // 0xx - Generalites
                new Shelf(1, "001", 13, "Pourpre"),      // Curiosites et ouvrages a acces restreint
                new Shelf(2, "004", 40, "Bleu"),         // Informatique
                new Shelf(3, "030", 25, "Gris"),         // Encyclopedies et dictionnaires
                new Shelf(4, "070", 20, "Gris"),         // Journalisme et medias
                // 1xx - Philosophie et psychologie
                new Shelf(5, "100", 30, "Vert"),         // Philosophie generale
                new Shelf(6, "150", 30, "Vert"),         // Psychologie
                new Shelf(7, "170", 25, "Vert"),         // Morale et ethique
                // 2xx - Religion et mythologie
                new Shelf(8, "200", 25, "Violet"),       // Religions
                new Shelf(9, "290", 20, "Violet"),       // Mythologies et esoterisme
                // 3xx - Sciences sociales
                new Shelf(10, "300", 30, "Orange"),      // Sociologie
                new Shelf(11, "320", 25, "Orange"),      // Sciences politiques
                new Shelf(12, "330", 25, "Orange"),      // Economie
                new Shelf(13, "370", 20, "Orange"),      // Education
                // 5xx - Sciences pures
                new Shelf(14, "500", 35, "Jaune"),       // Sciences generales
                new Shelf(15, "510", 30, "Jaune"),       // Mathematiques
                new Shelf(16, "520", 30, "Jaune"),       // Astronomie
                new Shelf(17, "530", 30, "Jaune"),       // Physique
                new Shelf(18, "570", 30, "Jaune"),       // Biologie
                // 6xx - Sciences appliquees
                new Shelf(19, "610", 30, "Turquoise"),   // Medecine
                new Shelf(20, "620", 25, "Turquoise"),   // Ingenierie
                new Shelf(21, "630", 20, "Turquoise"),   // Agriculture et jardinage
                new Shelf(22, "640", 25, "Turquoise"),   // Cuisine et vie domestique
                // 7xx - Arts, sports et loisirs
                new Shelf(23, "700", 30, "Rouge"),       // Arts generalites
                new Shelf(24, "741", 60, "Rouge"),       // Bande dessinee
                new Shelf(25, "742", 50, "Rouge"),       // Manga
                new Shelf(26, "750", 20, "Rouge"),       // Peinture
                new Shelf(27, "780", 25, "Rouge"),       // Musique
                new Shelf(28, "791", 25, "Rouge"),       // Cinema
                new Shelf(29, "796", 20, "Rouge"),       // Sports et loisirs
                // 8xx - Litterature
                new Shelf(30, "801", 20, "Blanc"),       // Poesie
                new Shelf(31, "820", 45, "Blanc"),       // Litterature anglophone
                new Shelf(32, "833", 50, "Blanc"),       // Science-fiction et fantasy
                new Shelf(33, "840", 55, "Blanc"),       // Litterature francaise
                new Shelf(34, "843", 40, "Noir"),        // Roman policier
                new Shelf(35, "895", 25, "Blanc"),       // Litteratures d'Asie
                // 9xx - Geographie et histoire
                new Shelf(36, "910", 25, "Brun"),        // Geographie et voyages
                new Shelf(37, "920", 25, "Brun"),        // Biographies
                new Shelf(38, "930", 20, "Brun"),        // Histoire ancienne
                new Shelf(39, "940", 30, "Brun"),        // Histoire de l'Europe
                new Shelf(40, "949", 15, "Brun"),        // Histoire de la Belgique
            ]);
        }

        private void SeedEditors()
        {
            editors.AddRange(
            [
                new Editor(1, "Gallimard", "Maison generaliste, collection Folio", "France", true),
                new Editor(2, "Flammarion", "Litterature et vulgarisation scientifique", "France", true),
                new Editor(3, "Actes Sud", "Litterature et sciences humaines", "France", true),
                new Editor(4, "Casterman", "Historiquement l'editeur de Tintin", "Belgique", true),
                new Editor(5, "Dupuis", "Le journal de Spirou", "Belgique", true),
                new Editor(6, "Le Lombard", "Bande dessinee franco-belge", "Belgique", true),
                new Editor(7, "Penguin Books", "Classiques de langue anglaise", "Royaume-Uni", true),
                new Editor(8, "Bloomsbury", "Fiction contemporaine", "Royaume-Uni", true),
                new Editor(9, "O'Reilly Media", "Ouvrages techniques et informatiques", "Etats-Unis", true),
                new Editor(10, "Glenat", "Manga et bande dessinee", "France", true),
                new Editor(11, "Kana", "Specialiste du manga", "France", true),
                new Editor(12, "Marabout", "Guides pratiques, fonds historique", "Belgique", false),
                new Editor(13, "Presses de la Cite", "Roman policier et litterature populaire", "France", true),
                new Editor(14, "Hachette", "Dictionnaires, scolaire et grand public", "France", true),
                new Editor(15, "Les Humanoides Associes", "Science-fiction graphique", "France", false),
                new Editor(16, "Albin Michel", "Litterature et essais", "France", true),
                new Editor(17, "Editions du Seuil", "Sciences humaines et vulgarisation", "France", true),
                new Editor(18, "Addison-Wesley", "Reference en genie logiciel", "Etats-Unis", false),
                new Editor(19, "Editions de Minuit", "Avant-garde litteraire", "France", true),
                new Editor(20, "Miskatonic University Press", "Tirages confidentiels, reserve universitaire", "Etats-Unis", false),
            ]);
        }

        private void SeedAuthors()
        {
            authors.AddRange(
            [
                new Author(1, "Jules", "Verne", new DateTime(1828, 2, 8), new DateTime(1905, 3, 24)),
                new Author(2, "Victor", "Hugo", new DateTime(1802, 2, 26), new DateTime(1885, 5, 22)),
                new Author(3, "Agatha", "Christie", new DateTime(1890, 9, 15), new DateTime(1976, 1, 12)),
                new Author(4, "John Ronald Reuel", "Tolkien", new DateTime(1892, 1, 3), new DateTime(1973, 9, 2)),
                new Author(5, "Terry", "Pratchett", new DateTime(1948, 4, 28), new DateTime(2015, 3, 12)),
                new Author(6, "Neil", "Gaiman", new DateTime(1960, 11, 10), null),
                new Author(7, "Georges", "Remi", new DateTime(1907, 5, 22), new DateTime(1983, 3, 3)),
                new Author(8, "Rene", "Goscinny", new DateTime(1926, 8, 14), new DateTime(1977, 11, 5)),
                new Author(9, "Albert", "Uderzo", new DateTime(1927, 4, 25), new DateTime(2020, 3, 24)),
                new Author(10, "Maurice", "De Bevere", new DateTime(1923, 12, 1), new DateTime(2001, 7, 16)),
                new Author(11, "Andre", "Franquin", new DateTime(1924, 1, 3), new DateTime(1997, 1, 5)),
                new Author(12, "Pierre", "Culliford", new DateTime(1928, 6, 25), new DateTime(1992, 12, 24)),
                new Author(13, "Yvan", "Delporte", new DateTime(1928, 5, 23), new DateTime(2007, 3, 8)),
                new Author(14, "Alejandro", "Jodorowsky", new DateTime(1929, 2, 17), null),
                new Author(15, "Jean", "Giraud", new DateTime(1938, 5, 8), new DateTime(2012, 3, 10)),
                new Author(16, "Eiichiro", "Oda", new DateTime(1975, 1, 1), null),
                new Author(17, "Naoki", "Urasawa", new DateTime(1960, 1, 2), null),
                new Author(18, "Stephen", "King", new DateTime(1947, 9, 21), null),
                new Author(19, "Howard Phillips", "Lovecraft", new DateTime(1890, 8, 20), new DateTime(1937, 3, 15)),
                new Author(20, "Abdul", "Alhazred", new DateTime(700, 1, 1), new DateTime(738, 7, 13)),
                new Author(21, "Antoine", "de Saint-Exupery", new DateTime(1900, 6, 29), new DateTime(1944, 7, 31)),
                new Author(22, "Arthur", "Conan Doyle", new DateTime(1859, 5, 22), new DateTime(1930, 7, 7)),
                new Author(23, "Georges", "Simenon", new DateTime(1903, 2, 13), new DateTime(1989, 9, 4)),
                new Author(24, "Amelie", "Nothomb", new DateTime(1966, 8, 13), null),
                new Author(25, "Marguerite", "Yourcenar", new DateTime(1903, 6, 8), new DateTime(1987, 12, 17)),
                new Author(26, "Simone", "de Beauvoir", new DateTime(1908, 1, 9), new DateTime(1986, 4, 14)),
                new Author(27, "Albert", "Camus", new DateTime(1913, 11, 7), new DateTime(1960, 1, 4)),
                new Author(28, "Michel", "de Montaigne", new DateTime(1533, 2, 28), new DateTime(1592, 9, 13)),
                new Author(29, "Marc", "Aurele", new DateTime(121, 4, 26), new DateTime(180, 3, 17)),
                new Author(30, "Michel", "Foucault", new DateTime(1926, 10, 15), new DateTime(1984, 6, 25)),
                new Author(31, "Yuval Noah", "Harari", new DateTime(1976, 2, 24), null),
                new Author(32, "Stephen", "Hawking", new DateTime(1942, 1, 8), new DateTime(2018, 3, 14)),
                new Author(33, "Leonard", "Mlodinow", new DateTime(1954, 11, 26), null),
                new Author(34, "Carl", "Sagan", new DateTime(1934, 11, 9), new DateTime(1996, 12, 20)),
                new Author(35, "Ann", "Druyan", new DateTime(1949, 6, 13), null),
                new Author(36, "Hubert", "Reeves", new DateTime(1932, 7, 13), new DateTime(2023, 10, 13)),
                new Author(37, "Murielle", "Szac", new DateTime(1964, 3, 2), null),
                new Author(38, "Jean", "Chevalier", new DateTime(1906, 4, 11), new DateTime(1993, 2, 25)),
                new Author(39, "Erich", "Gamma", new DateTime(1961, 3, 13), null),
                new Author(40, "Richard", "Helm", new DateTime(1958, 5, 2), null),
                new Author(41, "Ralph", "Johnson", new DateTime(1955, 10, 7), null),
                new Author(42, "John", "Vlissides", new DateTime(1961, 8, 2), new DateTime(2005, 11, 24)),
                new Author(43, "Robert Cecil", "Martin", new DateTime(1952, 12, 5), null),
                new Author(44, "Andrew", "Hunt", new DateTime(1964, 3, 9), null),
                new Author(45, "David", "Thomas", new DateTime(1956, 6, 20), null),
                new Author(46, "Mark John", "Price", new DateTime(1970, 5, 15), null),
                new Author(47, "Martin", "Fowler", new DateTime(1963, 12, 18), null),
                new Author(48, "Frederick", "Brooks", new DateTime(1931, 4, 19), new DateTime(2022, 11, 17)),
                new Author(49, "Stuart", "Russell", new DateTime(1962, 1, 1), null),
                new Author(50, "Peter", "Norvig", new DateTime(1956, 12, 14), null),
                new Author(51, "Bruce", "Chatwin", new DateTime(1940, 5, 13), new DateTime(1989, 1, 18)),
                new Author(52, "Jared", "Diamond", new DateTime(1937, 9, 10), null),
                new Author(53, "Henri", "Pirenne", new DateTime(1862, 12, 23), new DateTime(1935, 10, 25)),
                new Author(54, "Ada", "Lovelace", new DateTime(1815, 12, 10), new DateTime(1852, 11, 27)),
                new Author(55, "Alan", "Turing", new DateTime(1912, 6, 23), new DateTime(1954, 6, 7)),
            ]);
        }

        private void SeedBooks()
        {
            books.AddRange(
            [
                // ---------- 0 : Generalites ----------
                new Book(1, "Design Patterns", "Les 23 modeles de conception du Gang of Four", "978-0-201-73434-8", 0, 18, BookSize.PaperBack),
                new Book(2, "Clean Code", "Manuel d'artisanat logiciel et de lisibilite du code", "978-0-201-46855-7", 0, 18, BookSize.PaperBack),
                new Book(3, "The Pragmatic Programmer", "Du compagnon a la maitrise du metier de developpeur", "978-0-201-20276-2", 0, 18, BookSize.PaperBack),
                new Book(4, "C# 12 et .NET 8", "Developpement moderne multiplateforme, LINQ inclus", "978-1-492-93697-8", 0, 9, BookSize.PaperBack),
                new Book(5, "Refactoring", "Ameliorer la conception du code existant sans le casser", "978-0-201-67118-6", 0, 18, BookSize.HardCover),
                new Book(6, "Le Petit Larousse illustre 2026", "Dictionnaire encyclopedique, ouvrage collectif sans auteur attribue", "978-2-01-440539-2", 0, 14, BookSize.HardCover),

                // ---------- 1 : Philosophie et psychologie ----------
                new Book(7, "Les Essais", "Que sais-je ? Trois livres de sagesse sceptique", "978-2-07-513960-1", 1, 1, BookSize.Pocket),
                new Book(8, "Le Mythe de Sisyphe", "Essai sur l'absurde et le suicide philosophique", "978-2-07-587381-9", 1, 1, BookSize.Pocket),
                new Book(9, "Le Deuxieme Sexe", "On ne nait pas femme, on le devient", "978-2-07-660802-1", 1, 1),
                new Book(10, "Pensees pour moi-meme", "Carnets stoiciens d'un empereur romain en campagne", "978-2-08-734223-5", 1, 2, BookSize.Pocket),

                // ---------- 2 : Religion et mythologie ----------
                new Book(11, "Les Mythes nordiques", "Odin, Thor et Loki racontes du commencement au Ragnarok", "978-2-07-807644-6", 2, 1),
                new Book(12, "Le Feuilleton d'Hermes", "La mythologie grecque en cent episodes", "978-2-330-81065-8", 2, 3),
                new Book(13, "Dictionnaire des symboles", "Mythes, reves, coutumes, gestes, formes et couleurs", "978-2-08-954486-6", 2, 2, BookSize.HardCover),
                new Book(14, "Al Azif, le Necronomicon", "Traite des Grands Anciens. Exemplaire incomplet, pages 141 a 157 manquantes", "978-0-666-27907-1", 2, 20, BookSize.HardCover),
                new Book(15, "Le Necronomicon annote", "Edition critique etablie huit siecles apres la mort de son auteur", "978-0-666-01328-6", 2, 20, BookSize.HardCover),

                // ---------- 3 : Sciences sociales ----------
                new Book(16, "Sapiens", "Une breve histoire de l'humanite", "978-2-226-74749-5", 3, 16),
                new Book(17, "Homo Deus", "Une breve histoire de l'avenir", "978-2-226-48170-2", 3, 16),
                new Book(18, "Surveiller et punir", "Naissance de la prison et des societes disciplinaires", "978-2-07-321591-8", 3, 1, BookSize.Pocket),

                // ---------- 5 : Sciences pures ----------
                new Book(19, "Une breve histoire du temps", "Du big bang aux trous noirs", "978-2-08-395012-0", 5, 2),
                new Book(20, "Y a-t-il un grand architecte dans l'Univers ?", "La theorie M et la question des origines", "978-2-226-68433-2", 5, 16, BookSize.HardCover),
                new Book(21, "Cosmos", "Quinze milliards d'annees d'evolution cosmique", "978-2-08-541854-3", 5, 2, BookSize.HardCover),
                new Book(22, "Poussieres d'etoiles", "L'astrophysique racontee avec poesie", "978-2-02-615275-8", 5, 17, BookSize.Pocket),
                new Book(23, "Science & Vie, hors-serie : les trous noirs", "Numero special, redaction collective", "978-2-01-688696-0", 5, 14, BookSize.Magazines),

                // ---------- 6 : Sciences appliquees ----------
                new Book(24, "Le Mythe du mois-homme", "Ajouter des developpeurs a un projet en retard le retarde davantage", "978-0-201-62117-4", 6, 18, BookSize.PaperBack),
                new Book(25, "Intelligence artificielle : une approche moderne", "Le manuel de reference des agents intelligents", "978-1-492-35538-0", 6, 9, BookSize.HardCover),
                new Book(26, "Petit manuel du jardinier debutant", "Guide pratique collectif, premiere semis a premiere recolte", "978-2-501-08959-3", 6, 12, BookSize.PaperBack),

                // ---------- 7 : Arts, sports et loisirs ----------
                new Book(27, "Tintin au Tibet", "Tchang est vivant, Tintin part le chercher dans l'Himalaya", "978-2-203-82380-8", 7, 4, BookSize.ComicBook),
                new Book(28, "Les Bijoux de la Castafiore", "Une enquete immobile a Moulinsart", "978-2-203-55801-4", 7, 4, BookSize.ComicBook),
                new Book(29, "L'Etoile mysterieuse", "Course a l'aerolithe entre deux expeditions rivales", "978-2-203-29222-2", 7, 4, BookSize.ComicBook),
                new Book(30, "Asterix le Gaulois", "Le village qui resiste encore et toujours a l'envahisseur", "978-2-01-202643-8", 7, 14, BookSize.ComicBook),
                new Book(31, "Asterix chez les Bretons", "De l'eau chaude, un nuage de lait et beaucoup de courage", "978-2-01-276064-6", 7, 14, BookSize.ComicBook),
                new Book(32, "Le Tour de Gaule d'Asterix", "Un tour gastronomique de la Gaule occupee", "978-2-01-349485-4", 7, 14, BookSize.ComicBook),
                new Book(33, "Lucky Luke : le Pied-tendre", "Un gentleman anglais herite d'un ranch au Far West", "978-2-800-22906-5", 7, 5, BookSize.ComicBook),
                new Book(34, "Gaston Lagaffe, tome 10", "Gaffes, bricoles et contrats jamais signes", "978-2-800-96327-3", 7, 5, BookSize.ComicBook),
                new Book(35, "Les Schtroumpfs noirs", "Une mouche, une piqure et toute une epidemie", "978-2-800-69748-2", 7, 5, BookSize.ComicBook),
                new Book(36, "L'Incal", "Space opera metaphysique en six albums", "978-2-7316-3169-2", 7, 15, BookSize.ComicBook),
                new Book(37, "One Piece, tome 1", "A l'aube d'une grande aventure", "978-2-344-16590-4", 7, 10, BookSize.Manga),
                new Book(38, "Monster, tome 1", "Un chirurgien sauve un enfant qui deviendra un monstre", "978-2-505-90011-5", 7, 11, BookSize.Manga),

                // ---------- 8 : Litterature ----------
                new Book(39, "Vingt mille lieues sous les mers", "Le capitaine Nemo et le Nautilus", "978-2-01-863432-3", 8, 14),
                new Book(40, "Le Tour du monde en quatre-vingts jours", "Un pari de vingt mille livres au Reform Club", "978-2-01-936853-1", 8, 14, BookSize.Pocket),
                new Book(41, "Voyage au centre de la Terre", "Descends dans le cratere du Sneffels, voyageur audacieux", "978-2-01-010274-5", 8, 14, BookSize.Pocket),
                new Book(42, "De la Terre a la Lune", "Un obus habite tire par un canon de neuf cents pieds", "978-2-01-083695-4", 8, 14, BookSize.Pocket),
                new Book(43, "Michel Strogoff", "Courrier du tsar de Moscou a Irkoutsk", "978-2-01-157116-8", 8, 14),
                new Book(44, "Les Miserables", "Jean Valjean, Cosette et la barricade de la rue de la Chanvrerie", "978-2-07-230537-5", 8, 1, BookSize.HardCover),
                new Book(45, "Notre-Dame de Paris", "Quasimodo, Esmeralda et la cathedrale comme personnage principal", "978-2-07-303958-3", 8, 1),
                new Book(46, "Les Contemplations", "Recueil en deux parties, avant et apres la mort de Leopoldine", "978-2-07-377379-1", 8, 1, BookSize.Pocket),
                new Book(47, "Le Crime de l'Orient-Express", "Douze suspects immobilises par la neige", "978-2-258-50800-2", 8, 13, BookSize.Pocket),
                new Book(48, "Le Meurtre de Roger Ackroyd", "Le roman qui a brise les regles du genre policier", "978-2-258-24221-0", 8, 13, BookSize.Pocket),
                new Book(49, "Mort sur le Nil", "Une croisiere, un triangle amoureux, trois cadavres", "978-2-258-97642-9", 8, 13, BookSize.Pocket),
                new Book(50, "Ils etaient dix", "Dix invites sur une ile, aucun survivant prevu", "978-2-258-71063-4", 8, 13, BookSize.Pocket),
                new Book(51, "Le Seigneur des Anneaux", "La Communaute, les Deux Tours et le Retour du roi en un volume", "978-0-14-744484-4", 8, 7, BookSize.HardCover),
                new Book(52, "Bilbo le Hobbit", "Un voyage imprevu jusqu'a la Montagne Solitaire", "978-0-14-817905-9", 8, 7),
                new Book(53, "Le Silmarillion", "Les mythes fondateurs d'Arda, publies a titre posthume", "978-0-14-891326-4", 8, 7),
                new Book(54, "La Huitieme Couleur", "Le premier tome des Annales du Disque-monde", "978-1-4088-4747-3", 8, 8, BookSize.Pocket),
                new Book(55, "De bons presages", "Un ange et un demon sabotent l'Apocalypse, ecrit a quatre mains", "978-1-4088-8168-2", 8, 8, BookSize.Pocket),
                new Book(56, "American Gods", "Les anciens dieux affrontent les nouveaux sur les routes des Etats-Unis", "978-1-4088-1589-2", 8, 8),
                new Book(57, "Shining", "L'hotel Overlook ne laisse jamais repartir ses gardiens", "978-2-226-85010-2", 8, 16, BookSize.Pocket),
                new Book(58, "Ca", "Sept enfants de Derry et ce qui revient tous les vingt-sept ans", "978-2-226-58431-1", 8, 16, BookSize.HardCover),
                new Book(59, "L'Appel de Cthulhu", "La chose la plus misericordieuse au monde est l'incapacite de l'esprit humain a correler ses contenus", "978-2-07-331852-7", 8, 1, BookSize.Pocket),
                new Book(60, "Le Petit Prince", "On ne voit bien qu'avec le coeur", "978-2-07-405273-4", 8, 1),
                new Book(61, "Une etude en rouge", "Premiere rencontre entre Sherlock Holmes et le docteur Watson", "978-0-14-478694-7", 8, 7, BookSize.Pocket),
                new Book(62, "Le Chien des Baskerville", "Une legende familiale et une bete sur la lande de Dartmoor", "978-0-14-552115-8", 8, 7, BookSize.Pocket),
                new Book(63, "Maigret tend un piege", "Le commissaire appate un tueur dans le Marais", "978-2-258-25536-4", 8, 13, BookSize.Pocket),
                new Book(64, "Maigret et le corps sans tete", "Un cadavre demembre remonte du canal Saint-Martin", "978-2-258-98957-3", 8, 13, BookSize.Pocket),
                new Book(65, "Stupeur et tremblements", "Une annee d'humiliation hierarchique dans une entreprise japonaise", "978-2-226-72378-9", 8, 16),
                new Book(66, "Hygiene de l'assassin", "Huis clos entre un prix Nobel mourant et cinq journalistes", "978-2-226-45799-8", 8, 16, BookSize.Pocket),
                new Book(67, "Memoires d'Hadrien", "Lettre d'un empereur vieillissant au jeune Marc Aurele", "978-2-07-919220-6", 8, 1),
                new Book(68, "La Chanson de Roland", "Chanson de geste du XIe siecle, auteur inconnu", "978-2-08-992641-9", 8, 2, BookSize.Pocket),

                // ---------- 9 : Geographie et histoire ----------
                new Book(69, "Atlas mondial illustre", "Cartographie politique et physique, edition collective", "978-2-01-066062-7", 9, 14, BookSize.HardCover),
                new Book(70, "En Patagonie", "Recit de voyage au bout du continent", "978-2-07-139483-7", 9, 1, BookSize.Pocket),
                new Book(71, "De l'inegalite parmi les societes", "Pourquoi certaines civilisations ont pris l'avantage", "978-2-07-212904-9", 9, 1),
                new Book(72, "Histoire de Belgique", "La synthese fondatrice de l'historiographie belge", "978-2-501-86325-4", 9, 12),
            ]);
        }

        /// <summary>
        /// Table de liaison N-N. Volontairement incomplete :
        /// les livres 6, 23, 26, 68 et 69 n'ont aucun auteur (ouvrages collectifs ou anonymes),
        /// et les auteurs 54 et 55 n'ont aucun livre.
        /// </summary>
        private void SeedBookAuthors()
        {
            bookAuthors.AddRange(
            [
                new BookAuthor(1, 39),
                new BookAuthor(1, 40),
                new BookAuthor(1, 41),
                new BookAuthor(1, 42),
                new BookAuthor(2, 43),
                new BookAuthor(3, 44),
                new BookAuthor(3, 45), 
                new BookAuthor(4, 46),
                new BookAuthor(5, 47),
                new BookAuthor(7, 28),
                new BookAuthor(8, 27),
                new BookAuthor(9, 26),
                new BookAuthor(10, 29),
                new BookAuthor(11, 6),
                new BookAuthor(12, 37),
                new BookAuthor(13, 38),
                new BookAuthor(14, 20),
                new BookAuthor(15, 20),
                new BookAuthor(15, 19), 
                new BookAuthor(16, 31), 
                new BookAuthor(17, 31),
                new BookAuthor(18, 30),
                new BookAuthor(19, 32),
                new BookAuthor(20, 32),
                new BookAuthor(20, 33),
                new BookAuthor(21, 34),
                new BookAuthor(21, 35), 
                new BookAuthor(22, 36),
                new BookAuthor(24, 48),
                new BookAuthor(25, 49),
                new BookAuthor(25, 50),
                new BookAuthor(27, 7),
                new BookAuthor(28, 7),
                new BookAuthor(29, 7),
                new BookAuthor(30, 8),
                new BookAuthor(30, 9),  
                new BookAuthor(31, 8),
                new BookAuthor(31, 9),
                new BookAuthor(32, 8),
                new BookAuthor(32, 9),
                new BookAuthor(33, 8),
                new BookAuthor(33, 10),  
                new BookAuthor(34, 11),
                new BookAuthor(35, 12),
                new BookAuthor(35, 13), 
                new BookAuthor(36, 14),
                new BookAuthor(36, 15),
                new BookAuthor(37, 16),
                new BookAuthor(38, 17),
                new BookAuthor(39, 1),
                new BookAuthor(40, 1),
                new BookAuthor(41, 1),
                new BookAuthor(42, 1),
                new BookAuthor(43, 1),
                new BookAuthor(44, 2),
                new BookAuthor(45, 2),
                new BookAuthor(46, 2),
                new BookAuthor(47, 3),
                new BookAuthor(48, 3),
                new BookAuthor(49, 3),
                new BookAuthor(50, 3),
                new BookAuthor(51, 4),
                new BookAuthor(52, 4),
                new BookAuthor(53, 4),
                new BookAuthor(54, 5),
                new BookAuthor(55, 5),
                new BookAuthor(55, 6),
                new BookAuthor(56, 6),
                new BookAuthor(57, 18),
                new BookAuthor(58, 18),
                new BookAuthor(59, 19),
                new BookAuthor(60, 21),
                new BookAuthor(61, 22),
                new BookAuthor(62, 22),
                new BookAuthor(63, 23),
                new BookAuthor(64, 23),
                new BookAuthor(65, 24),
                new BookAuthor(66, 24),
                new BookAuthor(67, 25),
                new BookAuthor(70, 51),
                new BookAuthor(71, 52),
                new BookAuthor(72, 53),
            ]);
        }
    }
}
