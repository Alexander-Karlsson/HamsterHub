namespace HamsterHub.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int HamsterId { get; set; }
    public Hamster Hamster { get; set; } = null!;
    public int Score { get; set; }
    public string Comment { get; set; } = String.Empty;
    public string CustomerName { get; set; } = String.Empty;
    public DateTime ReviewCreatedDate { get; set; } = DateTime.UtcNow;
}