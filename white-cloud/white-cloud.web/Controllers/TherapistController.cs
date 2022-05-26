using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using white_cloud.entities;
using white_cloud.identity.Tokens;
using white_cloud.interfaces.Data;
using white_cloud.web.Services;

namespace white_cloud.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = Roles.Therapist)]
    public class TherapistController : ControllerBase
    {
        private readonly ILogger<TherapistController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUrlService _urlService;
        private readonly ITherapistsRepository _therapistsRepository;

        public TherapistController(
            ILogger<TherapistController> logger,
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

        [HttpPost("invite")]
        public async Task<IActionResult> InviteClient([FromBody] string email)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var therapist = await _therapistsRepository.Get(user.Id);
            if(therapist == null)
            {
                _logger.LogError("Could not find therapist for the user with id {id}", user.Id);
                return NotFound("Could not find therapist for the user");
            }

            var token = await _userManager.GenerateUserTokenAsync(user, InviteUserTokenProvider.TokenType, $"invite-user-{user.Id}-{email}");
            var url = _urlService.GetInviteUserEmailUrl(Url, token, email, user.Id, Request.Scheme);
            var emailBody = $"{user.FirstName} {user.LastName} va invita in platforma noastra. Click aici pentru a va crea un cont: {url}";
            await _emailService.SendEmail(email, "Invitatie in platforma", emailBody);
            var invite = new ClientInvite()
            {
                TherapistId = therapist.Id,
                Email = email,
                SentDate = DateTime.Now.ToUniversalTime(),
            };
            invite = await _therapistsRepository.InsertClientInvite(invite);
            return Ok(invite);
        }

        [HttpGet("invites")]
        public async Task<IActionResult> GetInvites()
        {
            var therapist = await GetTherapistFromContext();
            if(therapist == null)
            {
                return NotFound("Could not find therapist");
            }
            var invites = await _therapistsRepository.GetClientInvites(therapist.Id);
            return Ok(invites);
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            var therapist = await GetTherapistFromContext();
            if (therapist == null)
            {
                return NotFound("Could not find therapist");
            }
            var clients = await _therapistsRepository.GetClients(therapist.Id);
            return Ok(clients);
        }

        private async Task<Therapist?> GetTherapistFromContext()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _therapistsRepository.Get(userId);
        }
    }
}
