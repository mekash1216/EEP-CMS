using API.Data;
using API.Model;
using API.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public class ReferralRepository : IReferralRepository
    {
        private readonly ApiContext _context;

        public ReferralRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Referral> AddReferralAsync(Referral referral)
        {
            _context.Referrals.Add(referral);
            await _context.SaveChangesAsync();
            return referral;
        }

        public async Task<Referral> GetReferralByIdAsync(int id)
        {
            return await _context.Referrals
                .Include(r => r.Examiner)
                .ThenInclude(e => e.Patient)
                .Include(r => r.Examiner)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Referral>> GetAllReferralsAsync()
        {
            return await _context.Referrals
                .Include(r => r.Examiner)
                    .ThenInclude(e => e.Patient)
                .Include(r => r.Examiner)
                .ToListAsync();
        }


        public async Task<bool> DeleteReferralAsync(int id)
        {
            var referral = await _context.Referrals.FindAsync(id);
            if (referral == null)
                return false;

            _context.Referrals.Remove(referral);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Referral> GetReferralWithDetailsAsync(int examinerId)
        {
            return await _context.Referrals
                .Where(r => r.ExaminerId == examinerId)
                .Include(r => r.Examiner)
                .ThenInclude(e => e.Patient)
                .Include(r => r.Examiner)
                .FirstOrDefaultAsync();
        }


        public async Task<Referral> UpdateReferralAsync(Referral referral)
        {
            try
            {
                // Retrieve the existing referral from the database
                var existingReferral = await _context.Referrals.FindAsync(referral.Id);
                if (existingReferral == null)
                {
                    return null; // or throw an exception
                }

                // Update only the specific properties that are allowed to change
                existingReferral.ReferralDate = referral.ReferralDate;
                existingReferral.InvestigationResult = referral.InvestigationResult;
                existingReferral.Reason = referral.Reason;
                existingReferral.Clinicalfinding= referral.Clinicalfinding;
                existingReferral.Diagnosis= referral.Diagnosis;
                existingReferral.Rxgiven= referral.Rxgiven;
                existingReferral.To = referral.To;
                existingReferral.From = referral.From;


                // Keep the original ExaminerId unchanged
                //existingReferral.ExaminerId = referral.ExaminerId;

                // Update the existing referral entity
                _context.Referrals.Update(existingReferral);
                await _context.SaveChangesAsync();

                return existingReferral;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating referral with ID {referral.Id}: {ex.Message}");
                throw; 
            }
        }

        public async Task<IEnumerable<Referral>> GetByPatientId(int patientId)
        {
            return await _context.Referrals
                .Include(r => r.Examiner)
                    .ThenInclude(e => e.Patient) 
                .Include(r => r.Examiner)
                .Where(r => r.Examiner.PatientId == patientId)
                .ToListAsync();
        }




    }

}
