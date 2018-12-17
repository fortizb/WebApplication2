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
    public class costosHojaRutasController : Controller
    {
        private dimacodevEntities db = new dimacodevEntities();

        // GET: costosHojaRutas
        public ActionResult Index()
        {
            int id = Convert.ToInt32(TempData["id"]);
            TempData["id"] = id;
            var costosHojaRuta = db.costosHojaRuta.Include(c => c.hojaRuta).Where(x => x.idHojaRuta == id);
            return View(costosHojaRuta.ToList());
        }

        // GET: costosHojaRutas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            costosHojaRuta costosHojaRuta = db.costosHojaRuta.Find(id);
            if (costosHojaRuta == null)
            {
                return HttpNotFound();
            }
            return View(costosHojaRuta);
        }

        // GET: costosHojaRutas/Create
        public ActionResult Create()
        {
            int id = Convert.ToInt32(TempData["id"]);
            TempData["id"] = id;
            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente");
            return View();
        }

        // POST: costosHojaRutas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "concepto,numDocumento,proveedor,monto")] costosHojaRuta costosHojaRuta)
        {
            int id = Convert.ToInt32(TempData["id"]);
            TempData["id"] = id;
            if (ModelState.IsValid)
            {
                costosHojaRuta.idHojaRuta = id;
                costosHojaRuta.fecha = DateTime.Now;
                db.costosHojaRuta.Add(costosHojaRuta);
                db.SaveChanges();
                TempData["Alerta"] = "Costo Agregado";
                return RedirectToAction("Index");
            }
            

            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", costosHojaRuta.idHojaRuta);
            return View(costosHojaRuta);
        }

        // GET: costosHojaRutas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            costosHojaRuta costosHojaRuta = db.costosHojaRuta.Find(id);
            if (costosHojaRuta == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", costosHojaRuta.idHojaRuta);
            return View(costosHojaRuta);
        }

        // POST: costosHojaRutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCosto,idHojaRuta,concepto,numDocumento,proveedor,monto,fecha")] costosHojaRuta costosHojaRuta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costosHojaRuta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", costosHojaRuta.idHojaRuta);
            return View(costosHojaRuta);
        }

        // GET: costosHojaRutas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            costosHojaRuta costosHojaRuta = db.costosHojaRuta.Find(id);
            if (costosHojaRuta == null)
            {
                return HttpNotFound();
            }
            return View(costosHojaRuta);
        }

        // POST: costosHojaRutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            costosHojaRuta costosHojaRuta = db.costosHojaRuta.Find(id);
            db.costosHojaRuta.Remove(costosHojaRuta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Finalizar()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int id = Convert.ToInt32(TempData["id"]);
                TempData["id"] = id;
                hojaRuta hojaRuta = db.hojaRuta.Find(id);
                hojaRuta.estado = false;
                hojaRuta.fechaModificacion = DateTime.Now;
                db.SaveChanges();
                TempData["Alerta"] = "Hoja de Ruta Cerrada";
                return RedirectToAction("Index", "hojaRutas");
            }
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
