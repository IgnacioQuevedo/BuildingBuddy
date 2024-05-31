﻿using Adapter.CustomExceptions;
using ILoaders;
using IServiceLogic;
using Moq;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.LoaderReponses;

namespace Test.Adapters;

[TestClass]
public class LoaderAdapterTest
{
    private Mock<ILoaderService> _mockService;
    private LoaderAdapter _loaderAdapter;
    private Mock<ILoader> _mockLoader;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<ILoaderService>();
        _mockLoader = new Mock<ILoader>();
        _loaderAdapter = new LoaderAdapter(_mockService.Object);
    }

    [TestMethod]
    public void CreateAllBuildingsFromLoad_ReturnsListOfCreateBuildingFromLoadResponse()
    {
        string filePath = "test";
        string loaderName = "testLoader";
        
        CreateBuildingFromLoadResponse createBuildingFromLoadResponse = new CreateBuildingFromLoadResponse
        {
            idOfBuildingCreated = Guid.NewGuid(),
            Details = "test, ok"
        };
        CreateLoaderRequest createLoaderRequest = new CreateLoaderRequest
        {
            Filepath = filePath,
            LoaderName = loaderName
        };

        List<CreateBuildingFromLoadResponse> createBuildingFromLoadResponses = new List<CreateBuildingFromLoadResponse>
        {
            createBuildingFromLoadResponse
        };

        _mockLoader.Setup(loader => loader.LoaderName()).Returns(loaderName);
        _mockLoader.Setup(loader => loader.LoadAllBuildings(filePath)).Returns(createBuildingFromLoadResponses);

        List<ILoader> loaders = new List<ILoader> { _mockLoader.Object };

        _mockService.Setup(service => service.GetAllImporters()).Returns(loaders);

        List<CreateBuildingFromLoadResponse> result = _loaderAdapter.CreateAllBuildingsFromLoad(createLoaderRequest);

        Assert.IsInstanceOfType(result, typeof(List<CreateBuildingFromLoadResponse>));
        Assert.AreEqual(createBuildingFromLoadResponses.Count, result.Count);
        Assert.AreEqual(createBuildingFromLoadResponse.idOfBuildingCreated, result[0].idOfBuildingCreated);
        Assert.AreEqual(createBuildingFromLoadResponse.Details, result[0].Details);
    }
}
