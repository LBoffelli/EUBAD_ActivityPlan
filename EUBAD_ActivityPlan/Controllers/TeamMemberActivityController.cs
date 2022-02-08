using EUBAD_ActivityPlan.Interfaces;
using EUBAD_ActivityPlan.Models;
using EUBAD_ActivityPlan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EUBAD_ActivityPlan.Controllers
{
    public class TeamMemberActivityController : Controller
    {
        private readonly ITeamMemberActivityRepositoryManager _teamMemberActivityPlanRepo;
        private readonly ITeamMemberRepositoryManager _teamMemberRepo;
        private readonly IActivityRepositoryManager _activityRepo;
        public ActivityCalendarViewModel activityCalendar = new ActivityCalendarViewModel();

        public TeamMemberActivityController(ITeamMemberActivityRepositoryManager teamMemberActivityPlanRepo, ITeamMemberRepositoryManager teamMemberRepo, IActivityRepositoryManager activityRepo)
        {
            _teamMemberActivityPlanRepo = teamMemberActivityPlanRepo;
            _teamMemberRepo = teamMemberRepo;
            _activityRepo = activityRepo;
        }

        public ViewResult List(string startdateString, string enddateString)
        {
            ViewData["Title"] = "Activity Calendar";

            DateTime selDateS;
            DateTime selDateE;

            bool startOk = DateTime.TryParse(startdateString, out selDateS);
            bool endOk = DateTime.TryParse(enddateString, out selDateE);

            if (!startOk)
            {
                ViewData["ErrorMessage"] = "Start Date format is not correct. Displaying all Activities until " + selDateE.ToShortDateString();
                Console.WriteLine("Unable to convert {0} in date", startdateString);
                selDateS = DateTime.MinValue;
            }
            else
                Console.WriteLine("Converted '{0}' to '{1}'", startdateString, selDateS);
            if (!endOk)
            {
                ViewData["ErrorMessage"] = "End Date format is not correct. Displaying all Activities from " + selDateS.ToShortDateString();
                Console.WriteLine("Unable to convert {0} in date", enddateString);
                selDateE = DateTime.MaxValue;
            }
            else
                Console.WriteLine("Converted '{0}' to '{1}'", enddateString, selDateE);
            if (!(startOk || endOk))
            {
                ViewData["ErrorMessage"] = "Date format is not correct. Displaying All Activities";
                Console.WriteLine("Unable to convert {0} and {1} in date", startdateString, enddateString);
            }

            return View(_teamMemberActivityPlanRepo.GetTeamMemberActivitiesByDate(selDateS, selDateE));
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add an Activity for a Team Member";
            activityCalendar.TeamMembers = _teamMemberRepo.GetSelectListItems();
            activityCalendar.Activities = _activityRepo.GetSelectListItems();
            return View(activityCalendar);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int teammemberId, int activityId, DateTime date)
        {
            TeamMemberActivity teamMemberActivity = new TeamMemberActivity
            {
                TeamMemberId = teammemberId,
                TeamMember = _teamMemberRepo.GetTeamMemberById(teammemberId),
                ActivityId = activityId,
                Activity = _activityRepo.GetActivityById(activityId),
                Day = date
            };
            if (ModelState.IsValid)
            {
                await _teamMemberActivityPlanRepo.AddTeamMemberActivity(teamMemberActivity);
                return RedirectToAction("List");
            }
            return View(teamMemberActivity);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var teamMemberActivity = _teamMemberActivityPlanRepo.GetTeamMemberActivityById(id);
            await _teamMemberActivityPlanRepo.DeleteTeamMemberActivity(teamMemberActivity);
            return RedirectToAction("List");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit a Team Member Activity";
            var teamMemberActivity = _teamMemberActivityPlanRepo.GetTeamMemberActivityById(id);
            activityCalendar.TeamMembers = _teamMemberRepo.GetSelectListItems();
            activityCalendar.Activities = _activityRepo.GetSelectListItems();
            activityCalendar.Date = teamMemberActivity.Day;
            activityCalendar.ActivityId = teamMemberActivity.ActivityId;
            activityCalendar.TeamMemberId = teamMemberActivity.TeamMemberId;
            return View(activityCalendar);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int teammemberId, int activityId, DateTime date, int id)
        {
            TeamMemberActivity teamMemberActivity = new TeamMemberActivity
            {
                Id = id,
                TeamMemberId = teammemberId,
                TeamMember = _teamMemberRepo.GetTeamMemberById(teammemberId),
                ActivityId = activityId,
                Activity = _activityRepo.GetActivityById(activityId),
                Day = date
            };
            if (ModelState.IsValid)
            {
                await _teamMemberActivityPlanRepo.EditTeamMemberActivity(teamMemberActivity);
                return RedirectToAction("List");
            }
            return View(teamMemberActivity);
        }
    }
}
