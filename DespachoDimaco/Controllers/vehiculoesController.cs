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
    public class vehiculoesController : Controller
    {
        private dimacodevEntities db = new dimacodevEntities();

        // GET: vehiculoes
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View(db.vehiculo.Where(x => x.activo == true).ToList());
            }
        }

        // GET: vehiculoes/Details/5
        public ActionResult Details(string id)
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
                vehiculo vehiculo = db.vehiculo.Find(id);
                if (vehiculo == null)
                {
                    return HttpNotFound();
                }
                return View(vehiculo);
            }
        }

        // GET: vehiculoes/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        // POST: vehiculoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patente,descripcion,marca,modelo,color,velocidadPromedio,rendimiento,capacidadCarga")] vehiculo vehiculo)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    vehiculo.activo = true;
                    if(vehiculo.descripcion == null)
                    {
                        vehiculo.descripcion = "Sin descripción";
                    }
                    db.vehiculo.Add(vehiculo);
                    db.SaveChanges();
                    TempData["alerta"] = "Agregar vehiculo";
                    return RedirectToAction("Index");
                }

                return View(vehiculo);
            }
        }
        // GET: vehiculoes/Edit/5
        public ActionResult Edit(string id)
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
                vehiculo vehiculo = db.vehiculo.Find(id);
                if (vehiculo == null)
                {
                    return HttpNotFound();
                }
                return View(vehiculo);
            }
        }
        // POST: vehiculoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patente,descripcion,marca,modelo,color,velocidadPromedio,rendimiento,capacidadCarga")] vehiculo vehiculo)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vehiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["alerta"] = "Editar vehiculo";
                    return RedirectToAction("Index");
                }
                return View(vehiculo);
            }
        }
        // GET: vehiculoes/Delete/5
        public ActionResult Delete(string id)
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
                vehiculo vehiculo = db.vehiculo.Find(id);
                if (vehiculo == null)
                {
                    return HttpNotFound();
                }
                return View(vehiculo);
            }
        }

        // POST: vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                vehiculo vehiculo = db.vehiculo.Find(id);
                vehiculo.activo = false;
                db.SaveChanges();
                TempData["alerta"] = "Borrar vehiculo";
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
