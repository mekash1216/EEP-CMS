using API.Data;
using API.Model;
using API.Model.DTO;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Implementation
{
    public class ExaminerRepository : IExaminerRepository
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ExaminerRepository(ApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ExaminerDetailDto>> GetAllAssignmentsAsync()
        {
            var examiners = await _context.Examiners
                .Include(e => e.Patient)
                .Include(e => e.Assignment)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ExaminerDetailDto>>(examiners);
        }

        public async Task<ExaminerDetailDto> GetAssignmentByIdAsync(int id)
        {
            var examiner = await _context.Examiners
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<ExaminerDetailDto>(examiner);
        }


        public async Task AddAssignmentAsync(ExaminerDto examinerDto)
        {
            var assignment = _mapper.Map<Examiner>(examinerDto);
            await _context.Examiners.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssignmentAsync(ExaminerDto examinerDto)
        {
            var examiner = await _context.Examiners.FindAsync(examinerDto.Id);

            if (examiner != null)
            {
                examiner.PatientId = examinerDto.PatientId;
                examiner.AssignmentId = examinerDto.AssignmentId;
                examiner.AssignedDate = examinerDto.AssignedDate;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Assignment not found");
            }
        }

        public async Task DeleteAssignmentAsync(int id)
        {
            var examiner = await _context.Examiners.FindAsync(id);
            if (examiner == null)
            {
                throw new KeyNotFoundException("Assignment not found");
            }

            _context.Examiners.Remove(examiner);
            await _context.SaveChangesAsync();
        }
    }
}
