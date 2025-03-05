using Business_Layer.Service;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Model;
using NLog;
using NLog.Config;
using Repository_Layer.Entity;
using Repository_Layer.Interface;

namespace HelloGreetingApplicationn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingApplicationnController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        GreetingBL _greetingBL;
        UserModel userModel;
        private readonly IGreetingRL _greetingRL;


        public HelloGreetingApplicationnController(GreetingBL _greetingBL, UserModel userModel, IGreetingRL greetingRL)
        {
            this._greetingBL = _greetingBL;
            this.userModel = userModel;
            _greetingRL = greetingRL;
        }
        [HttpGet("list-greetings")]
        public IActionResult GetAllGreetings()
        {
            var greetings = _greetingBL.GetAllGreetings();

            if (greetings == null || greetings.Count == 0)
            {
                return NotFound("No greetings found.");
            }

            return Ok(greetings);
        }

        [HttpGet("find-greeting/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var greeting = _greetingBL.GetGreetingById(id);

            if (greeting == null)
            {
                return NotFound("Greeting not found.");
            }

            return Ok(greeting);
        }

        [HttpPost("save-greeting")]
        public IActionResult SaveGreeting([FromBody] GreetingRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { error = "Message cannot be empty." });
            }

            var greetingEntity = new GreetingEntity { Message = request.Message };
            _greetingRL.SaveGreeting(greetingEntity);
            return Ok(new { message = "Greeting saved successfully." });
        }

        // Define a request model class:
        public class GreetingRequest
        {
            public string Message { get; set; }
        }

        [HttpGet("Hello/World/Message")]
        public string DefaultGreeting()
        {
            return _greetingBL.GetGreeting();
        }
        [HttpGet("personalized/greeting")]
        public string GetPersonalizedGreeting() 
        {
            return _greetingBL.PersonalizedGreeting(userModel);
        }
        [HttpGet]
        public IActionResult Get()
        {
            _logger.Info("GET request received.");
            var greetingMessage = _greetingBL.GetGreeting();
            
            var responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Hello to Greeting App API Endpoint",
                Data = "Hello, World"
            };

            _logger.Info("GET response: {@Response}", responseModel);
            return Ok(responseModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] RequestModel requestModel)
        {
            _logger.Info("POST request received: Key = {Key}, Value = {Value}", requestModel.Key, requestModel.Value);

            var responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Request received successfully",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            _logger.Info("POST response: {@Response}", responseModel);
            return Ok(responseModel);
        }

        [HttpPut("edit-greeting/{id}")]
        public IActionResult EditGreeting(int id, [FromBody] GreetingUpdateRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.NewMessage))
            {
                return BadRequest("New message cannot be empty.");
            }

            var updatedGreeting = _greetingBL.UpdateGreeting(id, request.NewMessage);

            if (updatedGreeting == null)
            {
                return NotFound("Greeting not found.");
            }

            return Ok(new { message = "Greeting updated successfully.", updatedGreeting });
        }

        
        public class GreetingUpdateRequest
        {
            public string NewMessage { get; set; }
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] RequestModel requestModel)
        {
            _logger.Info("PATCH request received: Key = {Key}, Value = {Value}", requestModel.Key, requestModel.Value);

            var responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Data partially updated successfully",
                Data = $"Updated Key: {requestModel.Key}, Updated Value: {requestModel.Value}"
            };

            _logger.Info("PATCH response: {@Response}", responseModel);
            return Ok(responseModel);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] RequestModel requestModel)
        {
            _logger.Info("DELETE request received: Key = {Key}", requestModel.Key);

            var responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Data deleted successfully",
                Data = $"Deleted Key: {requestModel.Key}, Deleted Value: {requestModel.Value}"
            };

            _logger.Info("DELETE response: {@Response}", responseModel);
            return Ok(responseModel);
        }
    }
}
