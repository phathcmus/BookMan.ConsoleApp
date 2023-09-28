namespace BookMan.ConsoleApp
{
    using DataServices;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    internal class Config
    {
        private static string defaultDirectoryProfile = @"C:\Users\quanp\source\repos\ConsoleApp1\ConsoleApp1";
        private static string userProfile = Directory.GetCurrentDirectory() + @"\appsettings.json";
        private readonly IConfigurationBuilder _builder = new ConfigurationBuilder()
                    .SetBasePath(File.Exists(userProfile) ? Directory.GetCurrentDirectory() : defaultDirectoryProfile)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        private IConfigurationRoot _configuration;
        private static Config _instance;
        public static Config Instance = _instance ?? (_instance = new Config());
        private Config() 
        {
            _configuration = _builder.Build();
        }
        public IDataAccess IDataAccess
        {
            get
            {
                var da = _configuration["Properties.Settings:DataAccess"];
                Console.WriteLine(da);
                switch (da.ToLower())
                {
                    case "binary": return new BinaryDataAccess();
                    case "json": return new JsonDataAccess();
                    case "xml": return new XMLDataAccess();
                    default: return new BinaryDataAccess();
                }
            }
        }
        public string DataAccess
        {
            get => _configuration["Properties.Settings:DataAccess"];
            set
            {
                _configuration["Properties.Settings:DataAccess"] = value;
                File.WriteAllText("appsettings.json", Serialize(_configuration).ToString());
            }
        }
        public string PromptText
        {
            get => _configuration["Properties.Settings:PromptText"]; 
            set
            {
                _configuration["Properties.Settings:PromptText"] = value;
                File.WriteAllText("appsettings.json", Serialize(_configuration).ToString());
            }
        }
        public ConsoleColor PromptColor
        {
            get
            {
                try
                {
                    return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _configuration["Properties.Settings:PromptColor"], true);
                }
                catch (Exception)
                {
                }
                return ConsoleColor.Green;
            }
            set
            {
                _configuration["Properties.Settings:PromptColor"] = value.ToString();
                File.WriteAllText("appsettings.json", Serialize(_configuration).ToString());
            }
        }
        public string DataFile
        {
            get => _configuration["Properties.Settings:DataFile"];
            set
            {
                _configuration["Properties.Settings:DataFile"] = value;
                File.WriteAllText("appsettings.json", Serialize(_configuration).ToString());
            }
        }
        private static JToken Serialize(IConfiguration config)
        {
            JObject json = new JObject();
            foreach (var child in config.GetChildren())
            {
                json.Add(child.Key, Serialize(child));
            }

            if (!json.HasValues && config is IConfigurationSection section)
                return new JValue(section.Value);

            return json;
        }
    }
}