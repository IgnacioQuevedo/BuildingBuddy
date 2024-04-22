namespace WebModel.Requests.MaintenanceRequests;

public class CreateRequestMaintenanceRequest
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public string? Description { get; set; }
    public Guid FlatId { get; set; }
    public Guid Category { get; set; }
    public StatusEnumMaintenanceRequest RequestStatus { get; set; }
}