using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Linq.Models
{
    public record class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public BookAuthor (int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
