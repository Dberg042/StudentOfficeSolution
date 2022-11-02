using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StudentOffice.Data;
using StudentOffice.Models;
using StudentOffice.ViewModels;

namespace StudentOffice.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly StudentOfficeContext _context;

        public EmployeesController(StudentOfficeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = _context.Employees.
                Include(e => e.Team).
                Include(e=> e.EmployeeRole).
                Include(p =>p.EmployeeConditions).ThenInclude(p=>p.Condition).
                AsNoTracking();
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Team)
                .Include(e => e.EmployeeRole)
                .Include(p => p.EmployeeConditions).ThenInclude(p => p.Condition)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var employee = new Employee();
            PopulateAssignedConditionData(employee);
            PopulateDropDownLists();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber,FirstName,MiddleName,LastName,DOB,SuggestionNumber,Phone,EmployeeRoleID,TeamID")] Employee employee, string[] selectedOptions)
        {
            try
            {
                //Add the selected conditions
                if (selectedOptions != null)
                {
                    foreach (var condition in selectedOptions)
                    {
                        var conditionToAdd = new EmployeeCondition { EmployeeID = employee.ID, ConditionID = int.Parse(condition) };
                        employee.EmployeeConditions.Add(conditionToAdd);
                    }
                }

                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employees.EmployeeNumber"))
                {
                    ModelState.AddModelError("EmployeeNumber", "Unable to save changes. Remember, you cannot have duplicate EmployeeNumber numbers.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            PopulateAssignedConditionData(employee);
            PopulateDropDownLists(employee);
            return View(employee);
        }

     

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(p => p.EmployeeConditions)
                .ThenInclude(p => p.Condition)
                .FirstOrDefaultAsync(p =>p.ID ==id);
            if (employee == null)
            {
                return NotFound();
            }
            PopulateAssignedConditionData(employee);
            PopulateDropDownLists(employee);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedOptions)
        {
            var employeeToUpdate = await _context.Employees
                .Include(p => p.EmployeeConditions)
                .ThenInclude(p => p.Condition)
                .FirstOrDefaultAsync(p => p.ID == id);

            //Check that you got it or exit with a not found error
            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            UpdateEmployeeConditions(selectedOptions, employeeToUpdate);
            //Try updating it with the values posted- My white list
            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "",
                p => p.EmployeeNumber, p => p.FirstName, p => p.MiddleName, p => p.LastName, p => p.DOB,
                p => p.SuggestionNumber, p => p.Phone, p => p.TeamID, p => p.EmployeeRoleID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                //try 5 times then raise exception
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("Cannot insert duplicate key row in object"))
                    {
                        ModelState.AddModelError("EmployeeNumber", "Unable to save changes. Remember, you cannot have duplicate Employee Number.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
                
            }
            PopulateAssignedConditionData(employeeToUpdate);
            PopulateDropDownLists(employeeToUpdate);
            return View(employeeToUpdate);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Team)
                .Include(e => e.EmployeeRole)
                .Include(p => p.EmployeeConditions).ThenInclude(p => p.Condition)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'StudentOfficeContext.Employees'  is null.");
            }
            var employee = await _context.Employees
               .Include(e => e.Team)
               .Include(e => e.EmployeeRole)
               .Include(p => p.EmployeeConditions).ThenInclude(p => p.Condition)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {

                //Note: there is really no reason a delete should fail if you can "talk" to the database.
                ModelState.AddModelError("", "Unable to delete record. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }

        private void PopulateAssignedConditionData(Employee employee)
        {
            //For this to work, you must have Included the EmployeeConditions 
            //in the Employee
            var allOptions = _context.Conditions;
            var currentOptionIDs = new HashSet<int>(employee.EmployeeConditions.Select(b => b.ConditionID));
            var checkBoxes = new List<CheckOptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new CheckOptionVM
                {
                    ID = option.ID,
                    DisplayText = option.ConditionName,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["ConditionOptions"] = checkBoxes;
        }
        private void UpdateEmployeeConditions(string[] selectedOptions, Employee employeeToUpdate)
        {
            if (selectedOptions == null)
            {
                employeeToUpdate.EmployeeConditions = new List<EmployeeCondition>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var employeeOptionsHS = new HashSet<int>
                (employeeToUpdate.EmployeeConditions.Select(c => c.ConditionID));//IDs of the currently selected conditions
            foreach (var option in _context.Conditions)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString())) //It is checked
                {
                    if (!employeeOptionsHS.Contains(option.ID))  //but not currently in the department
                    {
                        employeeToUpdate.EmployeeConditions.Add(new EmployeeCondition { EmployeeID = employeeToUpdate.ID, ConditionID = option.ID });
                    }
                }
                else
                {
                    //Checkbox Not checked
                    if (employeeOptionsHS.Contains(option.ID)) //but it is currently in the department - so remove it
                    {
                        EmployeeCondition conditionToRemove = employeeToUpdate.EmployeeConditions.SingleOrDefault(c => c.ConditionID == option.ID);
                        _context.Remove(conditionToRemove);
                    }
                }
            }
        }

        private SelectList TeamSelectList(int? selectedId)
        {
          return new SelectList(_context.Teams
                   .OrderBy(d => d.LastName)
                   .ThenBy(d => d.FirstName)
                   , "ID", "FormalName", selectedId);

        }

        private SelectList EmployeeRoleSelectList(int? selectedId)
        {
            return new SelectList(_context
                   .EmployeeRoles
                   .OrderBy(d => d.RoleName)
                   , "ID", "RoleName", selectedId);

        }
        private void PopulateDropDownLists(Employee employee = null)
        {
            ViewData["TeamID"] = TeamSelectList(employee?.TeamID);
            ViewData["EmployeeRoleID"] = EmployeeRoleSelectList(employee?.EmployeeRoleID);
        }
        private bool EmployeeExists(int id)
        {
          return _context.Employees.Any(e => e.ID == id);
        }
    }
}
