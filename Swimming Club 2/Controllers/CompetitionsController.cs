using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Swimming_Club_2.Controllers
{
    public class CompetitionsController : Controller
    {
        // GET: CompetitionsController
        public ActionResult Index()
        {

            return View();
        }

        // GET: CompetitionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompetitionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionsController/Create
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

        // GET: CompetitionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompetitionsController/Edit/5
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

        // GET: CompetitionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionsController/Delete/5
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
    }
}
