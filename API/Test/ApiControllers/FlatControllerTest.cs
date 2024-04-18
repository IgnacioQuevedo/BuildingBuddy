﻿using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Requests;
using WebModel.Requests.FlatRequests;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;
using WebModels.Responses;

namespace Test.ApiControllers
{
    [TestClass]
    public class FlatControllerTest
    {
        #region Initilizing aspects
        private Mock<IFlatAdapter> _flatAdapter;
        private FlatController _flatController;

        [TestInitialize]
        public void Initialize()
        {
            _flatAdapter = new Mock<IFlatAdapter>(MockBehavior.Strict);
            _flatController = new FlatController(_flatAdapter.Object);
        }

        #endregion

        #region GetAllFlats

        [TestMethod]
        public void GetAllFlats_OkIsReturned()
        {
            IEnumerable<GetFlatResponse> expectedFlats = new List<GetFlatResponse>()
            {
                new GetFlatResponse()
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = 102,
                    Owner = new OwnerResponse()
                    {
                        Name = "Barry",
                        Lastname = "White",
                        Email =         "barrywhite@gmail.com",
                    },
                    TotalRooms = 3,
                    TotalBaths = 2
                }
            };

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedFlats);

            _flatAdapter.Setup(adapter => adapter.GetAllFlats()).Returns(expectedFlats);

            IActionResult controllerResponse = _flatController.GetAllFlats();

            _flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            List<GetFlatResponse>? controllerResponseValueCasted =
                controllerResponseCasted.Value as List<GetFlatResponse>;
            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(controllerResponseValueCasted));
        }

        [TestMethod]
        public void GetAllFlats_500StatusCodeIsReturned()
        {
            ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
            expectedControllerResponse.StatusCode = 500;

            _flatAdapter.Setup(adapter => adapter.GetAllFlats()).Throws(new Exception("Something went wrong"));

            FlatController flatController = new FlatController(_flatAdapter.Object);

            IActionResult controllerResponse = flatController.GetAllFlats();

            _flatAdapter.VerifyAll();

            ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        }

        #endregion

        #region  CreateFlat
        [TestMethod]
        public void CreateFlatRequest_OkIsReturned()
        {
            CreateFlatResponse expectedAdapterResponse = new CreateFlatResponse()
            {
                Id = Guid.NewGuid()
            };

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedAdapterResponse);

            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<Guid>())).Returns(expectedAdapterResponse);

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<Guid>());

            _flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            CreateFlatResponse? controllerResponseValueCasted =
                controllerResponseCasted.Value as CreateFlatResponse;
            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.IsTrue(controllerResponseValueCasted.Id.Equals(controllerResponseValueCasted.Id));
        }

        [TestMethod]
        public void CreateFlatRequest_BadRequestIsReturned()
        {
            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<Guid>())).Throws(new ObjectErrorException("Owner can't be null"));

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<Guid>());

            BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Owner can't be null");

            _flatAdapter.VerifyAll();

            BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        }

        [TestMethod]
        public void CreateFlatRequest_500StatusCodeIsReturned()
        {
            ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
            expectedControllerResponse.StatusCode = 500;

            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<Guid>()))
                .Throws(new Exception("An specific error on the server"));

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<Guid>());
            _flatAdapter.VerifyAll();

            ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(controllerResponseCasted.Value, expectedControllerResponse.Value);
            Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);

        }

        #endregion
    }
}