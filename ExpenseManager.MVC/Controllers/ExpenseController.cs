using ExpenseManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.MVC.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpensesDbContext _context;
        public ExpenseController(ExpensesDbContext expensesDbContext)
        {
            _context = expensesDbContext;
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult GetAllExpensesView()
        {
            var allExpenses = _context.Expense.ToList();

            ViewBag.Expenses = allExpenses.Sum(x => x.Value);
            ViewData["Remaining"] = 10000 - ViewBag.Expenses;

            return View(allExpenses);
        }

        public IActionResult AddUpdateExpenseView(int id)
        {
            var expense = _context.Expense.FirstOrDefault(x => x.Id == id);
            if (expense is not null)
            {
                return View(expense);
            }

            return View();
        }
        public IActionResult AddUpdateExpenseInDb(Expense requestExpense)
        {
            var expense = _context.Expense.FirstOrDefault(x => x.Id == requestExpense.Id);

            if (expense is null)
            {
                _context.Expense.Add(requestExpense);
            }
            else
            {
                expense.Value = requestExpense.Value;
                expense.Description = requestExpense.Description;

                _context.Expense.Update(expense);
            }
            _context.SaveChanges();

            return RedirectToAction("GetAllExpensesView");
        }

        //public IActionResult AddUpdateExpenseInDb(int? id)
        //{
        //    var expense = new Expense();
        //    if (id is not null)
        //    {
        //        expense = _context.Expense.First(x => x.Id == id);
        //        TempData["expense"] = expense;
        //        RedirectToAction("AddUpdateExpenseInDb");
        //    }
            

        //    return RedirectToAction("AddUpdateExpenseInDb");
        //}

        public IActionResult DeleteExpense(int id)
        {
            var expense = _context.Expense.FirstOrDefault(x => x.Id == id);

            if (expense is not null)
            {
                _context.Expense.Remove(expense);
                _context.SaveChanges();
            }

            return RedirectToAction("GetAllExpensesView");
        }
    }
}
