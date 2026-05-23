using Badminton.Model.Abstract;
namespace Badminton.Model
{
    public class User:Entity
    {
        public required string Account { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Name { get; set; } 
        public ICollection<Role>? Roles { get; set; }
    }
}
