namespace BookMan.ConsoleApp.DataServices
{
    using Models;

    public class SimpleDataAccess : IDataAccess
    {
        public List<Book> Books {  get; set; }
        public void Load()
        {
            Books = new List<Book>
            {
                new Book { Id = 1, Title = "A new book" },
                new Book { Id = 2, Title = "A new book" },
                new Book { Id = 3, Title = "A new book" },
                new Book { Id = 4, Title = "A new book" },
                new Book { Id = 5, Title = "A new book" },
                new Book { Id = 6, Title = "A new book" },
                new Book { Id = 7, Title = "A new book" },
                new Book { Id = 8, Title = "A new book" },
                new Book { Id = 9, Title = "A new book" }
            };
        }
        public void SaveChanges() { }
    }
}
