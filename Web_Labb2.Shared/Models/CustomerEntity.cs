using System.Text.Json.Serialization;
using Web_Labb2.Shared.Models;

public class CustomerEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public AddressEntity? AddressInformation { get; set; }
    [JsonIgnore]
    public User User { get; set; } = null!;

    public List<OrderInfo> Orders { get; set; } = new();
}
