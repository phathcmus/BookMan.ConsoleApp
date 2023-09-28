using Newtonsoft.Json;
namespace BookMan.ConsoleApp.DataServices
{
    using Models;
    public class JsonDataAccess : IDataAccess
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
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sReader = new StreamReader(_file))
            using (JsonReader jReader = new JsonTextReader(sReader))
            {
                Books = serializer.Deserialize<List<Book>>(jReader);
            }
            //var jsonString = File.ReadAllText(_file);
            //Books = JsonConvert.DeserializeObject<List<Book>>(jsonString);
        }
        public void SaveChanges()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sWriter = new StreamWriter(_file))
            using (JsonWriter jWriter = new JsonTextWriter(sWriter))
            {
                serializer.Serialize(jWriter, Books);
            }
            //var jsonString = JsonConvert.SerializeObject(Books);
            //File.WriteAllText(_file, jsonString);
        }
    }
}