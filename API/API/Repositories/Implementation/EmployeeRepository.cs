using API.Data;
using API.Model;
using API.Repositories.Interface;

namespace API.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApiContext _context;

        public EmployeeRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(string employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

  
    }

}
