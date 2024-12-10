using API.Model;

namespace API.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(string employeeId);
        Task AddAsync(Employee employee);
       
    }

}
