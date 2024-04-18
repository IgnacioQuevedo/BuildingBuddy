﻿using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;
using WebModels.Responses;

namespace Test.ApiControllers
{
    [TestClass]
    public class FlatControllerTest
    {

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

            Mock<IFlatAdapter> flatAdapter = new Mock<IFlatAdapter>(MockBehavior.Strict);
            flatAdapter.Setup(adapter => adapter.GetAllFlats()).Returns(expectedFlats);

            FlatController flatController = new FlatController(flatAdapter.Object);

            IActionResult controllerResponse = flatController.GetAllFlats();

            flatAdapter.VerifyAll();

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

            Mock<IFlatAdapter> flatAdapter = new Mock<IFlatAdapter>(MockBehavior.Strict);
            flatAdapter.Setup(adapter => adapter.GetAllFlats()).Throws(new Exception("Something went wrong"));

            FlatController flatController = new FlatController(flatAdapter.Object);

            IActionResult controllerResponse = flatController.GetAllFlats();

            flatAdapter.VerifyAll();

            ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        }
    }
}
