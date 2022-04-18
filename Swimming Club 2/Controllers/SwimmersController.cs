using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swimming_Club_2.Models;
using Swimming_Club_2.Services;
using System;
using System.Collections.Generic;

namespace Swimming_Club_2.Controllers
{
    [Authorize]
    public class SwimmersController : Controller
    {
        private readonly ISwimmerDataService _dataService;

        public SwimmersController(ISwimmerDataService swimmersDAO)
        {
            _dataService = swimmersDAO;
        }       

        public IActionResult Index()
        {
            List<Swimmer> swimmers = new List<Swimmer>();
            swimmers.AddRange(_dataService.GetAll());
            return View("Index", swimmers);
        }

        public IActionResult SearchByName()
        {
            return View("SearchForm");
        }
        public IActionResult ProcessSearch(string LastName)
        {
            var result = _dataService.SearchByLastName(LastName);
            return View("Index", result);
        }

        // GET: SwimmersController/Edit/5
        public IActionResult EditForm(int id)
        {
           var result = _dataService.GetOne(id);
            return View(result);
        }

        public IActionResult ProcessEdit(Swimmer swimmer)
        {
            var updatedSwimmer = _dataService.Update(swimmer);
            List<Discipline> disciplines = _dataService.GetPRsBySwimmerId(updatedSwimmer.Id);
            updatedSwimmer.Disciplines = disciplines;


            return View("Details", updatedSwimmer);
        }

        public IActionResult Delete(int id)
        {
            var dao = new SwimmersDAO();
            var x = dao.Delete(id);
            return View("Index", dao.GetAll());
        }

        public IActionResult Details(int id)
        {            
            var swimmer = _dataService.GetOne(id);
            List<Discipline> disciplines = _dataService.GetPRsBySwimmerId(id);
            swimmer.Disciplines = disciplines;
            ViewBag.SwimmerId = swimmer.Id;
            return View(swimmer);
        }
        
        public IActionResult CreateForm()
        {
            return View();
        }

        // POST: SwimmersController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Swimmer swimmer)
        {
            var dao = new SwimmersDAO();
            var result = dao.Insert(swimmer);
            return View("Details", swimmer);
        }
        [HttpGet]
        public IActionResult CreateDiscipline(int swimmerId)
        {            
            return View();
        }
        [HttpPost]
        public IActionResult CreateDiscipline(Discipline discipline)
        {
            Swimmer swimmer = _dataService.GetOne(discipline.SwimmerId);
            swimmer.Disciplines.Add(discipline);

            return View("Details", swimmer);
        }
    }
}
