﻿using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.ManagerResponses;

namespace Test.Adapters;

[TestClass]

public class ManagerAdapterTest
{
    
    private Mock<IManagerService> _managerService;
    private ManagerAdapter _managerAdapter;
    
    [TestInitialize]
    public void Initialize()
    {
        _managerService = new Mock<IManagerService>(MockBehavior.Strict);
        _managerAdapter = new ManagerAdapter(_managerService.Object);
    }
    
    [TestMethod]
    public void GetAllManagers_ShouldConvertFromDomainToResponse()
    {
        IEnumerable<Manager> domainResponse = new List<Manager>()
        {
            new Manager
            {
                Id = Guid.NewGuid(),
                Name = "Michael Kent",
                Email = "michaelKent@gmail.com",
                Password = "random238"
            }
        };

        IEnumerable<GetManagerResponse> expectedAdapterResponse = new List<GetManagerResponse>()
        {
            new GetManagerResponse
            {
                Id = domainResponse.First().Id,
                Name = domainResponse.First().Name,
                Email = domainResponse.First().Email
            }
        };
        
        _managerService.Setup(service => service.GetAllManagers()).Returns(domainResponse);
        
        IEnumerable<GetManagerResponse> adapterResponse = _managerAdapter.GetAllManagers();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetAllManagers_ShouldThrowException()
    {
        _managerService.Setup(service => service.GetAllManagers()).Throws(new Exception("Something went wrong"));
        
        Assert.ThrowsException<Exception>(() => _managerAdapter.GetAllManagers());
    }
    
    [TestMethod]
    public void DeleteManagerById_ShouldDeleteManager()
    {
        _managerService.Setup(service => service.DeleteManagerById(It.IsAny<Guid>()));
        
        _managerAdapter.DeleteManagerById(It.IsAny<Guid>());
        
        _managerService.Verify(service => service.DeleteManagerById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void DeleteManagerById_ShouldThrowObjectNotFoundAdapterException()
    {
        _managerService.Setup(service => service.DeleteManagerById(It.IsAny<Guid>())).Throws(new ObjectNotFoundServiceException());
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _managerAdapter.DeleteManagerById(It.IsAny<Guid>()));
        
        _managerService.Verify(service => service.DeleteManagerById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void DeleteManagerById_ShouldThrowException()
    {
        _managerService.Setup(service => service.DeleteManagerById(It.IsAny<Guid>())).Throws(new Exception("Something went wrong"));
        
        Assert.ThrowsException<Exception>(() => _managerAdapter.DeleteManagerById(It.IsAny<Guid>()));
        
        _managerService.Verify(service => service.DeleteManagerById(It.IsAny<Guid>()), Times.Once);
        
        
    }
    
}