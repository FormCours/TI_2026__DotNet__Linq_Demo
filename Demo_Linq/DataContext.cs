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
    }
}
