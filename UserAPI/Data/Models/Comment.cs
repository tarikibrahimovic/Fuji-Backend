
namespace UserAPI.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Komentar { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int IdSadrzaja { get; set; }
        public string TipSadrzaja { get; set; }
        public string Date { get; set; }
    }
}
