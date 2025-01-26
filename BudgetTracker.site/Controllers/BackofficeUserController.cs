using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;

namespace YourSiteProject.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // Get all members
        [HttpGet]
        public IActionResult GetAllMembers()
        {
            var members = _memberService.GetAll(0, int.MaxValue, out var totalMembers);
            if (!members.Any())
            {
                return NotFound("No members found.");
            }

            var memberDtos = members.Select(member => new
            {
                MemberId = member.Id,
                Username = member.Username,
                Email = member.Email,
                Name = member.Name,
            });

            return Ok(memberDtos);
        }

        // Get a single member by username
        [HttpGet("{username}")]
        public IActionResult GetMemberByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username is required.");
            }

            var member = _memberService.GetByUsername(username);
            if (member == null)
            {
                return NotFound($"Member with username '{username}' not found.");
            }

            return Ok(new
            {
                MemberId = member.Id,
                Username = member.Username,
                Email = member.Email,
                Name = member.Name,
            });
        }
    }
}
