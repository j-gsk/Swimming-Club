using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swimming_Club_2.Models;
using Swimming_Club_2.Services;
using System;
using System.Collections.Generic;

namespace Swimming_Club_2.Controllers
{
    //[Authorize]
    public class SwimmersController : Controller
    {
        private readonly ISwimmerDataService _dataService;

        public SwimmersController(ISwimmerDataService swimmersDAO)
        {
            _dataService = swimmersDAO;
        }       

        public ActionResult Index()
        {
            List<Swimmer> swimmers = new List<Swimmer>();
            swimmers.AddRange(_dataService.GetAll());
            return View("Index", swimmers);
        }

        public ActionResult SearchByName()
        {
            return View("SearchForm");
        }
        public ActionResult ProcessSearch(string LastName)
        {
            var result = _dataService.SearchByLastName(LastName);
            return View("Index", result);
        }

        // GET: SwimmersController/Edit/5
        public ActionResult EditForm(int id)
        {
           var result = _dataService.GetOne(id);
            return View(result);
        }

        public ActionResult ProcessEdit(Swimmer swimmer)
        {
            var updatedSwimmer = _dataService.Update(swimmer);
            List<Discipline> disciplines = _dataService.GetPRsBySwimmerId(updatedSwimmer.Id);
            updatedSwimmer.Disciplines = disciplines;


            return View("Details", updatedSwimmer);
        }

        public ActionResult Delete(int id)
        {
            var dao = new SwimmersDAO();
            var x = dao.Delete(id);
            return View("Index", dao.GetAll());
        }

        public ActionResult Details(int id)
        {            
            var swimmer = _dataService.GetOne(id);
            List<Discipline> disciplines = _dataService.GetPRsBySwimmerId(id);
            swimmer.Disciplines = disciplines;
            return View(swimmer);
        }

        public ActionResult CreateForm()
        {
            return View();
        }

        // POST: SwimmersController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Swimmer swimmer)
        {
            var dao = new SwimmersDAO();
            var result = dao.Insert(swimmer);
            return View("Details", swimmer);
        }        
    }
}
