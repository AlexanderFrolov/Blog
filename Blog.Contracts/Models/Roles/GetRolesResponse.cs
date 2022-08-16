namespace Blog.Contracts.Models.Roles
{
    public class GetRolesResponse
    {
        public int RolesAmount { get; set; }
        public RoleView[] Roles { get; set; }
    }

    public class RoleView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
