namespace WebApi.Data.UserContext.Entities
{
    public class UserTranslations
    {
        public int Id { get; set; }
        public int UserId {get; set;}
        public string Locale { get; set; } = null!;
        public string Name { get; set; } = null!;
        // public virtual User? User {get; set;}
        
    }
}