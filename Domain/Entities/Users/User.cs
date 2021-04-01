using Domain.Base;
using Domain.Entities.Departments;
using System;

namespace Domain.Entities
{
    public partial class User : BaseEntity<int>
    {
        public User()
        {
        }

        public string UserName { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Address { get; internal set; }
        public DateTime? BirthDate { get; internal set; }
        public int DepartmentId { get; internal set; }

        public virtual Department Department { get; internal set; }
    }
}