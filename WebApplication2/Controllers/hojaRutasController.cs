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
    public class hojaRutasController : Controller
    {
        private dimacodevEntities1 db = new dimacodevEntities1();

        // GET: hojaRutas
      
        public ActionResult Index()
        {

            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var hojaRuta = db.hojaRuta.Where(h => h.idHojaRuta > 2).Include(h => h.vehiculo).Include(h => h.vehiculo1);
                return View(hojaRuta.ToList());
                
            }
        }
        // GET: hojaRutas/Details/5
        public ActionResult AddDatos(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hojaRuta hojaRuta = db.hojaRuta.Find(id);
            if (hojaRuta == null)
            {
                return HttpNotFound();
            }
            TempData["id"] = id;
            return RedirectToAction("Create", "colaboradorHojaRutas");
        }

        // GET: hojaRutas/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                ViewBag.patente = new SelectList(db.vehiculo, "patente", "patente");
                return View();
            }
        }

        // POST: hojaRutas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHojaRuta,patente,fechaIngreso,estado")] hojaRuta hojaRuta)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            else
            {
                if (ModelState.IsValid)
                {
                    hojaRuta.fechaCreacion = DateTime.Now;
                    hojaRuta.estado = "1";
                    db.hojaRuta.Add(hojaRuta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.patente = new SelectList(db.vehiculo, "patente", "descripcion", hojaRuta.patente);
                ViewBag.patente = new SelectList(db.vehiculo, "patente", "descripcion", hojaRuta.patente);
                return View(hojaRuta);
            }
        }
            // GET: hojaRutas/Edit/5
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
                hojaRuta hojaRuta = db.hojaRuta.Find(id);
                if (hojaRuta == null)
                {
                    return HttpNotFound();
                }
      
                ViewBag.patente = new SelectList(db.vehiculo, "patente", "descripcion", hojaRuta.patente);
                ViewBag.patente = new SelectList(db.vehiculo, "patente", "descripcion", hojaRuta.patente);
                return View(hojaRuta);
             }
         }

        // POST: hojaRutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHojaRuta,patente,fechaCreacion,fechaModificacion,estado")] hojaRuta hojaRuta)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hojaRuta).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.patente = new SelectList(db.vehiculo, "patente", "descripcion", hojaRuta.patente);
                ViewBag.patente = new SelectList(db.vehiculo, "patente", "descripcion", hojaRuta.patente);
                return View(hojaRuta);
            }
        }

        // GET: hojaRutas/Delete/5
        public ActionResult AddPeoneta(int? id)
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
                hojaRuta hojaRuta = db.hojaRuta.Find(id);
                if (hojaRuta == null)
                {
                    return HttpNotFound();
                }
                TempData["id"] = id;
                TempData["cargo"] = "Peoneta";
                return RedirectToAction("Index", "colaboradorHojaRutas");
            }
        }
        public ActionResult AddChofer(int? id)
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
                hojaRuta hojaRuta = db.hojaRuta.Find(id);
                if (hojaRuta == null)
                {
                    return HttpNotFound();
                }
                TempData["id"] = id;
                TempData["cargo"] = "Chofer";
                return RedirectToAction("Index", "colaboradorHojaRutas");
            }
        }


    }
}
