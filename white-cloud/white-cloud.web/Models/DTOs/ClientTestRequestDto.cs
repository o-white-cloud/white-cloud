using Mapster;
using white_cloud.entities.Tests;
using white_cloud.interfaces.Data;

namespace white_cloud.web.Models.DTOs
{
    public class ClientTestRequestDto
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public int TestId { get; set; } = 0;
        public string TestName { get; set; } = string.Empty;
        public int TherapistId { get; set; } 
        public string TherapistUserFirstName { get; set;} = string.Empty;
        public string TherapistUserLastName { get; set; } = string.Empty;
    }

    public static class ClientTestRequestDtoMappers
    {
        public static async Task<IEnumerable<ClientTestRequestDto>> ToDtoList(IEnumerable<TestRequest> requestEntities, ITestsRepository testsRepository)
        {
            var tests = await testsRepository.GetTests();
            var testsDict = tests.ToDictionary(t => t.Id, t => t);
            var requestDtos = requestEntities.Where(r => testsDict.ContainsKey(r.TestId)).Select(x => ToDto(x, testsDict[x.TestId]?.Name ?? ""));
            return requestDtos;
        }

        public static ClientTestRequestDto ToDto(TestRequest entity, string testName)
        {
            var dto = entity.Adapt<ClientTestRequestDto>();
            dto.TestName = testName;
            return dto;
        }
    }
}
