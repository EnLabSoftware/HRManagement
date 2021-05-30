namespace Domain.Users
{
    public class PayslipValueObject
    {
        public PayslipValueObject(float workingDays
            , float coefficientsSalary
            , decimal bonus)
        {
            TotalSalary = (decimal)(workingDays * coefficientsSalary) + bonus;
        }

        public decimal TotalSalary { get; init; }
    }
}