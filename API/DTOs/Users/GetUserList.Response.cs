using System;

namespace API.DTOs.Users
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? DepartmentId { get; set; }
    }
}