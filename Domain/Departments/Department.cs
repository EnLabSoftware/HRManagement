using Domain.Base;
using Domain.Users;
using System.Collections.Generic;

namespace Domain.Departments
{
    public partial class Department : BaseEntity<short>
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }

        public virtual ICollection<User> Users { get; internal set; }
    }
}