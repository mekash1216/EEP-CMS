
using Microsoft.AspNetCore.Mvc;
using API.Repositories;
using API.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.DTO;
using API.Repositories.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryTestResultController : ControllerBase
    {
        private readonly ILabratoryTestResultRepository _laboratoryRepository;
        private readonly IMapper _mapper;

        public LaboratoryTestResultController(ILabratoryTestResultRepository laboratoryRepository, IMapper mapper)
        {
            _laboratoryRepository = laboratoryRepository;
            _mapper = mapper;
        }

        [HttpGet("results/patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<LaboratoryTestResultDto>>> GetTestResultsByPatientId(int patientId)
        {
            // Fetch test results for the given patient ID
            var testResults = await _laboratoryRepository.GetTestResultsByPatientIdAsync(patientId);

            // Check if no test results found
            if (testResults == null || !testResults.Any())
            {
                return NotFound();
            }

            // Map the test results to DTOs
            var testResultDtos = await Task.WhenAll(testResults.Select(result => ProcessTestResultAsync(result)));

         

            return Ok(testResultDtos);
        }

        // Method to process test results and map to DTO
        private async Task<LaboratoryTestResultDto> ProcessTestResultAsync(LaboratoryTestResult result)
        {
            return new LaboratoryTestResultDto
            {
                Id = result.Id,
                Category = result.Category,
                Test = result.Test,
                Result = result.Result,
                ReferenceRange = result.ReferenceRange,
                Units = result.Units,
                Gender = result.Gender,
                Severity = result.Severity,
                PatientId = result.PatientId,
                TestDate = result.TestDate
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaboratoryTestResultDto>>> GetAllTestResults()
        {
            var testResults = await _laboratoryRepository.GetAllTestResultsAsync();
            if (testResults == null || !testResults.Any())
            {
                return NotFound();
            }

            var testResultDtos = testResults.Select(result => _mapper.Map<LaboratoryTestResultDto>(result));
            return Ok(testResultDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LaboratoryTestResultDto>> GetTestResult(int id)
        {
            var testResult = await _laboratoryRepository.GetTestResultByIdAsync(id);
            if (testResult == null)
            {
                return NotFound();
            }

            var testResultDto = await _laboratoryRepository.ProcessTestResultAsync(testResult);
            return Ok(testResultDto);
        }

        [HttpPost]
        public async Task<ActionResult<LaboratoryTestResult>> CreateTestResult(LaboratoryResultDto testResultDto)
        {
           
            var testResult = _mapper.Map<LaboratoryTestResult>(testResultDto);
            if (testResultDto.Test == "cbc")
            {
                testResult.SubTests = testResultDto.SubTests.Select(subTestDto => _mapper.Map<LaboratorySubTestResult>(subTestDto)).ToList();

                foreach (var subTest in testResult.SubTests)
                {
                    // Process each subtest individually
                    var processedSubTest = await _laboratoryRepository.ProcessSubTestResultAsync(subTest);
                    subTest.ReferenceRange = processedSubTest.ReferenceRange;
                    subTest.Units = processedSubTest.Units;
                    subTest.Severity = processedSubTest.Severity;
                }
            }

                var processedResultDto = await _laboratoryRepository.ProcessTestResultAsync(testResult);

            testResult.ReferenceRange = processedResultDto.ReferenceRange;
            testResult.Units = processedResultDto.Units;
            testResult.Severity = processedResultDto.Severity;
            testResult.TestDate = DateTime.UtcNow;
            var createdTestResult = await _laboratoryRepository.CreateTestResultAsync(testResult);

            return CreatedAtAction(nameof(GetTestResult), new { id = createdTestResult.Id }, createdTestResult);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestResult(int id, LaboratoryResultDto testResultDto)
        {
            if (id != testResultDto.Id)
            {
                return BadRequest();
            }

            // Map the DTO to the LaboratoryTestResult model
            var testResult = _mapper.Map<LaboratoryTestResult>(testResultDto);
            if (testResultDto.Test == "cbc")
            {
                testResult.SubTests = testResultDto.SubTests.Select(subTestDto => _mapper.Map<LaboratorySubTestResult>(subTestDto)).ToList();
            }

            // Process the result to set ReferenceRange, Units, and Severity
            var processedResultDto = await _laboratoryRepository.ProcessTestResultAsync(testResult);

            // Update the original testResult object with the processed values from processedResultDto
            testResult.ReferenceRange = processedResultDto.ReferenceRange;
            testResult.Units = processedResultDto.Units;
            testResult.Severity = processedResultDto.Severity;
            testResult.TestDate = testResultDto.TestDate;
            // Update the result in the database
            await _laboratoryRepository.UpdateTestResultAsync(testResult);

            return NoContent();
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestResult(int id)
        {
            await _laboratoryRepository.DeleteTestResultAsync(id);
            return NoContent();
        }
    }
}