// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ELMSWebAPI.Models;

// namespace ELMSWebAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class EmployeeController : ControllerBase
//     {
//         private readonly ELMSWebAPIContext _context;

//         public EmployeeController(ELMSWebAPIContext context)
//         {
//             _context = context;
//         }

//         // GET: api/Employee
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
//         {
//           if (_context.Employee == null)
//           {
//               return NotFound();
//           }
//             return await _context.Employee.ToListAsync();
//         }

//         // GET: api/Employee/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Employee>> GetEmployee(long id)
//         {
//           if (_context.Employee == null)
//           {
//               return NotFound();
//           }
//             var employee = await _context.Employee.FindAsync(id);

//             if (employee == null)
//             {
//                 return NotFound();
//             }

//             return employee;
//         }

//         // PUT: api/Employee/5
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutEmployee(long id, Employee employee)
//         {
//             if (id != employee.Id)
//             {
//                 return BadRequest();
//             }

//             _context.Entry(employee).State = EntityState.Modified;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!EmployeeExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }

//             return NoContent();
//         }

//         // POST: api/Employee
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPost]
//         public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
//         {
//           if (_context.Employee == null)
//           {
//               return Problem("Entity set 'ELMSWebAPIContext.Employee'  is null.");
//           }
//             _context.Employee.Add(employee);
//             await _context.SaveChangesAsync();

//             return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
//         }

//         // DELETE: api/Employee/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteEmployee(long id)
//         {
//             if (_context.Employee == null)
//             {
//                 return NotFound();
//             }
//             var employee = await _context.Employee.FindAsync(id);
//             if (employee == null)
//             {
//                 return NotFound();
//             }

//             _context.Employee.Remove(employee);
//             await _context.SaveChangesAsync();

//             return NoContent();
//         }

//         private bool EmployeeExists(long id)
//         {
//             return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
//         }
//     }
// }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELMSWebAPI.Models;

namespace ELMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ELMSWebAPIContext _context;

        public EmployeeController(ELMSWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            try
            {
                var employees = await _context.Employee.ToListAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving employees.");
            }
        }

        // GET: api/employee/{id}
        [HttpGet("{id:regex(\\d+)}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the employee.");
            }
        }

        // PUT: api/employee/{id}
        [HttpPut("{id:regex(\\d+)}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest();
                }

                _context.Entry(employee).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the employee.");
            }
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the employee.");
            }
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id:regex(\\d+)}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the employee.");
            }
        }

        private bool EmployeeExists(long id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}