using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Model_Layer.Model;
using ModelLayer.Model;
using Repository_Layer.Context;
using Business_Layer.Service;
using Model_Layer.Model;
using RepositoryLayer.Entity;
using Microsoft.EntityFrameworkCore;

[Route("api/auth")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly GreetingBL _greetingBL;
    private readonly JwtService jwtService;
    private readonly UserContext userContext;
    private readonly EmailService emailService;
    public UserController(GreetingBL _greetingBL, JwtService jwtService, UserContext userContext, EmailService emailService)
    {
        this._greetingBL = _greetingBL;
        this.jwtService = jwtService;
        this.userContext = userContext;
        this.emailService = emailService;
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
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDTO forgetPasswordDTO)
    {
        if (string.IsNullOrEmpty(forgetPasswordDTO.Email))
        {
            return BadRequest("Email is required");
        }

        var user = userContext.UserDetails.FirstOrDefault(u => u.Email == forgetPasswordDTO.Email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        // Generate a reset token
        user.ResetToken = Guid.NewGuid().ToString();
        user.TokenExpiry = DateTime.UtcNow.AddHours(1); // Token expires in 1 hour
        await userContext.SaveChangesAsync();

        // Send the reset token in the email
        await emailService.SendEmailAsync(user.Email, "Password Reset", user.ResetToken);

        return Ok("Password reset token has been sent to your email.");
    }
    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
    {
        if (_greetingBL.ResetPassword(resetPasswordDTO))
            return Ok("Password has been reset successfully.");

        return BadRequest("Invalid or expired token.");
    }

}


