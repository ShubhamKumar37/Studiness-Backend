namespace Backend.Models
{
    public class Otp : BaseEntity
    {
        public string Value { get; set; } = "999999";
        public string Email { get; set; } = "Studiness@gmail.com";
        public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddMinutes(10);
    }
}
