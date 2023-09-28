using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public enum MessageType { Success, Information, Error, Confirmation }
    public class Message
    {
        public MessageType Type { get; set; } = MessageType.Success;
        public string Label { get; set; }
        public string Text { get; set; } = "Your action has completed sccessfully";
        public string BackRoute { get; set; }
    }
    public class MessageView : ViewBase<Message>
    {
        public MessageView(Message model) : base(model) 
        { 
        }
        public override void Render()
        {
            switch (Model.Type)
            {
                case MessageType.Success:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "SUCCESS", ConsoleColor.Green);
                    break;
                case MessageType.Error:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "ERROR", ConsoleColor.Red);
                    break;
                case MessageType.Information:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "SUCCESS", ConsoleColor.Yellow);
                    break;
                case MessageType.Confirmation:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "SUCCESS", ConsoleColor.Cyan);
                    break;
            }
            if (Model.Type != MessageType.Confirmation)
            {
                ViewHelp.WriteLine(Model.Text, ConsoleColor.White);
            }
            else
            {
                ViewHelp.Write(Model.Text, ConsoleColor.Magenta);
                var answer = Console.ReadLine().ToLower();
                if (answer == "y" || answer == "yes") 
                {
                    Router.Forward(Model.BackRoute);
                }
            }
        }
    }
}
