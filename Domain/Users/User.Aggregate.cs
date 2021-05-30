using Domain.Base;
using Domain.Departments;
using Domain.Entities.Users.Events;
using System;
using System.Linq;

namespace Domain.Users
{
    public partial class User: IAggregateRoot
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

        public void AddDepartment(int departmentId)
        {
            DepartmentId = departmentId;
        }

        public Payslip AddPayslip(DateTime date
            , float workingDays
            , decimal bonus
            , bool isPaid
            )
        {
            // Make sure there's only one payslip  per month
            var exist = PaySlips.Any(_ => _.Date.Month == date.Month && _.Date.Year == date.Year);
            if (exist)
                throw new Exception("Payslip for this month already exist.");

            var payslip = new Payslip(this.Id, date, workingDays, bonus);
            if (isPaid)
            {
                payslip.Pay(this.CoefficientsSalary);
            }

            PaySlips.Add(payslip);

            var addEvent = new OnPayslipAddedDomainEvent()
            {
                Payslip = payslip
            };

            AddEvent(addEvent);

            return payslip;
        }
    }
}