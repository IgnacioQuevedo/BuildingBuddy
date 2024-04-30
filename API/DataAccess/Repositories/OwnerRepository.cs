using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly DbContext _dbContext;

    public OwnerRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IEnumerable<Owner> GetAllOwners()
    {
        try
        {
            return _dbContext.Set<Owner>().Include(owner => owner.Flats).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
        
    }

    public Owner GetOwnerById(Guid ownerIdToObtain)
    {
        try
        {
            return _dbContext.Set<Owner>().Find(ownerIdToObtain);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
        
    }

    public void CreateOwner(Owner ownerToCreate)
    {
        try
        {
            _dbContext.Set<Owner>().Add(ownerToCreate);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void UpdateOwnerById(Owner ownerWithUpdates)
    {
        throw new NotImplementedException();
    }
}