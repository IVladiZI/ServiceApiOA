namespace Application.DTOs.Users
{
    /// <RegisterRequest>
    /// Data request class for requesting data from the customer register
    /// </RegisterRequest>
    public class RegisterRequest
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
