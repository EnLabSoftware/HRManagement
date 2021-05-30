using Domain.Base;
using System;

namespace Domain.Users
{
    public class Payslip : BaseEntity<int>
    {
        public Payslip(int userId
            , DateTime date
            , float workingDays
            , decimal bonus)
        {
            UserId = userId;
            Date = date;
            WorkingDays = workingDays;
            Bonus = bonus;
        }

        public PayslipValueObject Value;

        public DateTime Date { get; private set; }
        public float WorkingDays { get; private set; }
        public bool IsPaid { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public int UserId { get; private set; }
        public decimal TotalSalary { get; private set; }
        public decimal Bonus { get; private set; }

        public virtual User User { get; private set; }

        public void Pay(
            float coefficientsSalary
            )
        {
            if (IsPaid)
                throw new Exception("This Payslip has been paid.");

            IsPaid = true;
            Value = new PayslipValueObject(WorkingDays, coefficientsSalary, Bonus);
            TotalSalary = Value.TotalSalary;
            PaymentDate = DateTime.Now;
        }
    }
}