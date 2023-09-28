namespace BookMan.ConsoleApp.Controllers
{
    using DataServices;
    using Models;
    using Views;
    using Framework;
    using System.Diagnostics;

    internal class ShellController : ControllerBase
    {
        protected Repository Repository;
        public ShellController(IDataAccess context)
        {
            Repository = new Repository(context);
        }
        public void Shell(string folder, string ext = "*.pdf")
        {
            if (!Directory.Exists(folder)) 
            {
                Error("Folder not found!");
                return;
            }
            var files = Directory.GetFiles(folder, ext ?? "*.pdf", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                Success($"{files.Length} item(s) found");
                foreach (var f in files)
                {
                    Repository.Insert(new Book { Title = Path.GetFileNameWithoutExtension(f), File = f });
                }
                return;
            }
            Inform("No item found!", "Sorry!");
        }
        public void Read(int id)
        {
            var book = Repository.Select(id);
            if (book == null)
            {
                Error("Book not found!");
                return;
            }
            if (!File.Exists(book.File))
            {
                Error("File not found!");
                return;
            }
            var psi = new ProcessStartInfo(book.File)
            {
                UseShellExecute = true
            };
            Process.Start(psi);
            Success($"You are reading the book '{book.Title}'");
        }
        public void Clear(bool process = false)
        {
            if (!process)
            {
                Confirm("Do you really want to clear the shell? ", "do clear");
                return;
            }
            Repository.Clear();
            Inform("The shell has been cleared");
        }
        public void Save()
        {
            Repository.SaveChanges();
            Success("Data save!");
        }
    }
}
