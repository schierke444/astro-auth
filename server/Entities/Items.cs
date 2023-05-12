namespace server.Entities;

public class Items : BaseEntity 
{
   public required string Name { get; set; }

    public Guid UserId { get; set; }
   public User? User { get; set; } 
}