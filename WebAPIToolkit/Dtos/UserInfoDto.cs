namespace WebAPIToolkit.Dtos
{
    public class UserInfoDto
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}