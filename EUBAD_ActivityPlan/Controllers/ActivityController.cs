using EUBAD_ActivityPlan.Interfaces;
using EUBAD_ActivityPlan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EUBAD_ActivityPlan.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepositoryManager _activityRepo;

        public ActivityController(IActivityRepositoryManager activityRepo)
        {
            _activityRepo = activityRepo;
        }

        public ViewResult List()
        {
            ViewData["Title"] = "Activities";
            return View(_activityRepo.GetAllActivities());
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add an Activity";
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: "Name, IsActive")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                await _activityRepo.AddActivity(activity);
                return RedirectToAction("List");
            }
            return View(activity);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var activity = _activityRepo.GetActivityById(id);
            if (activity != null)
            {
                await _activityRepo.DeleteActivity(activity);
            }
            return RedirectToAction("List");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit an Activity";
            var activity = _activityRepo.GetActivityById(id);
            return View(activity);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                await _activityRepo.EditActivity(activity);
                return RedirectToAction("List");
            }
            return View(activity);
        }
    }
}
