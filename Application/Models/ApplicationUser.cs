namespace Application.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public static ApplicationUser SystemUser = new()
        {
            Id = "11111111-1111-1111-1111-111111111111",
            FirstName = "سیستم",
            LastName = "system",
            UserName = "system",
        };
    }
}
