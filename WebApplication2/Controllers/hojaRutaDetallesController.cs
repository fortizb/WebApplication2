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
    public class hojaRutaDetallesController : Controller
    {
        private dimacodevEntities1 db = new dimacodevEntities1();

        // GET: hojaRutaDetalles
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var hojaRutaDetalle = db.hojaRutaDetalle.Include(h => h.hojaRuta);
                return View(hojaRutaDetalle.ToList());
            }
        }

        // GET: hojaRutaDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                hojaRutaDetalle hojaRutaDetalle = db.hojaRutaDetalle.Find(id);
                if (hojaRutaDetalle == null)
                {
                    return HttpNotFound();
                }
                return View(hojaRutaDetalle);
            }
        }

        // GET: hojaRutaDetalles/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente");
                return View();
            }
        }

        // POST: hojaRutaDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHojaRutaDetalle,idHojaRuta,fechaSalida,fechaLlegada,observaciones")] hojaRutaDetalle hojaRutaDetalle)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.hojaRutaDetalle.Add(hojaRutaDetalle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", hojaRutaDetalle.idHojaRuta);
                return View(hojaRutaDetalle);
            }
        }

        // GET: hojaRutaDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                hojaRutaDetalle hojaRutaDetalle = db.hojaRutaDetalle.Find(id);
                if (hojaRutaDetalle == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", hojaRutaDetalle.idHojaRuta);
                return View(hojaRutaDetalle);
            }
        }

        // POST: hojaRutaDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHojaRutaDetalle,idHojaRuta,fechaSalida,fechaLlegada,observaciones")] hojaRutaDetalle hojaRutaDetalle)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hojaRutaDetalle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", hojaRutaDetalle.idHojaRuta);
                return View(hojaRutaDetalle);
            }
        }

        // GET: hojaRutaDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                hojaRutaDetalle hojaRutaDetalle = db.hojaRutaDetalle.Find(id);
                if (hojaRutaDetalle == null)
                {
                    return HttpNotFound();
                }
                return View(hojaRutaDetalle);
            }
        }

        // POST: hojaRutaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                hojaRutaDetalle hojaRutaDetalle = db.hojaRutaDetalle.Find(id);
                db.hojaRutaDetalle.Remove(hojaRutaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
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
