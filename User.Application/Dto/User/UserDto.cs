namespace User.Application.Dto.User
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid MediaId { get; set; }
        public string Base64Media { get; set; }

    }
}
