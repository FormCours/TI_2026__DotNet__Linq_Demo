using Demo_Linq.CustomEnums;

namespace Demo_Linq.Models
{
    // TODO Gestion de la dimension

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Isbn { get; set; }
        public int CategoryId { get; set; }
        public int EditorId { get; set; }
        public BookSize Size { get; set; }

        public Book (int id, string title, string desc, string isbn, int categoryId, int editorId)
        {
            this.Id = id;
            this.Title = title;
            this.Desc = desc;
            this.Isbn = isbn;
            this.CategoryId = categoryId;
            this.EditorId = editorId;
            this.Size = BookSize.Standard;
        }

        public Book(int id, string title, string desc, string isbn, int categoryId, int editorId, BookSize size) : this(id, title, desc, isbn, categoryId, editorId)
        {
            this.Size = size;
        }
    }
}
