using System;
namespace BookMan.ConsoleApp.Views
{
    using Framework;
    using Models;
    internal class BookSingleView : ViewBase<Book>
    {
        public BookSingleView(Book model) : base(model) { }
        public override void Render()
        {
            if (Model == null)
            {
                ViewHelp.WriteLine("NO BOOK FOUND. SORRY!", ConsoleColor.Red);
                return;
            }
            ViewHelp.WriteLine("BOOK DETAIL INFORMATION", ConsoleColor.Green);
            Console.WriteLine($"Authors:     {Model.Authors}");
            Console.WriteLine($"Title:       {Model.Title}");
            Console.WriteLine($"Publisher:   {Model.Publisher}");
            Console.WriteLine($"Year:        {Model.Year}");
            Console.WriteLine($"Edition:     {Model.Edition}");
            Console.WriteLine($"Isbn:        {Model.Isbn}");
            Console.WriteLine($"Tags:        {Model.Tags}");
            Console.WriteLine($"Description: {Model.Description}");
            Console.WriteLine($"Rating:      {Model.Rating}");
            Console.WriteLine($"Reading:     {Model.Reading}");
            Console.WriteLine($"File:        {Model.File}");
            Console.WriteLine($"File Name:   {Model.FileName}");
        }
    }
}