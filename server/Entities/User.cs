namespace server.Entities;

public class User : BaseEntity
{
   public required string Username { get; set; } 

   public required string Password { get; set; }

   public ICollection<Items> Items { get; set; } = new List<Items>();
}