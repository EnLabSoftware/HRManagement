using Domain.Base;
using System.Collections.Generic;

namespace Domain.Entities.Departments
{
    public partial class Department : BaseEntity<short>
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }

        public virtual ICollection<User> Users { get; internal set; }
    }
}