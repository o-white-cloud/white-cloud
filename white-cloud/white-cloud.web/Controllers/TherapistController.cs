using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using white_cloud.entities;
using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;
using white_cloud.identity.Tokens;
using white_cloud.interfaces.Data;
using white_cloud.web.Models.DTOs;
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
        private readonly ITestsRepository _testsRepository;

        public TherapistController(
            ILogger<TherapistController> logger,
            UserManager<User> userManager,
            IEmailService emailService,
            IUrlService urlService,
            ITherapistsRepository therapistsRepository,
            ITestsRepository testsRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
            _urlService = urlService;
            _therapistsRepository = therapistsRepository;
            _testsRepository = testsRepository;
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteClient([FromBody(EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Allow)] string? email, [FromQuery] int? id)
        {
            if (!id.HasValue && string.IsNullOrEmpty(email))
            {
                return BadRequest("Email or existing invite id must be present in the request");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var therapist = await _therapistsRepository.Get(user.Id);
            if (therapist == null)
            {
                _logger.LogError("Could not find therapist for the user with id {id}", user.Id);
                return NotFound("Could not find therapist for the user");
            }

            var invite = id.HasValue ? await _therapistsRepository.GetClientInvite(id.Value) : new ClientInvite()
            {
                TherapistId = therapist.Id,
                Email = email,
                SentDate = DateTime.UtcNow,
            };

            if (invite == null)
            {
                return NotFound("Could not find invite");
            }

            var token = await _userManager.GenerateUserTokenAsync(user, InviteUserTokenProvider.TokenType, $"invite-user-{user.Id}-{invite.Email}");
            var url = _urlService.GetInviteUserEmailUrl(token, invite.Email, user.Id);
            var emailBody = $"{user.FirstName} {user.LastName} va invita in platforma noastra. Click aici pentru a va crea un cont: {url}";
            await _emailService.SendEmail(invite.Email, "Invitatie in platforma", emailBody);

            if (invite.Id != default)
            {
                invite.SentDate = DateTime.UtcNow;
                await _therapistsRepository.UpdateClientInvite(invite);
            }
            else
            {
                invite = await _therapistsRepository.InsertClientInvite(invite);
            }
            return Ok(invite);
        }

        [HttpGet("invites")]
        public async Task<IActionResult> GetInvites()
        {
            var therapist = await GetTherapistFromContext();
            if (therapist == null)
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
            var clientEntities = await _therapistsRepository.GetClients(therapist.Id);
            var clients = clientEntities.Select(x => x.Adapt<ClientDto>());
            return Ok(clients);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            var therapist = await GetTherapistFromContext();
            if (therapist == null)
            {
                return NotFound("Therapist not found");
            }
            if (!await _therapistsRepository.IsClient(therapist.Id, clientId)) ;
            var clientEntity = await _therapistsRepository.GetClient(clientId);
            if (clientEntity == null)
            {
                return NotFound("Client not found");
            }

            var client = new ClientDto
            {
                ClientDate = clientEntity.ClientDate,
                Id = clientEntity.Id,
                UserId = clientEntity.UserId,
                UserEmail = clientEntity.User.Email,
                UserFirstName = clientEntity.User.FirstName,
                UserLastName = clientEntity.User.LastName,
                Age = clientEntity.Age,
                Gender = clientEntity.Gender,
                Ocupation = clientEntity.Ocupation
            };
            return Ok(client);
        }


        [HttpPost("testRequest")]
        public async Task<IActionResult> RequestTest(RequestATestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var therapist = await GetTherapistFromContext();
            if (therapist == null)
            {
                return NotFound("Could not find therapist");
            }

            if (!await _therapistsRepository.IsClient(therapist.Id, model.ClientId))
            {
                return NotFound("Client does not belong to therapist");
            }

            var testRequests = model.TestIds.Select(id => new TestRequest()
            {
                ClientId = model.ClientId,
                TherapistId = therapist.Id,
                TestId = id,
                SentDate = DateTime.UtcNow
            });
            var requestEntities = await _therapistsRepository.AddTestRequests(testRequests);
            var requestDtos = await ClientTestRequestDtoMappers.ToDtoList(requestEntities, _testsRepository);
            return Ok(requestDtos);
        }

        [HttpDelete("testRequest/{id}")]
        public async Task<IActionResult> RequestTest(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var therapist = await GetTherapistFromContext();
            if (therapist == null)
            {
                return NotFound("Could not find therapist");
            }

            var result = await _therapistsRepository.DeleteTestRequest(id);
            if (result == null)
            {
                return NotFound();
            }
            if (!result.Value)
            {
                return Conflict();
            }
            return Ok();
        }

        [HttpGet("testRequests")]
        public async Task<IActionResult> GetTestRequests(int clientId)
        {
            var therapist = await GetTherapistFromContext();
            if (therapist == null)
                return NotFound("Could not find therapist");
            if (!await _therapistsRepository.IsClient(therapist.Id, clientId))
            {
                return Forbid("Client does not belong to therapist");
            }

            var requestEntities = await _therapistsRepository.GetTestRequests(therapist.Id, clientId);
            var requestDtos = await ClientTestRequestDtoMappers.ToDtoList(requestEntities, _testsRepository);
            return Ok(requestDtos);
        }

        [HttpGet("testShares")]
        public async Task<IActionResult> GetClientTestShares(int clientId)
        {
            var therapist = await GetTherapistFromContext();
            if (therapist is null)
                return NotFound("Could not find therapist");
            if (!await _therapistsRepository.IsClient(therapist.Id, clientId))
            {
                return Forbid("Client does not belong to therapist");
            }
            var client = await _therapistsRepository.GetClient(clientId);
            if(client is null)
            {
                return NotFound("Client not found");
            }
            var shares = await _therapistsRepository.GetTestSubmissionShares(therapist.Id, client.UserId);
            var tests = await _testsRepository.GetTestsMap();
            var dtos = shares.Select(s =>
            {
                var dto = s.Adapt<ClientTestSubmissionShareDto>();
                dto.TestSubmissionTestName = tests.ContainsKey(dto.TestSubmissionTestId) ? tests[dto.TestSubmissionTestId].Name : "";
                return dto;
            });
            return Ok(dtos);
        }

        private async Task<Therapist?> GetTherapistFromContext()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _therapistsRepository.Get(userId);
        }
    }
}
