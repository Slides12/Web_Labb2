using Web_Labb2.Shared.DTO_s;

namespace Web_Labb2.DTO_s
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public AddressDTO? AddressInformation { get; set; }
    }
}
