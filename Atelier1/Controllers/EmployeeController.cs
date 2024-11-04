using Atelier1.Models;
using Atelier1.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atelier1.Controllers
{
	public class EmployeeController : Controller
	{
		readonly IRepository<Employee> employeeRepository;

		public EmployeeController(IRepository<Employee> empRepository)
		{
			employeeRepository = empRepository;
		}

		// GET: EmployeeController
		public ActionResult Index()
		{
			var employees = employeeRepository.GetAll();

            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
		}

		// GET: EmployeeController/Details/5
		public ActionResult Details(int id)
		{
			var employee = employeeRepository.FindByID(id);
			return View(employee);
		}

		// GET: EmployeeController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: EmployeeController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Employee e)
		{ try
			{ 
			if (employeeRepository.GetAll().Contains(e))
			{
				ModelState.AddModelError("id_exist", "ID DEJA EXISTANT");
				return View();
			}
			else
				employeeRepository.Add(e);
			return RedirectToAction(nameof(Index));
		}
            catch
            {
                return View();
	}

}

// GET: EmployeeController/Edit/5
public ActionResult Edit(int id)
		{
			var employee = employeeRepository.FindByID(id);

			return View(employee);

		}

		// POST: EmployeeController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Employee newemp)
		{
			try
			{
				employeeRepository.Update(id, newemp);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}

		}

		// GET: EmployeeController/Delete/5
		public ActionResult Delete(int id)
		{
			var emp = employeeRepository.FindByID(id);
            
            return View(emp);
			           
		}
        public ActionResult Search(string term)
        {
            var result = employeeRepository.Search(term);
            return View("Index", result);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(Employee emp)
		{

            try
            {
               employeeRepository.Delete(emp.Id);
            return RedirectToAction("Index");

                    }
            catch
            {
                return View();
            }



        }
    }
}
