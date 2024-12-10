
using API.Model;
using API.Model.DTO;
using API.Repositories.Implementation;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminerController : ControllerBase
    {
        private readonly IExaminerRepository _examinerRepository;
        private readonly IMapper _mapper;

        public ExaminerController(IExaminerRepository examinerRepository, IMapper mapper)
        {
            _examinerRepository = examinerRepository ?? throw new ArgumentNullException(nameof(examinerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _examinerRepository.GetAllAssignmentsAsync();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            var assignment = await _examinerRepository.GetAssignmentByIdAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddAssignment(ExaminerDto examinerDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        await _examinerRepository.AddAssignmentAsync(examinerDto);
        //        return CreatedAtAction(nameof(GetAssignmentById), new { id = examinerDto.Id }, examinerDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it in an appropriate way
        //        return StatusCode(500, "An error occurred while processing the request.");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> AddAssignment(ExaminerDto examinerDto)
        {
            await _examinerRepository.AddAssignmentAsync(examinerDto);
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, ExaminerDto examinerDto)
        {
            if (id != examinerDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _examinerRepository.UpdateAssignmentAsync(examinerDto);
                return NoContent();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                await _examinerRepository.DeleteAssignmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
             
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
