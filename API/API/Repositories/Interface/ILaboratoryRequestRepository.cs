using API.Model;

namespace API.Repositories.Interface
{
    public interface ILaboratoryRequestRepository
    {
        Task<IEnumerable<LaboratoryRequest>> GetAllRequests();
        Task<LaboratoryRequest> GetRequestById(int id);
        Task<LaboratoryRequest> CreateRequest(LaboratoryRequest request);
        Task<LaboratoryRequest> UpdateRequest(LaboratoryRequest request);
        Task DeleteRequest(int id);
    }
}
