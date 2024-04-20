using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/constructionCompanies")]
    [ApiController]
    public class ConstructionCompanyController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IConstructionCompanyAdapter _constructionCompanyAdapter;

        public ConstructionCompanyController(IConstructionCompanyAdapter constructionCompanyAdapter)
        {
            _constructionCompanyAdapter = constructionCompanyAdapter;
        }

        #endregion

        #region GetConstructionCompanies

        [HttpGet]
        public IActionResult GetConstructionCompanies()
        {
            try
            {
                return Ok(_constructionCompanyAdapter.GetConstructionCompanies());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
        
        #region CreateConstructionCompany
        
        [HttpPost]
        public IActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyRequest createConstructionCompanyRequest)
        {
            try
            {
                CreateConstructionCompanyResponse response = _constructionCompanyAdapter.CreateConstructionCompany(createConstructionCompanyRequest);
                
                return CreatedAtAction(nameof(CreateConstructionCompany), new { id = response.Id }, response);
            }
            catch (ObjectErrorException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
    }
}