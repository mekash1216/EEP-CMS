
using Microsoft.EntityFrameworkCore;
using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using API.Utils;
using API.Repositories.Interface;
using API.Data;
using API.Model.DTO;
using AutoMapper;
using System.Security.Permissions;

namespace API.Repositories
{
    public class LabratoryTestResultRepository : ILabratoryTestResultRepository
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public LabratoryTestResultRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LaboratoryTestResult>> GetAllTestResultsAsync()
        {
            return await _context.LaboratoryTestResults.Include(l => l.SubTests).ToListAsync();
        }
        public async Task<IEnumerable<LaboratoryTestResult>> GetTestResultsByPatientIdAsync(int patientId)
        {
            return await _context.LaboratoryTestResults
                .Where(result => result.PatientId == patientId) 
                .ToListAsync();
        }
        public async Task<LaboratoryTestResult> GetTestResultByIdAsync(int id)
        {
            return await _context.LaboratoryTestResults.Include(l => l.SubTests).FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<LaboratoryTestResult> CreateTestResultAsync(LaboratoryTestResult testResult)
        {

            if (testResult.Test == "cbc")
            {
                testResult.Result = "0";
                testResult.ReferenceRange = null;
                testResult.Units = null;
                testResult.Severity = null;
            }
            else
            {
                testResult.SubTests.Clear(); 
            }
            _context.LaboratoryTestResults.Add(testResult);
            await _context.SaveChangesAsync();
            return testResult;
        }

        public async Task<LaboratoryTestResult> UpdateTestResultAsync(LaboratoryTestResult testResult)
        {
            if (testResult.Test == "cbc")
            {
                testResult.Result = null;
                testResult.ReferenceRange = null;
                testResult.Units = null;
                testResult.Severity = null;
            }
            else
            {
                testResult.SubTests.Clear(); // Clear any sub-tests if the test is not CBC
            }
            _context.Entry(testResult).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return testResult;
        }

        public async Task DeleteTestResultAsync(int id)
        {
            var testResult = await _context.LaboratoryTestResults.Include(l => l.SubTests).FirstOrDefaultAsync(r => r.Id == id);
            if (testResult != null)
            {
                _context.LaboratoryTestResults.Remove(testResult);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<LaboratoryTestResultDto> ProcessTestResultAsync(LaboratoryTestResult testResult)
        {
            if (testResult.Test == "totalBetaHCGT3" || testResult.Test == "fsh" || testResult.Test == "lh"
                || testResult.Test == "prolactin" || testResult.Test == "ggt" || testResult.Test == "ldh") 
            {
                DetermineBetaHCGSeverity(testResult);
            }
            else if (testResult.Test == "PSA")
            {
                DeterminePSASeverity(testResult);
            }
            else
            {
                // For non-PSA and non-Beta HCG tests, find the reference range based on test and gender
                var range = TestReferenceRanges.NumericReferenceRanges
                    .FirstOrDefault(r => r.Test == testResult.Test &&
                                         (r.Gender == null || r.Gender.Equals(testResult.Gender, StringComparison.OrdinalIgnoreCase)));

                if (range != default)
                {
                    // Parse the result value and determine severity
                    if (decimal.TryParse(testResult.Result, out decimal resultValue))
                    {
                        if (resultValue < range.Min)
                        {
                            testResult.Severity = "Low";
                        }
                        else if (resultValue > range.Max)
                        {
                            testResult.Severity = "High";
                        }
                        else
                        {
                            testResult.Severity = "Normal";
                        }
                    }

                    testResult.Units = range.Unit;
                    testResult.ReferenceRange = $"{range.Min} - {range.Max}";
                }
                else
                {
                    // Handle cases where the test does not have a defined range
                    testResult.Severity = "Unknown";
                }
            }

            return new LaboratoryTestResultDto
            {
                Id = testResult.Id,
                Category = testResult.Category,
                Test = testResult.Test,
                Result = testResult.Result,
                ReferenceRange = testResult.ReferenceRange,
                Units = testResult.Units,
                Gender = testResult.Gender,
                Severity = testResult.Severity,
                age = testResult.age,
            };
        }

        public void DeterminePSASeverity(LaboratoryTestResult result)
        {
            // Determine the appropriate reference range based on age
            if (result.age >= 40 && result.age <= 49)
            {
                result.ReferenceRange = "(0,2.5)";
            }
            else if (result.age >= 50 && result.age <= 59)
            {
                result.ReferenceRange = "(0,3.5)";
            }
            else if (result.age >= 60 && result.age <= 69)
            {
                result.ReferenceRange = "(0,4.5)";
            }
            else if (result.age >= 70 && result.age <= 79)
            {
                result.ReferenceRange = "(0,6.5)";
            }
            else
            {
                result.ReferenceRange = "Unknown";
            }

            // Parse the result value and determine severity
            if (decimal.TryParse(result.Result, out decimal resultValue))
            {
                var (min, max) = ParseReferenceRange(result.ReferenceRange);
                if (resultValue < min)
                {
                    result.Severity = "Low";
                }
                else if (resultValue > max)
                {
                    result.Severity = "High";
                }
                else
                {
                    result.Severity = "Normal";
                }
            }
            else
            {
                result.Severity = "Invalid Result";
            }
        }

        private (decimal min, decimal max) ParseReferenceRange(string referenceRange)
        {
            var parts = referenceRange.Trim('(', ')').Split(',');
            decimal min = parts[0] == "null" ? decimal.MinValue : decimal.Parse(parts[0]);
            decimal max = parts[1] == "null" ? decimal.MaxValue : decimal.Parse(parts[1]);
            return (min, max);
        }

        public void DetermineBetaHCGSeverity(LaboratoryTestResult result)
        {
            var range = TestReferenceRanges.BetaHCGReferenceRanges
                .FirstOrDefault(r => r.Test.Equals(result.Test, StringComparison.OrdinalIgnoreCase)
                                     && r.Gender.Equals(result.Gender, StringComparison.OrdinalIgnoreCase)
                                     && r.Phase.Equals(result.Phase, StringComparison.OrdinalIgnoreCase));

            if (range != default)
            {
                if (decimal.TryParse(result.Result, out decimal resultValue))
                {
                    var min = range.Min ?? decimal.MinValue;
                    var max = range.Max ?? decimal.MaxValue;

                    if (resultValue < min)
                    {
                        result.Severity = "Low";
                    }
                    else if (resultValue > max)
                    {
                        result.Severity = "High";
                    }
                    else
                    {
                        result.Severity = "Normal";
                    }

                    result.ReferenceRange = $"{min} - {max}";
                    result.Units = range.Unit;
                }
                else
                {
                    result.Severity = "Invalid Result";
                }
            }
            else
            {
                result.ReferenceRange = "Unknown";
                result.Severity = "Unknown";
            }
        }
        public async Task<LaboratorySubTestResultDto> ProcessSubTestResultAsync(LaboratorySubTestResult subTestResult)
        {
            var range = TestSubReferenceRanges.SubNumericReferenceRanges
                .FirstOrDefault(r => r.Test == subTestResult.Name &&
                                     (r.Gender == null || r.Gender.Equals(subTestResult.Gender, StringComparison.OrdinalIgnoreCase)));

            if (range != default)
            {
                // Parse the result value and determine severity
                if (decimal.TryParse(subTestResult.Result, out decimal resultValue))
                {
                    if (resultValue < range.Min)
                    {
                        subTestResult.Severity = "Low";
                    }
                    else if (resultValue > range.Max)
                    {
                        subTestResult.Severity = "High";
                    }
                    else
                    {
                        subTestResult.Severity = "Normal";
                    }
                }

                subTestResult.Units = range.Unit;
                subTestResult.ReferenceRange = $"{range.Min} - {range.Max}";
            }
            else
            {
                // Handle cases where the test does not have a defined range
                subTestResult.Severity = "Unknown";
            }

            // Map the result to the DTO before returning
            var subTestResultDto = _mapper.Map<LaboratorySubTestResultDto>(subTestResult);
            return subTestResultDto;
        }

  

    }
}