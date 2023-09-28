using System;
namespace BookMan.ConsoleApp.Views
{
    using Framework;
    internal class BookCreateView : ViewBase
    {
        public BookCreateView() { }
        public override void Render()
        {
            ViewHelp.WriteLine("CREATE A NEW BOOK", ConsoleColor.Green);
            var title = ViewHelp.InputString("Title");
            var authors = ViewHelp.InputString("Authors");
            var publisher = ViewHelp.InputString("Publisher");
            var year = ViewHelp.InputInt("Year");
            var edition = ViewHelp.InputInt("Edition");
            var tags = ViewHelp.InputString("Tags");
            var description = ViewHelp.InputString("Description");
            var rate = ViewHelp.InputInt("Rate");
            var reading = ViewHelp.InputBool("Reading");
            var file = ViewHelp.InputString("File");
            var request =
                "do create ? " +
                $"title = {title}" +
                $" & authors = {authors}" +
                $" & publisher = {publisher}" +
                $" & year = {year}" +
                $" & edition = {edition}" +
                $" & tags = {tags}" +
                $" & description = {description}" +
                $" & rate = {rate}" +
                $" & reading = {reading}" +
                $" & file = {file}";
            Router.Forward(request);
        }
    }
}