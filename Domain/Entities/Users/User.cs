using Domain.Base;
using System;

namespace Domain.Entities
{
    public partial class User : BaseEntity<int>
    {
        public User()
        {

        }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public int DepartmentId { get; set; }
    }
}