using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swimming_Club_2.Models;
using Swimming_Club_2.Services;

namespace Swimming_Club_2.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: AdministrationController
        public ActionResult Index()
        {
            var dao = new AdministrationDAO();
            var roles = dao.GetAll();
            return View(roles);
        }
        public ActionResult CreateForm()
        {
            return View();
        }

        // POST: SwimmersController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            var dao = new AdministrationDAO();
            var result = dao.Insert(role);
            return View("Index", dao.GetAll());
        }

        public ActionResult EditForm(string id)
        {
            var dao = new AdministrationDAO();
            var result = dao.FindById(id);
            return View(result);
        }

        public ActionResult ProcessEdit(Role role)
        {
            var dao = new AdministrationDAO();
            var result = dao.Update(role);            
            return View("Index", dao.GetAll());
        }

        // GET: SwimmersController/Delete/5
        public ActionResult Delete(string id)
        {
            var dao = new AdministrationDAO();
            var x = dao.Delete(id);
            return View("Index", dao.GetAll());
        }        
    }     
}
    
