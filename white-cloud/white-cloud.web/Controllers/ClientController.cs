using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using white_cloud.interfaces.Data;
using white_cloud.web.Models.DTOs;

namespace white_cloud.web.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _clientRepository;
        private readonly ITestsRepository _testsRepository;

        public ClientController(ILogger<ClientController> logger, IClientRepository clientRepository,
            ITestsRepository testsRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
            _testsRepository = testsRepository;
        }

        [HttpGet("therapist")]
        public async Task<IActionResult> GetTherapist()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var client = await _clientRepository.GetClientTherapist(userId);
            if (client == null || client.Therapist == null)
            {
                return NotFound();
            }

            var dto = new ClientTherapistInfoDto()
            {
                Id = client.Therapist.Id,
                ClientId = client.Id,
                Email = client.Therapist.User.Email,
                FirstName = client.Therapist.User.FirstName,
                LastName = client.Therapist.User.LastName,
                TherapistDate = client.ClientDate
            };
            return Ok(dto);
        }

        [HttpGet("testRequests")]
        public async Task<IActionResult> GetTestRequests()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var client = await _clientRepository.GetClient(userId);
            if (client == null)
            {
                return BadRequest();
            }
            var entities = await _clientRepository.GetTestRequests(client.Id);
            var requestDtos = await ClientTestRequestDtoMappers.ToDtoList(entities, _testsRepository);
            return Ok(requestDtos);
        }

        [HttpGet("testRequest/{id}")]
        public async Task<IActionResult> GetTestRequest(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var client = await _clientRepository.GetClient(userId);
            if (client == null)
            {
                return BadRequest();
            }
            var entity = await _clientRepository.GetTestRequest(id);
            if (entity.ClientId != client.Id)
            {
                return Forbid();
            }

            var requestDto = ClientTestRequestDtoMappers.ToDto(entity, "");
            return Ok(requestDto);
        }

        [HttpGet("testSubmissions")]
        public async Task<IActionResult> GetTestSubmissions()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            return Ok(await _clientRepository.GetTestSubmissions(userId));
        }
    }
}
