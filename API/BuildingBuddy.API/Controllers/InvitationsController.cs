using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests;
using WebModels.Responses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationAdapter _invitationAdapter;

        public InvitationsController(IInvitationAdapter invitationAdapter)
        {
            _invitationAdapter = invitationAdapter;
        }

        [HttpGet]
        public IActionResult GetAllInvitations()
        {
            try
            {
                return Ok(_invitationAdapter.GetAllInvitations());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("{idOfInvitation:Guid}")]
        public IActionResult GetInvitationById([FromRoute] Guid idOfInvitation)
        {
            try
            {
                return Ok(_invitationAdapter.GetInvitationById(idOfInvitation));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("Invitation was not found, reload the page");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest request)
        {
            try
            {
                CreateInvitationResponse response = _invitationAdapter.CreateInvitation(request);
                return CreatedAtAction(nameof(CreateInvitation), new { id = response.Id }, response);
            }
            catch (ObjectErrorException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}