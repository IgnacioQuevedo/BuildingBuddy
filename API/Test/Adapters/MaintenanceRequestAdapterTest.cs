﻿using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace Test.Adapters;

[TestClass]
public class MaintenanceRequestAdapterTest
{
    #region Initialize
    
    private Mock<IMaintenanceRequestService> _maintenanceRequestService;
    private MaintenanceRequestAdapter _maintenanceRequestAdapter;

    private MaintenanceRequest genericMaintenanceRequest;
    private GetMaintenanceRequestResponse genericMaintenanceRequestResponse;
    private CreateRequestMaintenanceRequest _dummyCreateRequestMaintenanceResponse;
    private UpdateMaintenanceRequestStatusRequest dummyUpdateRequest;

    [TestInitialize]
    public void Initialize()
    {
        _maintenanceRequestService = new Mock<IMaintenanceRequestService>(MockBehavior.Strict);
        _maintenanceRequestAdapter = new MaintenanceRequestAdapter(_maintenanceRequestService.Object);

        _dummyCreateRequestMaintenanceResponse = new CreateRequestMaintenanceRequest();
        dummyUpdateRequest = new UpdateMaintenanceRequestStatusRequest();
        
        genericMaintenanceRequest = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Description = "Test Description Random",
            FlatId = Guid.NewGuid(),
            Category = Guid.NewGuid(),
            RequestStatus = StatusEnum.Accepted,
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid()
        };
        
        genericMaintenanceRequestResponse = new GetMaintenanceRequestResponse
        {
            Id = genericMaintenanceRequest.Id,
            BuildingId = genericMaintenanceRequest.BuildingId,
            Description = genericMaintenanceRequest.Description,
            FlatId = genericMaintenanceRequest.FlatId,
            Category = genericMaintenanceRequest.Category,
            RequestStatus = (StatusEnumMaintenanceResponse)genericMaintenanceRequest.RequestStatus,
            OpenedDate = genericMaintenanceRequest.OpenedDate,
            ClosedDate = genericMaintenanceRequest.ClosedDate,
            RequestHandlerId = genericMaintenanceRequest.RequestHandlerId
        };
    }
    
    #endregion
    
    #region Get All Maintenance Requests

    [TestMethod]
    public void GetAllMaintenanceRequests_ReturnsMaintenanceRequestResponses()
    {
        IEnumerable<MaintenanceRequest> expectedServiceResponse = new List<MaintenanceRequest>
            { genericMaintenanceRequest };

        IEnumerable<GetMaintenanceRequestResponse> expectedAdapterResponse =
            new List<GetMaintenanceRequestResponse> {genericMaintenanceRequestResponse};

        _maintenanceRequestService.Setup(service => service.GetAllMaintenanceRequests())
            .Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceRequestResponse> adapterResponse =
            _maintenanceRequestAdapter.GetAllMaintenanceRequests();
        _maintenanceRequestService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
        
    }

    [TestMethod]
    public void GetAllMaintenanceRequests_ThrowsException_ReturnsExceptionMessage()
    {
        _maintenanceRequestService.Setup(service => service.GetAllMaintenanceRequests())
            .Throws(new Exception("Something went wrong"));

        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.GetAllMaintenanceRequests());

        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.GetAllMaintenanceRequests(), Times.Once);
    }
    
    #endregion
    
    #region Get Maintenance Request By Category Id

    [TestMethod]
    public void GetMaintenanceRequestByCategoryId_ReturnsMaintenanceRequestResponse()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestByCategory(It.IsAny<Guid>()))
            .Returns(genericMaintenanceRequest);

        GetMaintenanceRequestResponse adapterResponse =
            _maintenanceRequestAdapter.GetMaintenanceRequestByCategory(genericMaintenanceRequest.Id);
        _maintenanceRequestService.VerifyAll();

        Assert.AreEqual(genericMaintenanceRequestResponse, adapterResponse);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestById_ShouldThrowObjectNotFoundAdapterException()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestByCategory(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _maintenanceRequestAdapter.GetMaintenanceRequestByCategory(genericMaintenanceRequest.Id));
        
        _maintenanceRequestService.Verify(service => service.GetMaintenanceRequestByCategory(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestByCategory_ThrowsException()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestByCategory(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.GetMaintenanceRequestByCategory(genericMaintenanceRequest.Id));

        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.GetMaintenanceRequestByCategory((It.IsAny<Guid>())), Times.Once);
    }
    
    #endregion
    
    #region Create Maintenance Request
    
    [TestMethod]
    public void CreateMaintenanceRequest_ShouldReturnCreateMaintenanceRequestResponse()
    {
        _maintenanceRequestService.Setup(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()));
        
        CreateRequestMaintenanceResponse adapterResponse = _maintenanceRequestAdapter.CreateMaintenanceRequest(_dummyCreateRequestMaintenanceResponse);
        
        Assert.IsNotNull(adapterResponse);
        
        _maintenanceRequestService.Verify(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_ShouldThrowException()
    {
        _maintenanceRequestService.Setup(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()))
            .Throws(new Exception("Something went wrong"));
        
        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.CreateMaintenanceRequest(_dummyCreateRequestMaintenanceResponse));
        
        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_ShouldThrowObjectErrorAdapterException()
    {
        _maintenanceRequestService.Setup(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()))
            .Throws(new ObjectErrorServiceException("Description can't be empty"));
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _maintenanceRequestAdapter.CreateMaintenanceRequest(_dummyCreateRequestMaintenanceResponse));
        
        _maintenanceRequestService.Verify(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    #endregion
    
    #region Update Maintenance Request
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldUpdateMaintenanceRequest()
    {
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()));
        
        _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, dummyUpdateRequest);
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldThrowObjectErrorAdapterException()
    {
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new ObjectErrorServiceException("Request status can't be empty"));
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, dummyUpdateRequest));
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldThrowObjectNotFoundAdapterException()
    {
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new ObjectNotFoundServiceException());
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, dummyUpdateRequest));
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldThrowException()
    {
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new Exception("Something went wrong"));
        
        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, dummyUpdateRequest));
        
        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    #endregion
    
    #region Assign Maintenance Request

    [TestMethod]
    public void AssignMaintenanceRequest_ShouldAssignMaintenanceRequest()
    {
        _maintenanceRequestService
            .Setup(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()));
        
        _maintenanceRequestAdapter.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>());
        
        _maintenanceRequestService.Verify(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void AssignMaintenanceRequest_ShouldThrowObjectNotFoundAdapterException()
    {
        _maintenanceRequestService
            .Setup(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _maintenanceRequestAdapter.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()));
        
        _maintenanceRequestService.Verify(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void AssignMaintenanceRequest_ShouldThrowException()
    {
        _maintenanceRequestService
            .Setup(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));
        
        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()));
        
        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }
    
    #endregion
    
    #region Get Maintenance Requests By Handler
    
    [TestMethod]
    public void GetMaintenanceRequestsByHandler_ShouldReturnMaintenanceRequestResponses()
    {
        IEnumerable<MaintenanceRequest> expectedServiceResponse = new List<MaintenanceRequest>
            { genericMaintenanceRequest };

        IEnumerable<GetMaintenanceRequestResponse> expectedAdapterResponse =
            new List<GetMaintenanceRequestResponse> {genericMaintenanceRequestResponse};

        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestsByRequestHandler(It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceRequestResponse> adapterResponse =
            _maintenanceRequestAdapter.GetMaintenanceRequestsByRequestHandler(genericMaintenanceRequest.RequestHandlerId);
        
        _maintenanceRequestService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetMaintenanceRequestsByHandler_ShouldThrowException()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestsByRequestHandler(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.GetMaintenanceRequestsByRequestHandler(genericMaintenanceRequest.RequestHandlerId));

        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.GetMaintenanceRequestsByRequestHandler(It.IsAny<Guid>()), Times.Once);
        
    }
    
    #endregion
}