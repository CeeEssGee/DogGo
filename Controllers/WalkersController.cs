using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {

        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(
            IWalkerRepository walkerRepository, 
            IWalkRepository walkRepository, 
            IOwnerRepository ownerRepository, 
            IDogRepository dogRepository)

        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
        }


        // GET: Walkers
        
        public ActionResult Index()
        {

            List<Walker> walkers = _walkerRepo.GetAllWalkers();

            if (User.Identity.IsAuthenticated)
            {
                int userId = GetCurrentUserId();
                Owner owner = _ownerRepo.GetOwnerById(userId);
                List<Walker> neighborhoodWalkers = walkers.Where(walker => walker.NeighborhoodId == owner.NeighborhoodId).ToList();
                return View(neighborhoodWalkers);
            }
            else 

            return View(walkers);
        }

        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }
            List<Walk> matchingWalks = _walkRepo.GetWalkByWalkerId(id);

            WalkerProfileViewModel vm = new WalkerProfileViewModel
            {
                Walker = walker,
                Walks = matchingWalks,
            };
            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Get the id of current logged in user
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }


    }
}
