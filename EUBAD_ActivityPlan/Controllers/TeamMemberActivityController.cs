using EUBAD_ActivityPlan.Interfaces;
using EUBAD_ActivityPlan.Models;
using EUBAD_ActivityPlan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EUBAD_ActivityPlan.Controllers
{
    public class TeamMemberActivityController : Controller
    {
        private readonly ITeamMemberActivityRepositoryManager _teamMemberActivityPlanRepo;
        private readonly ITeamMemberRepositoryManager _teamMemberRepo;
        private readonly IActivityRepositoryManager _activityRepo;
        private ILogger<TeamMemberActivityController> _logger;
        public ActivityCalendarViewModel activityCalendar = new ActivityCalendarViewModel();

        public TeamMemberActivityController(ITeamMemberActivityRepositoryManager teamMemberActivityPlanRepo, ITeamMemberRepositoryManager teamMemberRepo, IActivityRepositoryManager activityRepo, ILogger<TeamMemberActivityController> logger)
        {
            _teamMemberActivityPlanRepo = teamMemberActivityPlanRepo;
            _teamMemberRepo = teamMemberRepo;
            _activityRepo = activityRepo;
            _logger = logger;
        }

        public async Task<ViewResult> List(string startdateString, string enddateString)
        {
            ViewData["Title"] = "Activity Calendar";

            DateTime selDateS;
            DateTime selDateE;

            bool startOk = DateTime.TryParse(startdateString, out selDateS);
            bool endOk = DateTime.TryParse(enddateString, out selDateE);

            if (!startOk)
            {
                ViewData["ErrorMessage"] = "Start Date format is not correct. Displaying all Activities until " + selDateE.ToShortDateString();
                _logger.LogWarning("Unable to convert {0} in Date format. Start Date not available", startdateString);
                selDateS = DateTime.MinValue;
            }
            else
                _logger.LogInformation("Converted successfully '{0}' to '{1}'", startdateString, selDateS);
            if (!endOk)
            {
                ViewData["ErrorMessage"] = "End Date format is not correct. Displaying all Activities from " + selDateS.ToShortDateString();
                _logger.LogWarning("Unable to convert {0} in Date format. End Date not available", enddateString);
                selDateE = DateTime.MaxValue;
            }
            else
                _logger.LogInformation("Converted successfully '{0}' to '{1}'", enddateString, selDateE);
            if (!(startOk || endOk))
            {
                ViewData["ErrorMessage"] = "Date format is not correct. Displaying All Activities";
                _logger.LogWarning("Date format for both start and end is not correct.");
            }

            return View(await _teamMemberActivityPlanRepo.GetTeamMemberActivitiesByDate(selDateS, selDateE));
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add an Activity for a Team Member";
            activityCalendar.TeamMembers = _teamMemberRepo.GetAllMembers();
            activityCalendar.Activities = _activityRepo.GetAllActivities();
            return View(activityCalendar);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int selectedteammemberId, int selectedactivityId, DateTime date)
        {
            TeamMemberActivity teamMemberActivity = new TeamMemberActivity
            {
                TeamMemberId = selectedteammemberId,
                TeamMember = _teamMemberRepo.GetTeamMemberById(selectedteammemberId),
                ActivityId = selectedactivityId,
                Activity = _activityRepo.GetActivityById(selectedactivityId),
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
            activityCalendar.TeamMembers = _teamMemberRepo.GetAllMembers();
            activityCalendar.Activities = _activityRepo.GetAllActivities();
            activityCalendar.Date = teamMemberActivity.Day;
            activityCalendar.SelectedActivityId = teamMemberActivity.ActivityId;
            activityCalendar.SelectedTeamMemberId = teamMemberActivity.TeamMemberId;
            return View(activityCalendar);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int selectedteammemberId, int selectedactivityId, DateTime date, int id)
        {
            TeamMemberActivity teamMemberActivity = new TeamMemberActivity
            {
                Id = id,
                TeamMemberId = selectedteammemberId,
                TeamMember = _teamMemberRepo.GetTeamMemberById(selectedteammemberId),
                ActivityId = selectedactivityId,
                Activity = _activityRepo.GetActivityById(selectedactivityId),
                Day = date
            };
            if (ModelState.IsValid)
            {
                await _teamMemberActivityPlanRepo.EditTeamMemberActivity(teamMemberActivity);
                return RedirectToAction("List");
            }
            return View(teamMemberActivity);
        }
        /*private IEnumerable<SelectListItem> GetTeamMembersAsSelectListItems() => _teamMemberRepo.GetAllMembers().Where(teamMember => teamMember.IsActive).Select(teamMember => new SelectListItem
        {
            Value = teamMember.Id.ToString(),
            Text = teamMember.FirstName + " " + teamMember.LastName
        });
        private IEnumerable<SelectListItem> GetActivitiesAsSelectListItems() => _activityRepo.GetAllActivities().Where(activity => activity.IsActive).Select(activity => new SelectListItem
        {
            Value = activity.Id.ToString(),
            Text = activity.Name
        });*/
    }
}
