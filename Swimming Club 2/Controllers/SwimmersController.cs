using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swimming_Club_2.Models;
using Swimming_Club_2.Services;
using System;
using System.Collections.Generic;

namespace Swimming_Club_2.Controllers
{
    [Authorize(Roles ="Admin, Coach")]
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

        [HttpGet]
        public IActionResult SearchByName()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchByName(string LastName)
        {
            var result = _dataService.SearchByLastName(LastName);
            return View("Index", result);
        }

        [HttpGet]

        // GET: SwimmersController/Edit/5
        public IActionResult EditSwimmer(int id)
        {
           var result = _dataService.GetOne(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult EditSwimmer(Swimmer swimmer)
        {
            var updatedSwimmer = _dataService.Update(swimmer);
            List<Discipline> disciplines = _dataService.GetDisciplinesById(updatedSwimmer.Id);
            updatedSwimmer.Disciplines = disciplines;

            return View("Details", updatedSwimmer);
        }
       
        public IActionResult Delete(int id)
        {            
            var x = _dataService.Delete(id);
            return View("Index", _dataService.GetAll());
        }

        public IActionResult Details(int id)
        {            
            var swimmer = _dataService.GetOne(id);
            List<Discipline> disciplines = _dataService.GetDisciplinesById(id);
            swimmer.Disciplines = disciplines;
            return View(swimmer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SwimmersController/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Swimmer swimmer)
        {           
            var result = _dataService.Insert(swimmer);
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
