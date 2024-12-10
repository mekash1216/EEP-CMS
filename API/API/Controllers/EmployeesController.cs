using API.Data;
using API.Model;
using API.Model.DTO;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest("Employee data is null.");
            }

            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeDTO.EmployeeId);

            if (existingEmployee != null)
            {
                return Conflict("Employee with this ID already exists.");
            }

            var employee = _mapper.Map<Employee>(employeeDTO);
            await _employeeRepository.AddAsync(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = employee.EmployeeId }, employeeDTO);
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(string employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return Ok(employeeDTO);
        }

    }

}
