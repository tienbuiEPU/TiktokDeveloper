using Api.Common.Bases;

namespace Api.Entities
{
    public class UserRole : AbstractEntity<int>
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
    }
}
