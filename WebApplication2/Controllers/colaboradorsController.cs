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
    public class colaboradorsController : Controller
    {
        private dimacodevEntities db = new dimacodevEntities();

        // GET: colaboradors
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View(db.colaborador.ToList());
            }
        }

        // GET: colaboradors/Details/5
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
                colaborador colaborador = db.colaborador.Find(id);
                if (colaborador == null)
                {
                    return HttpNotFound();
                }
                return View(colaborador);
            }
        }

        // GET: colaboradors/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
            {
                ViewBag.Msg = "Sesion Invalida";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        // POST: colaboradors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "run,rut,nombre,apellidoPaterno,apellidoMaterno,edad,cargo,telefono,valorHoraExtra")] colaborador colaborador)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    colaborador.activo = true;
                    db.colaborador.Add(colaborador);
                    db.SaveChanges();
                    TempData["alerta"] = "Agregar colaborador";
                    return RedirectToAction("Index");
                }

                return View(colaborador);
            }
        }

        // GET: colaboradors/Edit/5
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
                colaborador colaborador = db.colaborador.Find(id);
                if (colaborador == null)
                {
                    return HttpNotFound();
                }
                return View(colaborador);
            }
        }

        // POST: colaboradors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "run,rut,nombre,apellidoPaterno,apellidoMaterno,edad,cargo,telefono,valorHoraExtra,activo")] colaborador colaborador)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(colaborador).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["alerta"] = "Editar colaborador";
                    return RedirectToAction("Index");
                }
                return View(colaborador);
            }
        }

        // GET: colaboradors/Delete/5
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
                colaborador colaborador = db.colaborador.Find(id);
                if (colaborador == null)
                {
                    return HttpNotFound();
                }
                return View(colaborador);
            }
        }

        // POST: colaboradors/Delete/5
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
                colaborador colaborador = db.colaborador.Find(id);
                db.colaborador.Remove(colaborador);
                db.SaveChanges();
                TempData["alerta"] = "Borrar colaborador";
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
