using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace IAdapter;

public interface IMaintenanceAdapter
{
    public IEnumerable<GetMaintenanceRequestResponse> GetAllMaintenanceRequests();
    public GetMaintenanceRequestResponse GetMaintenanceRequestByCategory(Guid categoryId);
    public CreateRequestMaintenanceResponse CreateMaintenanceRequest(CreateRequestMaintenanceRequest requestToCreate);
        
}