using EUBAD_ActivityPlan.Interfaces;
using EUBAD_ActivityPlan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberRepositoryManager _teamMemberRepo;


        public TeamMemberController(ITeamMemberRepositoryManager teamMemberRepo)
        {
            _teamMemberRepo = teamMemberRepo;
        }

        public async Task<ViewResult> List()
        {
            ViewData["Title"] = "Team Members";
            return View(await _teamMemberRepo.GetMembersAsync());
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add a Team Member";
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: "FirstName, LastName, Email, Password, IsActive")] TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                await _teamMemberRepo.AddTeamMember(teamMember);
                return RedirectToAction("List");
            }
            return View(teamMember);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var teamMember = _teamMemberRepo.GetTeamMemberById(id);
            await _teamMemberRepo.DeleteTeamMember(teamMember);
            return RedirectToAction("List");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit a Team Member";
            var teamMember = _teamMemberRepo.GetTeamMemberById(id);
            return View(teamMember);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                await _teamMemberRepo.EditTeamMember(teamMember);
                return RedirectToAction("List");
            }
            return View(teamMember);
        }
    }
}