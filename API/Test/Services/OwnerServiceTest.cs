using Adapter.CustomExceptions;
using Domain;
using IRepository;
using Moq;
using Repositories.CustomExceptions;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class OwnerServiceTest
{
    #region Initialize

    private Mock<IOwnerRepository> _ownerRepository;
    private OwnerService _ownerService;

    [TestInitialize]
    public void Initialize()
    {
        _ownerRepository = new Mock<IOwnerRepository>(MockBehavior.Strict);
        _ownerService = new OwnerService(_ownerRepository.Object);
    }

    #endregion

    #region Get All Owners

    //Happy path
    [TestMethod]
    public void GetAllOwners_OwnersAreReturn()
    {
        Owner ownerInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "owner@gmail.com"
        };

        Flat flatOfOwner = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = ownerInDb,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };

        ownerInDb.Flats = new List<Flat> { flatOfOwner };

        IEnumerable<Owner> ownersInDb = new List<Owner>
        {
            ownerInDb
        };

        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners()).Returns(ownersInDb);

        IEnumerable<Owner> ownersResponse = _ownerService.GetAllOwners();
        _ownerRepository.VerifyAll();

        Assert.AreEqual(ownersInDb.Count(), ownersResponse.Count());
        Assert.IsTrue(ownersInDb.SequenceEqual(ownersResponse));
    }

    #region Get all Owners, Repository Validations

    [TestMethod]
    public void GetAllOwners_ThrowsUnknownErrorServiceException()
    {
        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners())
            .Throws(new UnknownRepositoryException("Unknown error in repository layer."));

        Assert.ThrowsException<UnknownServiceException>(() => _ownerService.GetAllOwners());
        _ownerRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Get Owner By Id

    //Happy path
    [TestMethod]
    public void GetOwnerById_OwnerIsReturned()
    {
        Owner ownerInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "owner@gmail.com"
        };

        Flat flatOfOwner = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = ownerInDb,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };

        ownerInDb.Flats = new List<Flat> { flatOfOwner };


        _ownerRepository.Setup(ownerRepository => ownerRepository.GetOwnerById(It.IsAny<Guid>())).Returns(ownerInDb);

        Owner ownerObtained = _ownerService.GetOwnerById(ownerInDb.Id);

        Assert.IsTrue(ownerInDb.Equals(ownerObtained));

        _ownerRepository.VerifyAll();
    }

    #region Get owner By Id, Repository Validations

    [TestMethod]
    public void GetOwnerById_ThrowsOwnerNotFoundServiceException()
    {
        _ownerRepository.Setup(ownerRepository => ownerRepository.GetOwnerById(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _ownerService.GetOwnerById(Guid.NewGuid()));
        _ownerRepository.VerifyAll();
    }

    [TestMethod]
    public void GetOwnerById_ThrowsUnknownServiceException()
    {
        _ownerRepository.Setup(ownerRepository => ownerRepository.GetOwnerById(It.IsAny<Guid>()))
            .Throws(new UnknownRepositoryException("Unknown error in repository layer."));

        Assert.ThrowsException<UnknownServiceException>(() => _ownerService.GetOwnerById(Guid.NewGuid()));
        _ownerRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Create Owner

    //Happy path
    [TestMethod]
    public void CreateOwner_OwnerIsCreated()
    {
        Owner ownerToCreate = new Owner
        {
            Firstname = "John",
            Lastname = "Doe",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        _ownerRepository.Setup(ownerRepository => ownerRepository.CreateOwner(ownerToCreate));
        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners()).Returns(new List<Owner>());

        _ownerService.CreateOwner(ownerToCreate);

        _ownerRepository.VerifyAll();
    }

    #region Create Owner, Domain Validations

    [TestMethod]
    public void CreateOwnerWithEmptyFirstname_ThrowsObjectErrorServiceException()
    {
        Owner ownerToCreateWithEmptyFirstname = new Owner
        {
            Firstname = "",
            Lastname = "Doe",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _ownerService.CreateOwner(ownerToCreateWithEmptyFirstname));
    }

    [TestMethod]
    public void CreateOwnerWithFirstnameThatHasSpecialDigits_ThrowsObjectErrorServiceException()
    {
        Owner ownerToCreateWithEmptyLastname = new Owner
        {
            Firstname = "John!!!",
            Lastname = "Kent",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _ownerService.CreateOwner(ownerToCreateWithEmptyLastname));
    }

    [TestMethod]
    public void CreateOwnerWithEmptyLastname_ThrowsObjectErrorServiceException()
    {
        Owner ownerToCreateWithEmptyLastname = new Owner
        {
            Firstname = "John",
            Lastname = "",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _ownerService.CreateOwner(ownerToCreateWithEmptyLastname));
    }

    [TestMethod]
    public void CreateOwnerWithLastnameThatHasSpecialDigits_ThrowsObjectErrorServiceException()
    {
        Owner ownerToCreateWithEmptyLastname = new Owner
        {
            Firstname = "John",
            Lastname = "",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _ownerService.CreateOwner(ownerToCreateWithEmptyLastname));
    }

    [TestMethod]
    public void CreateOwnerWithIncorrectPatternEmail_ThrowsObjectErrorServiceException()
    {
        Owner ownerToCreateWithWrongEmail = new Owner
        {
            Firstname = "John",
            Lastname = "Kent",
            Email = "gms.com",
            Flats = new List<Flat>()
        };
        Assert.ThrowsException<ObjectErrorServiceException>(
            () => _ownerService.CreateOwner(ownerToCreateWithWrongEmail));
    }

    #endregion

    #region Create Owner, Repository Validations

    [TestMethod]
    public void CreateOwnerWithEmailUsed_ThrowsObjectRepeatedException()
    {
        Owner ownerInDb = new Owner
        {
            Firstname = "John",
            Lastname = "Kent",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        IEnumerable<Owner> ownersInDb = new List<Owner> { ownerInDb };
        Owner ownerToCreate = new Owner
        {
            Firstname = "Johnnie",
            Lastname = "Doe",
            Email = "john@gmail.com",
            Flats = new List<Flat>()
        };

        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners()).Returns(ownersInDb);

        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _ownerService.CreateOwner(ownerToCreate));

        _ownerRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateOwner_ThrowsUnknownServiceException()
    {
        Owner ownerToCreate = new Owner
        {
            Firstname = "John",
            Lastname = "Doe",
            Email = "jhon@gmail.com",
            Flats = new List<Flat>()
        };

        _ownerRepository.Setup(ownerRepository => ownerRepository.CreateOwner(ownerToCreate))
            .Throws(new UnknownRepositoryException("Unknown error in repository layer."));
        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners()).Returns(new List<Owner>());    
        
        Assert.ThrowsException<UnknownServiceException>(() => _ownerService.CreateOwner(ownerToCreate));
    }

    #endregion

    #endregion
    
}