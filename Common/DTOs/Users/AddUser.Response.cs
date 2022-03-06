namespace Common.DTOs.Users
{
    public class AddUserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? DepartmentName { get; set; }
    }
}