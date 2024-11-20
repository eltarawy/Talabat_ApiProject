using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Repositories;
using TalabatG02.Core.Specification;

namespace TalabatG02.APIs.Controllers
{
    public class EmployeeController : ApiBaseController
    {
        private readonly IGenericRepository<Employee> empRepo;

        public EmployeeController(IGenericRepository<Employee> empRepo)
        {
            this.empRepo = empRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployee()
        {
            var apec = new EmployeeSpecification();
            var employees = empRepo.GetAllWithSpecAsync(apec);

            return Ok(employees);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var apec = new EmployeeSpecification(id);
            var employee = empRepo.GetByIdWithSpecAsync(apec);

            return Ok(employee);

        }

    }
}
 