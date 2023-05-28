using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace LibraryManagementSystem
{
        public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool IsCheckedOut { get; set; }

        public void CheckOut()
        {
            IsCheckedOut = true;
        }

        public void Return()
        {
            IsCheckedOut = false;
        }
    }

    public class LibraryManager
    {
        private List<Book> books;

        public LibraryManager()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public List<Book> SearchByTitle(string title)
        {
            return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> SearchByAuthor(string author)
        {
            return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SortBooksByTitle()
        {
            books = books.OrderBy(b => b.Title).ToList();
        }

        public void SortBooksByAuthor()
        {
            books = books.OrderBy(b => b.Author).ToList();
        }

        public void SortBooksByYear()
        {
            books = books.OrderBy(b => b.Year).ToList();
        }

        public bool CheckOutBook(string title)
        {
            Book book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book != null && !book.IsCheckedOut)
            {
                book.CheckOut();
                return true;
            }
            return false;
        }

        public bool ReturnBook(string title)
        {
            Book book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book != null && book.IsCheckedOut)
            {
                book.Return();
                return true;
            }
            return false;
        }

        public bool UpdateBook(string title, string newTitle, string newAuthor, int newYear)
        {
            Book book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book != null)
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                book.Year = newYear;
                return true;
            }
            return false;
        }

        public bool DeleteBook(string title)
        {
            Book book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book != null)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public void ReadBooksFromFile(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var serializer = new XmlSerializer(typeof(List<Book>));
                    books = (List<Book>)serializer.Deserialize(reader);
                }
                Console.WriteLine("Books loaded from file successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading books from file: {ex.Message}");
            }
        }

        public void WriteBooksToFile(string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    var serializer = new XmlSerializer(typeof(List<Book>));
                    serializer.Serialize(writer, books);
                }
                Console.WriteLine("Books written to file successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing books to file: {ex.Message}");
            }
        }
    }
    public class Program
{
    public static void Main()
    {
        LibraryManager libraryManager = new LibraryManager();
        string filePath = "books.xml"; 

        while (true)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Library Management System Menu");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Display all books");
            Console.WriteLine("3. Search books by title");
            Console.WriteLine("4. Search books by author");
            Console.WriteLine("5. Sort books by title");
            Console.WriteLine("6. Sort books by author");
            Console.WriteLine("7. Sort books by year");
            Console.WriteLine("8. Check out a book");
            Console.WriteLine("9. Return a book");
            Console.WriteLine("10. Update a book");
            Console.WriteLine("11. Delete a book");
            Console.WriteLine("12. Read books from file");
            Console.WriteLine("13. Write books to file");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Add a book");
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter book author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter book year: ");
                    int year = Convert.ToInt32(Console.ReadLine());

                    Book newBook = new Book
                    {
                        Title = title,
                        Author = author,
                        Year = year
                    };
                    libraryManager.AddBook(newBook);
                    Console.WriteLine("Book added successfully!");
                    break;

                case 2:
                    Console.WriteLine("Display all books");
                    List<Book> allBooks = libraryManager.GetAllBooks();
                    DisplayBooks(allBooks);
                    break;

                case 3:
                    Console.WriteLine("Search books by title");
                    Console.Write("Enter title to search: ");
                    string searchTitle = Console.ReadLine();
                    List<Book> searchByTitleBooks = libraryManager.SearchByTitle(searchTitle);
                    DisplayBooks(searchByTitleBooks);
                    break;

                case 4:
                    Console.WriteLine("Search books by author");
                    Console.Write("Enter author to search: ");
                    string searchAuthor = Console.ReadLine();
                    List<Book> searchByAuthorBooks = libraryManager.SearchByAuthor(searchAuthor);
                    DisplayBooks(searchByAuthorBooks);
                    break;

                case 5:
                    Console.WriteLine("Sort books by title");
                    libraryManager.SortBooksByTitle();
                    Console.WriteLine("Books sorted by title!");
                    break;

                case 6:
                    Console.WriteLine("Sort books by author");
                    libraryManager.SortBooksByAuthor();
                    Console.WriteLine("Books sorted by author!");
                    break;

                case 7:
                    Console.WriteLine("Sort books by year");
                    libraryManager.SortBooksByYear();
                    Console.WriteLine("Books sorted by year!");
                    break;

                case 8:
                    Console.WriteLine("Check out a book");
                    Console.Write("Enter book title: ");
                    string checkoutTitle = Console.ReadLine();
                    bool checkoutSuccess = libraryManager.CheckOutBook(checkoutTitle);
                    if (checkoutSuccess)
                        Console.WriteLine("The book has been checked out successfully.");
                    else
                        Console.WriteLine("The book is not currently available for checkout.");
                    break;

                case 9:
                    Console.WriteLine("Return a book");
                    Console.Write("Enter book title: ");
                    string returnTitle = Console.ReadLine();
                    bool returnSuccess = libraryManager.ReturnBook(returnTitle);
                    if (returnSuccess)
                        Console.WriteLine("The Books has been returned successfully.");
                        else
                        Console.WriteLine("The book is not currently checked out");
                        break;
                            case 10:
                Console.WriteLine("Update a book");
                Console.Write("Enter book title: ");
                string updateTitle = Console.ReadLine();
                Console.Write("Enter new title: ");
                string newTitle = Console.ReadLine();
                Console.Write("Enter new author: ");
                string newAuthor = Console.ReadLine();
                Console.Write("Enter new year: ");
                int newYear = Convert.ToInt32(Console.ReadLine());
                bool updateSuccess = libraryManager.UpdateBook(updateTitle, newTitle, newAuthor, newYear);
                if (updateSuccess)
                    Console.WriteLine("The book has been updated successfully.");
                else
                    Console.WriteLine("The book does not exist.");
                break;

            case 11:
                Console.WriteLine("Delete a book");
                Console.Write("Enter book title: ");
                string deleteTitle = Console.ReadLine();
                bool deleteSuccess = libraryManager.DeleteBook(deleteTitle);
                if (deleteSuccess)
                    Console.WriteLine("The book has been deleted successfully.");
                else
                    Console.WriteLine("The book does not exist.");
                break;

            case 12:
                Console.WriteLine("Read books from file");
                libraryManager.ReadBooksFromFile(filePath);
                break;
                
            case 13:
                Console.WriteLine("Write books to file");
                libraryManager.WriteBooksToFile(filePath);
                break;

            case 0:
                Console.WriteLine("Exiting the program...");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

private static void DisplayBooks(List<Book> books)
{
    if (books.Count > 0)
    {
        Console.WriteLine("Title\t\tAuthor\t\tYear\t\tStatus");
        Console.WriteLine("----------------------------------------------");
        foreach (var book in books)
        {
            string status = book.IsCheckedOut ? "Checked Out" : "Available";
            Console.WriteLine($"{book.Title}\t\t{book.Author}\t\t{book.Year}\t\t{status}");
        }
    }
    else
    {
        Console.WriteLine("No books found.");
    }
   }
  }
}