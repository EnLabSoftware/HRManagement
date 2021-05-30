using Domain.Base;
using Domain.Users;

namespace Domain.Entities.Users.Events
{
    public class OnPayslipAddedDomainEvent : BaseDomainEvent
    {
        public Payslip Payslip { get; set; }
    }
}