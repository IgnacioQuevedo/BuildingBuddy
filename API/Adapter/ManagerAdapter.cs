﻿using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.ManagerResponses;

namespace Adapter;

public class ManagerAdapter
{
    private readonly IManagerService _managerServiceLogic;

    public ManagerAdapter(IManagerService managerServiceLogic)
    {
        _managerServiceLogic = managerServiceLogic;
    }

    public IEnumerable<GetManagerResponse> GetAllManagers()
    {
        try
        {
            IEnumerable<Manager> serviceResponse = _managerServiceLogic.GetAllManagers();

            IEnumerable<GetManagerResponse> adapterResponse = serviceResponse.Select(manager =>
                new GetManagerResponse
                {
                    Id = manager.Id,
                    Email = manager.Email,
                    Name = manager.Name,
                });

            return adapterResponse;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    public void DeleteManagerById(Guid id)
    {
        try
        {
            _managerServiceLogic.DeleteManagerById(id);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
}