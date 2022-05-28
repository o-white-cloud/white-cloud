using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using white_cloud.entities;
using white_cloud.identity.Tokens;
using white_cloud.interfaces.Data;
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
        private readonly IUrlService _urlService;
        private readonly ITherapistsRepository _therapistsRepository;

        public UserController(
            ILogger<UserController> logger, 
            UserManager<User> userManager, 
            IEmailService emailService, 
            IUrlService urlService, 
            ITherapistsRepository therapistsRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
            _urlService = urlService;
            _therapistsRepository = therapistsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var firstName = HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

            return Ok(new UserModel
            {
                Id = id,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Roles = roles
            });
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
                    UserName = registerUserModel.Email,
                    FirstName = registerUserModel.FirstName,
                    LastName = registerUserModel.LastName,
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
                var confirmationEmailUrl = _urlService.GetConfirmEmailUrl(emailToken, user.Email);
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

        [HttpPost("registerInvite")]
        public async Task<IActionResult> RegisterClientInvite(RegisterClientInviteModel registerUserModel, CancellationToken cancellationToken)
        {
            var therapistUser = await _userManager.FindByIdAsync(registerUserModel.TherapistUserId);
            var tokenValid = await _userManager.VerifyUserTokenAsync(therapistUser, InviteUserTokenProvider.TokenType, $"invite-user-{therapistUser.Id}-{registerUserModel.Email}", registerUserModel.InviteToken);
            if(!tokenValid)
            {
                return BadRequest("Token not valid");
            }

            var therapist = await _therapistsRepository.Get(therapistUser.Id);
            if (therapist == null)
            {
                _logger.LogError("Could not find therapist for user {id}", therapistUser.Id);
                return BadRequest("Could not find therapist");
            }

            var user = await _userManager.FindByEmailAsync(registerUserModel.Email);
            if (user == null)
            {
                user = new User
                {
                    Email = registerUserModel.Email,
                    UserName = registerUserModel.Email,
                    FirstName = registerUserModel.FirstName,
                    LastName = registerUserModel.LastName,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, registerUserModel.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError("Could not create user with email {email}: {errors}", registerUserModel.Email, string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    return BadRequest(result.Errors);
                }
                _logger.LogInformation("Created user {email}", registerUserModel.Email);
            }

            await _therapistsRepository.AddClient(therapist.Id, user.Id);
            await _therapistsRepository.CompleteClientInvite(therapist.Id, user.Email);

            return Ok();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Confirmation request received but could not find user with email {email}", email);
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                var redirectUrl = _urlService.GetEmailConfirmedUrl();
                return Redirect(redirectUrl);
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
            var resetUrl = _urlService.GetResetPasswordUrl(token, user.Email);
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
