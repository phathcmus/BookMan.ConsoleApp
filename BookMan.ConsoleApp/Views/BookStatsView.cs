using BookMan.ConsoleApp.Models;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BookMan.ConsoleApp.Views
{
    internal class BookStatsView : ViewBase<IEnumerable<IGrouping<string, Book>>>
    {
        public BookStatsView(IEnumerable<IGrouping<string, Book>> model) : base(model)
        {
        }
        public override void Render()
        {
            foreach (var g in Model)
            {
                ViewHelp.WriteLine($"# {g.Key}", ConsoleColor.Magenta);
                foreach (var b in g)
                {
                    ViewHelp.Write($"[{b.Id}] ", ConsoleColor.Yellow);
                    ViewHelp.WriteLine(b.Title, b.Reading ? ConsoleColor.Cyan : ConsoleColor.White);
                }
            }
        }
    }
}