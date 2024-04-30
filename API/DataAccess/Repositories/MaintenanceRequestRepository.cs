﻿using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class MaintenanceRequestRepository : IMaintenanceRequestRepository
{
    #region Constructor and attributes
    
    private readonly DbContext _context;

    public MaintenanceRequestRepository(DbContext context)
    {
        _context = context;
    }
    
    #endregion
    
    #region Get All Maintenance Requests

    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests()
    {
        try
        {
            return _context.Set<MaintenanceRequest>().ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get Maintenance Request By Category

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid categoryId)
    {
        try
        {
            return _context.Set<MaintenanceRequest>()
                .Where(maintenanceRequest => maintenanceRequest.Category == categoryId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Create Maintenance Request

    public void CreateMaintenanceRequest(MaintenanceRequest requestToCreate)
    {
        try
        {
            _context.Set<MaintenanceRequest>().Add(requestToCreate);
            _context.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Update Maintenance Request

    public void UpdateMaintenanceRequest(Guid isAny, MaintenanceRequest maintenanceRequestSample)
    {
        try
        {
            _context.Set<MaintenanceRequest>().Update(maintenanceRequestSample);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get Maintenance Request By Id

    public MaintenanceRequest GetMaintenanceRequestById(Guid idToUpdate)
    {
        try
        {
            return _context.Set<MaintenanceRequest>().Find(idToUpdate);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get Maintenance Requests By Request Handler

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid requestHandlerId)
    {
        try
        {
            return _context.Set<MaintenanceRequest>()
                .Where(maintenanceRequest => maintenanceRequest.RequestHandlerId == requestHandlerId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
}