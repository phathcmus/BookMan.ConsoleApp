using BookMan.ConsoleApp.Models;
using System.Xml;
using System.Xml.Serialization;

namespace BookMan.ConsoleApp.DataServices
{
    public class XMLDataAccess : IDataAccess
    {
        public List<Book> Books { get; set; } = new List<Book>();
        private readonly string _file = Config.Instance.DataFile;
        public void Load()
        {
            if (!File.Exists(_file))
            {
                SaveChanges();
                return;
            }
            var serializer = new XmlSerializer(typeof(XMLDataAccess));
            using (var reader = XmlReader.Create(_file))
            {
                Books = (List<Book>)serializer.Deserialize(reader);
            }
        }
        public void SaveChanges()
        {
            var serialize = new XmlSerializer(typeof(XMLDataAccess));
            using (var writer = XmlWriter.Create(_file))
            {
                serialize.Serialize(writer, Books);
            }
        }
    }
}
