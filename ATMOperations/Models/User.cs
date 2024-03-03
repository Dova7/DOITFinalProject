namespace ATMOperations.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Pin { get; set; }
        public decimal Balance { get; set; }
    }
}