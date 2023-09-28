using System;
namespace BookMan.ConsoleApp
{
    using Framework;
    
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var text = Config.Instance.PromptText;
            var color = Config.Instance.PromptColor;
            ConfigRouter();
            while (true)
            {
                ViewHelp.Write(text, color);
                string request = Console.ReadLine();
                try
                {
                    Router.Instance.Forward(request);
                }
                catch (Exception e)
                {
                    ViewHelp.WriteLine(e.Message, ConsoleColor.Red);
                }
                finally
                {
                    Console.WriteLine();
                }
            }
        }
        private static void Help(Parameter parameter)
        {
            if (parameter == null)
            {
                ViewHelp.WriteLine("SUPPORTED COMMANDS:", ConsoleColor.Green);
                ViewHelp.WriteLine(Router.Instance.GetRoutes(), ConsoleColor.Yellow);
                ViewHelp.WriteLine("type: help ? cmd= <command> to get command details", ConsoleColor.Cyan);
                return;
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            var command = parameter["cmd"].ToLower();
            ViewHelp.WriteLine(Router.Instance.GetHelp(command));
        }
        private static void About(Parameter parameter)
        {
            ViewHelp.WriteLine("BOOK MANAGER version 1.0", ConsoleColor.Green);
            ViewHelp.WriteLine("by Mype Nguyen @ ictu.edu.vn", ConsoleColor.Magenta);
        }
    }
}