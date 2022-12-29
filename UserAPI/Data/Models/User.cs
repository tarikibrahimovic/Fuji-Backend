using System.Security.Claims;

namespace UserAPI.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; } = new byte[32];
        public byte[]? PassswordSalt { get; set; } = new byte[32];
        public string? Sub { get; set; } = string.Empty;
        public string? VerificationToken { get; set; }
        public string Role { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? PictureUrl { get; set; } = string.Empty;
        public List<Comment> Komentar { get; set; }
        public List<Links> Linkovi { get; set; }
        public List<LinkVotes> Votes { get; set; }
    }
}
