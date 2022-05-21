using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using white_cloud.identity.Entities;
using white_cloud.web.Models.DTOs.Authentication;
using white_cloud.web.Services;

namespace white_cloud.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserModel registerUserModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(registerUserModel.Email);
            if (user == null)
            {
                user = new User
                {
                    Email = registerUserModel.Email,
                    UserName = registerUserModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerUserModel.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError("Could not create user with email {email}: {errors}", registerUserModel.Email, string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    return BadRequest(result.Errors);
                }
                if (registerUserModel.AccountType == AccountType.Therapist)
                {
                    await _userManager.AddToRoleAsync(user, "Therapist");
                }
                
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationEmailUrl = Url.ActionLink("confirmEmail", "user", new { token = emailToken, email = user.Email}, Request.Scheme);
                try
                {
                    await _emailService.SendEmail(user.Email, "Confirm email", $"Urmariti acest link pentru confirmarea email-ului: {confirmationEmailUrl}");
                }
                catch (Exception eex)
                {
                    _logger.LogError(eex, "Could not send confirmation email to {email}", user.Email);
                }

                _logger.LogInformation("Created user {email}", registerUserModel.Email);
            }

            return Ok();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                _logger.LogWarning("Confirmation request received but could not find user with email {email}", email);
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded)
            {
                return Redirect("/login/emailConfirmed");
            }
            return StatusCode(StatusCodes.Status409Conflict, result.Errors.Select(e => e.Description));
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("User not found");
         
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = Url.ActionLink("resetPassword", "login", new { token = token, email = user.Email }, Request.Scheme);
            await _emailService.SendEmail(model.Email, "White Cloud password reset", $"Use this link to change your password: {resetUrl}");
            return Ok();
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return StatusCode(StatusCodes.Status409Conflict, errors);
                }
                return Ok();
            }

            return NotFound("User not found");
        }
    }
}
