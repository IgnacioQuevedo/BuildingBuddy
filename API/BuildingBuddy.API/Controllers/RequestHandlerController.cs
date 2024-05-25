using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [AuthenticationFilter(SystemUserRoleEnum.Manager)]
    [Route("api/v1/request-handlers")]
    [ApiController]
    public class RequestHandlerController : ControllerBase
    {
        private readonly IRequestHandlerAdapter _requestHandlerAdapter;

        public RequestHandlerController(IRequestHandlerAdapter requestHandlerAdapter)
        {
            _requestHandlerAdapter = requestHandlerAdapter;
        }

        [HttpPost]
        public IActionResult CreateRequestHandler([FromBody] CreateRequestHandlerRequest requestHandlerRequest)
        {
            CreateRequestHandlerResponse response =
                _requestHandlerAdapter.CreateRequestHandler(requestHandlerRequest);
            return CreatedAtAction(nameof(CreateRequestHandler),
                new { id = response.Id }, response);
        }
    }
}