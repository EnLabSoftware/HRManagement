using Microsoft.EntityFrameworkCore;
using Business.Departments;
using Business.Users;
using Business.Base;

namespace Data.EF
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Payslip> Payslips { get; set; }
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder
            //    .Entity<BaseEntity>(
            //        eb =>
            //        {
            //            eb.Ignore(c => c.Events);
            //        });
            //modelBuilder.Entity<RootEntity>().Ignore(c => c.Events);
            modelBuilder.Ignore<RootEntity>().Ignore<BaseDomainEvent>();

            base.OnModelCreating(modelBuilder);
        }
    }
}