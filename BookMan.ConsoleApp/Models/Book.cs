namespace BookMan.ConsoleApp.Models
{
    [Serializable]
    public class Book
    {
        private int _id = 1;
        public int Id
        {
            get { return _id; }
            set { if (value >= 1) _id = value; }
        }
        private string _authors = "Unknown author";
        public string Authors
        {
            get { return _authors; }
            set { if (!string.IsNullOrEmpty(value)) _authors = value; }
        }
        private string _title = "A new book";
        public string Title
        {
            get { return _title; }
            set { if (!string.IsNullOrEmpty(value)) _title = value; }
        }
        private string _publisher = "Unknown publisher";
        public string Publisher
        {
            get { return _publisher; }
            set { if (!string.IsNullOrEmpty(value)) _publisher = value; }
        }
        private int _year = 2018;
        public int Year
        {
            get { return _year; }
            set { if (value >= 1950) _year = value; }
        }
        private int _edition = 1;
        public int Edition
        {
            get { return _edition; }
            set { if (value >= 1) _edition = value; }
        }
        public string Isbn { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Description { get; set; } = "A new book";
        private int _rating = 1;
        public int Rating
        {
            get { return _rating; }
            set { if (value >= 1 && value <= 5) _rating = value; }
        }
        public bool Reading { get; set; }
        private string _file;
        public string File
        {
            get { return _file; }
            set { if (System.IO.File.Exists(value)) _file = value; }
        }
        public string FileName
        {
            get { return System.IO.Path.GetFileName(_file); }
        }
    }
}