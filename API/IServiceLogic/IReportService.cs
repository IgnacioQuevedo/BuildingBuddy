using Domain;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IServiceLogic;

public interface IReportService
{
    IEnumerable<Report> GetMaintenanceReportByBuilding(Guid personId, Guid request);
    IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(Guid request);
    IEnumerable<Report> GetMaintenanceReportByCategory(Guid buildingId, Guid request);
}