namespace Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public bool IsDone { get; set; }
        public bool IsNotify { get; set; }
    }
}
