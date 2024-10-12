
namespace data.account
{
    public class Auth0SignupDTO
    {
        public string Auth0Id {get; set;}
        public string? Username {get; set;}
        public string CreatedAt {get; set;} // defaultly set as string by Auth0 ; must parse
    }
}