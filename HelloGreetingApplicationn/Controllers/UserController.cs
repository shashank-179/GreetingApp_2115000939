using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model_Layer.Model;
using ModelLayer.Model;
using Repository_Layer.Context;
using Business_Layer.Service;
using Model_Layer.Model;
using RepositoryLayer.Entity;

[Route("api/auth")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly GreetingBL _greetingBL;
    private readonly JwtService jwtService;

    public UserController(GreetingBL _greetingBL, JwtService jwtService)
    {
        this._greetingBL = _greetingBL;
        this.jwtService = jwtService;
    }
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO registerDTO)
    {
        try
        {
            var registeredUser = _greetingBL.RegisterUserBL(registerDTO);
            return Ok(new { Message = "User registered successfully", User = registeredUser });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while registering the user.", Error = ex.Message });
        }
    }

    [HttpPost]
    [Route("login")]
    public IActionResult LoginUser(LoginDTO loginDTO)
    {
        if (loginDTO == null)
        {
            return BadRequest("Invalid Credentials");
        }

        var user = _greetingBL.LoginUserBL(loginDTO); // Expecting UserEntity

        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }

        // Generate JWT token
        var token = jwtService.GenerateToken(user);

        return Ok(new { message = "Login successful", token });
    }

}


