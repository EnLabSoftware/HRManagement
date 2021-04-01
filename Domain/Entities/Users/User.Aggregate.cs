using Domain.Entities.Departments;
using System;

namespace Domain.Entities
{
    public partial class User
    {
        public User(string userName
            , string firstName
            , string lastName
            , string address
            , DateTime? birthDate
            , int departmentId)
        {
            UserName = userName;

            this.Update(
                firstName
                , lastName
                , address
                , birthDate
                , departmentId
            );
        }

        public void Update(string firstName
            , string lastName
            , string address
            , DateTime? birthDate
            , int departmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            BirthDate = birthDate;
            DepartmentId = departmentId;
        }

        public void AddDepartment(Department department)
        {
            Department = department;
        }
    }
}