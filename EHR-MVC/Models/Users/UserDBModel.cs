namespace EHR_MVC.Models.Users
{
    public class UserDBModel
    {
        public long UserId { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public required string UserEmail { get; set; }
        public required string PasswordHashed { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
