﻿using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.InvitationResponses;

namespace Adapter;

public class InvitationAdapter
{
    
    private readonly IInvitationServiceLogic _invitationServiceLogic;
    
    public InvitationAdapter(IInvitationServiceLogic invitationServiceLogic)
    {
        _invitationServiceLogic = invitationServiceLogic;
    }
    
    public IEnumerable<GetInvitationResponse> GetAllInvitations()
    {
        try
        {
            IEnumerable<Invitation> serviceResponse = _invitationServiceLogic.GetAllInvitations();
            
            IEnumerable<GetInvitationResponse> adapterResponse = serviceResponse.Select(invitation => new GetInvitationResponse
            {
                Id = invitation.Id,
                Status = (StatusEnumResponse)invitation.Status,
                ExpirationDate = invitation.ExpirationDate,
                Email = invitation.Email,
                Firstname = invitation.Firstname,
                Lastname = invitation.Lastname
            });

            return adapterResponse;
        }
        catch (Exception exceptionCaught)
        {   
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    public GetInvitationResponse GetInvitationById(Guid idOfInvitationToFind)
    {
        try
        {
            Invitation serviceResponse = _invitationServiceLogic.GetInvitationById(idOfInvitationToFind);

            return new GetInvitationResponse
            {
                Id = serviceResponse.Id,
                Status = (StatusEnumResponse)serviceResponse.Status,
                ExpirationDate = serviceResponse.ExpirationDate,
                Email = serviceResponse.Email,
                Firstname = serviceResponse.Firstname,
                Lastname = serviceResponse.Lastname
            };
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
    }
    
}