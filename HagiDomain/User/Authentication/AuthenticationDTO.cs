namespace HagiDomain
{

    public class AuthenticationDTO
    {
        public string? Salt { get; set; }
        public string? Username { get; set; }
        public string? HashPassword { get; set; }
    }
}