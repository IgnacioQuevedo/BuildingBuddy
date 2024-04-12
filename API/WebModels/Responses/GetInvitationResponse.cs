
namespace WebModels.Responses;

public class GetInvitationResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Email { get; set; }
    public StatusEnumResponse Status { get; set; }
    public DateTime ExpirationDate { get; set; }
    
    public override bool Equals(object objectToCompare)
    {
        GetInvitationResponse? toCompare = objectToCompare as GetInvitationResponse;

        if (toCompare is null) return false;
        
        return Id == toCompare.Id &&
               Firstname == toCompare.Firstname &&
               Email == toCompare.Email &&
               Status == toCompare.Status &&
               ExpirationDate == toCompare.ExpirationDate;
    }
    
}