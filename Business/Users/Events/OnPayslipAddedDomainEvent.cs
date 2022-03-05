using Business.Base;
using Business.Users;

namespace Business.Users.Events
{
    public class OnPayslipAddedDomainEvent : BaseDomainEvent
    {
        public Payslip Payslip { get; set; }
    }
}