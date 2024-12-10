using API.Model;

namespace API.Repositories.Interface
{
    public interface IPrescriptionItemRepository
    {
        Task<IEnumerable<PrescriptionItem>> GetAll();
        Task Add(PrescriptionItem prescriptionItem);
        Task<IEnumerable<PrescriptionItem>> GetApprovedPrescriptions();
        Task<PrescriptionItem> GetById(Guid id);
        Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionId(Guid prescriptionId);
        Task Update(PrescriptionItem prescriptionItem);
        Task DeletePrescriptionItem(Guid id);
        Task SaveChanges();
    }
}
