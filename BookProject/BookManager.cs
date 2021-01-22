using BookProject.Models;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

namespace BookProject
{
    public class BookManager
    {
        public List<BookViewModel> ListBooks()
        {
            string booksJson = Repository.Read();
            if(!string.IsNullOrEmpty(booksJson))
                return JsonConvert.DeserializeObject<List<BookViewModel>>(booksJson);
            return new List<BookViewModel>(); 
        }
        public BookViewModel GetBookDetails(int bookId) => ListBooks().FirstOrDefault(b => b.Id == bookId);

        public bool AddBook(BookViewModel bookViewModel)
        {
            List<BookViewModel> books = ListBooks();
            int lastIndex = books.OrderBy(b => b.Id).LastOrDefault()?.Id??0;
            bookViewModel.Id = ++lastIndex; 
            books.Add(bookViewModel);
            return Repository.Write(JsonConvert.SerializeObject(books));
        }
        public bool UpdateBook(BookViewModel bookViewModel)
        {
            List<BookViewModel> books = ListBooks();
            BookViewModel editedBook = books.FirstOrDefault(b => b.Id == bookViewModel.Id);
            editedBook.Author = bookViewModel.Author;
            editedBook.Title = bookViewModel.Title;
            editedBook.Description = bookViewModel.Description;
            return Repository.Write(JsonConvert.SerializeObject(books));
        }
        public bool RemoveBook(int bookId)
        {
            List<BookViewModel> books = ListBooks();
            BookViewModel deletedBook = books.FirstOrDefault(b => b.Id == bookId);
            books.Remove(deletedBook);
            return Repository.Write(JsonConvert.SerializeObject(books));
        }
        public List<BookViewModel> Find(string searchKeyword)
        {
            List<BookViewModel> books = ListBooks();
            searchKeyword = searchKeyword.ToLower();
            return books.Where(b => b.Title.ToLower().Contains(searchKeyword)
            || b.Author.ToLower().Contains(searchKeyword)
            || b.Description.ToLower().Contains(searchKeyword)).ToList();
        }
    }
}
