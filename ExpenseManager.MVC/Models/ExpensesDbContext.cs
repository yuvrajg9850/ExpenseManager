using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.MVC.Models
{
    public class ExpensesDbContext : DbContext
    {
        public DbSet<Expense> Expense { get; set; }

        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
        {
            
        }
    }
}
