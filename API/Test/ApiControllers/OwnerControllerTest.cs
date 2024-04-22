﻿using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers;

[TestClass]
public class OwnerControllerTest
{
    #region  Initialize
    
    private Mock<IOwnerAdapter> _ownerAdapter;
    private OwnerController _ownerController;
    
    [TestInitialize]
    public void Initialize()
    {
        _ownerAdapter = new Mock<IOwnerAdapter>(MockBehavior.Strict);
        _ownerController = new OwnerController(_ownerAdapter.Object);
    }
    
    #endregion

    #region GetOwners

    [TestMethod]
    public void GetOwners_OkIsReturned()
    {
        IEnumerable<GetOwnerResponse> expectedOwners = new List<GetOwnerResponse>()
        {
            new GetOwnerResponse()
            {
                Id = Guid.NewGuid(),
                Name = "myOwner",
                Lastname = "myLastName",
                Email = "email@email.com",
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedOwners);
        
        _ownerAdapter.Setup(adapter => adapter.GetOwners()).Returns(expectedOwners);
        
        IActionResult controllerResponse = _ownerController.GetOwners();

        _ownerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetOwnerResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetOwnerResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedOwners));
    }
    
    [TestMethod]
    public void GetOwners_InternalServerErrorIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;
        
        _ownerAdapter.Setup(adapter => adapter.GetOwners()).Throws(new Exception("Something went wrong"));
        
        IActionResult controllerResponse = _ownerController.GetOwners();

        _ownerAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion
    
    #region CreateOwner
    
    [TestMethod]
    public void CreateOwner_CreatedAtActionIsReturned()
    {
        CreateOwnerResponse expectedOwner = new CreateOwnerResponse()
        {
            Id = Guid.NewGuid()
        };
        
        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateOwner", "CreateOwner"
                , expectedOwner.Id, expectedOwner);
        
        _ownerAdapter.Setup(adapter => adapter.CreateOwner(It.IsAny<CreateOwnerRequest>())).Returns(expectedOwner);
        
        IActionResult controllerResponse = _ownerController.CreateOwner(It.IsAny<CreateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    [TestMethod]
    public void CreateOwner_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Error on property");
        
        _ownerAdapter.Setup(adapter => adapter.CreateOwner(It.IsAny<CreateOwnerRequest>())).Throws(new ObjectErrorAdapterException("Error on property"));
        
        IActionResult controllerResponse = _ownerController.CreateOwner(It.IsAny<CreateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    [TestMethod]
    public void CreateOwner_InternalServerErrorIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;
        
        _ownerAdapter.Setup(adapter => adapter.CreateOwner(It.IsAny<CreateOwnerRequest>())).Throws(new Exception("Something went wrong"));
        
        IActionResult controllerResponse = _ownerController.CreateOwner(It.IsAny<CreateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    #endregion
    
    #region UpdateOwner

    [TestMethod]
    public void UpdateOwner_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();
        _ownerAdapter.Setup(adapter => adapter.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>()));
        
        IActionResult controllerResponse = _ownerController.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>());
        
        _ownerAdapter.Verify(
            adapter => adapter.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>()), Times.Once());
            
        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);

    }
    
    [TestMethod]
    public void UpdateOwner_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Error on property");
        
        _ownerAdapter.Setup(adapter => adapter.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>())).Throws(new ObjectErrorAdapterException("Error on property"));
        
        IActionResult controllerResponse = _ownerController.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    [TestMethod]
    public void UpdateOwnerRequest_NotFoundIsReturned()
    {
        NotFoundObjectResult expectedControllerResponse = new NotFoundObjectResult("The specific owner was not found in Database");
        
        _ownerAdapter.Setup(adapter => adapter.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>())).Throws(new ObjectNotFoundAdapterException());
        
        IActionResult controllerResponse = _ownerController.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    [TestMethod]
    public void UpdateOwner_InternalServerErrorIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;
        
        _ownerAdapter.Setup(adapter => adapter.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>())).Throws(new Exception("Something went wrong"));
        
        IActionResult controllerResponse = _ownerController.UpdateOwner(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    #endregion

}