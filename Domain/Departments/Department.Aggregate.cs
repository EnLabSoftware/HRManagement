using Domain.Users;
using System.Collections.Generic;

namespace Domain.Departments
{
    public partial class Department
    {
        public Department()
        {
            Users = new HashSet<User>();
        }

        public Department(string name
            , string description) : this()
        {
            this.Update(name, description);
        }

        public void Update(string name
            , string description)
        {
            Name = name;
            Description = description;
        }
    }
}