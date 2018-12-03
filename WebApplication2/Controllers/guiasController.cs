using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class guiasController : Controller
    {
        private dimacodevEntities db = new dimacodevEntities();

        // GET: guias
        public ActionResult Index()
        {
            int id = Convert.ToInt32(TempData["id"]);
            TempData["id"] = id;
            var guias = db.guias.Where(x => x.idHojaRuta == 2 && x.estado == "Pendiente").Include(g => g.hojaRuta);
            return View(guias.ToList());
        }


        // GET: guias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            guias guias = db.guias.Find(id);
            if (guias == null)
            {
                return HttpNotFound();
            }
            return View(guias);
        }
        public ActionResult Siguiente()
        {
            int id = Convert.ToInt32(TempData["id"]);
            TempData["id"] = id;
            return RedirectToAction("Index", "guias");
        }

        // GET: guias/Create
        public ActionResult Create()
        {
            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente");
            return View();
        }

        // POST: guias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numeroGuia,rut,nombre,direccion,telefono,ciudad,observacion")] guias guias)
        {
            if (ModelState.IsValid)
            {
                guias.idHojaRuta = 2;
                guias.fechaIngreso = DateTime.Now;
                guias.estado = "Pendiente";
                db.guias.Add(guias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", guias.idHojaRuta);
            return View(guias);
        }

        // GET: guias/Edit/5
        public ActionResult AddGuiaHR(int? idNumeroGuias)
        {
            if (idNumeroGuias == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            guias guias = db.guias.Find(idNumeroGuias);
            if (guias == null)
            {
                return HttpNotFound();
            }
           

            int id = Convert.ToInt32(TempData["id"]);
            guias.idHojaRuta = id;
            db.SaveChanges();
            TempData["id"] = id;
            return RedirectToAction("Index");
         
        
        }

        // POST: guias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHojaRuta")] guias guias)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(TempData["id"]);
                guias.idHojaRuta = id;
                db.Entry(guias).State = EntityState.Modified;
                db.SaveChanges();
                TempData["id"] = id;
                return RedirectToAction("Index");

            }
            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", guias.idHojaRuta);
            return View(guias);
        }

        // GET: guias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            guias guias = db.guias.Find(id);
            if (guias == null)
            {
                return HttpNotFound();
            }
            return View(guias);
        }

        // POST: guias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            guias guias = db.guias.Find(id);
            db.guias.Remove(guias);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
