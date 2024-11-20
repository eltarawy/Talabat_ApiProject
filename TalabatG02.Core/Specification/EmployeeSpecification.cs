using TalabatG02.Core.Entities;

namespace TalabatG02.Core.Specification
{
    public class EmployeeSpecification: BaseSpecification<Employee>
    { 
        public EmployeeSpecification() 
        {
            Includes.Add(E => E.Department);
        }

        public EmployeeSpecification(int id):base(E => E.Id == id)
        {
            Includes.Add(E => E.Department);
        }
    }
}
