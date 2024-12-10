// API/Repositories/Implementation/PrescriptionItemRepository.cs

using API.Data;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Implementation
{

    public class PrescriptionItemRepository : IPrescriptionItemRepository
    {
        private readonly ApiContext _context;

        public PrescriptionItemRepository(ApiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PrescriptionItem>> GetAll()
        {
            return await _context.PrescriptionItems.ToListAsync();
        }
        public async Task<PrescriptionItem> GetById(Guid id)
        {
            return await _context.PrescriptionItems.FindAsync(id);
        }
        public async Task<IEnumerable<PrescriptionItem>> GetApprovedPrescriptions()
        {
            return await _context.PrescriptionItems
                .Where(pi => pi.IsApproved && pi.Quantity > pi.ApprovedQuantity)
                .ToListAsync();
        }
        public async Task Add(PrescriptionItem prescriptionItem)
        {
            await _context.PrescriptionItems.AddAsync(prescriptionItem);
        }

        public async Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionId(Guid prescriptionId)
        {
            return await _context.PrescriptionItems
                                 .Where(pi => pi.PrescriptionId == prescriptionId)
                                 .Include(pi => pi.Stock)
                                 .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(PrescriptionItem prescriptionItem)
        {
            _context.Entry(prescriptionItem).State = EntityState.Modified;
        }

        public async Task DeletePrescriptionItem(Guid id)
        {
            var item = await _context.Set<PrescriptionItem>().FindAsync(id);
            if (item != null)
            {
                _context.Set<PrescriptionItem>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}