namespace Backend.Models
{
    public class Otp
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddMinutes(10);
    }
}
