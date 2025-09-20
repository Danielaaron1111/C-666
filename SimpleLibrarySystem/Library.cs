using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrarySystem
{
    internal class Library
    {
        private List<Book> _books = new List<Book>();
        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }
            _books.Add(book);
        }
        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books); // return a copy to prevent //modification
            
        }

        public Book FindBookByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null, empty, or whitespace.");
            }

            string cleanTitle = title.Trim().ToLower();

            foreach (var book in _books)
            {
                if (book.Title != null && book.Title.Trim().ToLower() == cleanTitle)
                {
                    return book; // found it 
                }
            }
            return null;
        }




        //public Book FindBookByTitle(string title)
        //{
        //    // Check if title is null or empty
        //    if (string.IsNullOrWhiteSpace(title))
        //    {
        //        throw new ArgumentException("Title cannot be null, empty, or whitespace.");
        //    }

        //    // Clean the input title
        //    string cleanTitle = title.Trim().ToLower();

        //    // Use LINQ to find the first book with matching title
        //    return _books.FirstOrDefault(book => book.Title.ToLower() == cleanTitle);
        //}



















    }
}
