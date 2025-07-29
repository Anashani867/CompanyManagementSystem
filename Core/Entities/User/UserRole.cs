namespace Core.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public User User { get; set; } = null!;
        public int UserId { get; set; }
        public Role Role { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
