using System;
namespace BookMan.ConsoleApp.Views
{
    using Framework;
    using Models;
    internal class BookListView : ViewBase<Book[]>
    {
        public BookListView(Book[] model) : base(model) { }
        public override void Render()
        {
            if (Model.Length == 0)
            {
                ViewHelp.WriteLine("No book found!", ConsoleColor.Yellow);
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("THE BOOK LIST");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Book b in Model)
            {
                ViewHelp.Write($"[{b.Id}]", ConsoleColor.Yellow);
                ViewHelp.WriteLine($" {b.Title}", b.Reading ? ConsoleColor.Cyan : ConsoleColor.White);
            }
            ViewHelp.WriteLine($"{Model.Length} item(s)", ConsoleColor.Green);
        }
    }
}