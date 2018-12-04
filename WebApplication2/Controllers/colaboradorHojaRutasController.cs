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
    public class colaboradorHojaRutasController : Controller
    {
        private dimacodevEntities1 db = new dimacodevEntities1();

        // GET: colaboradorHojaRutas
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int id = Convert.ToInt32(TempData["id"]);
                TempData["id"] = id;
                var colaboradorHojaRuta = db.colaboradorHojaRuta.Include(c => c.colaborador).Include(c => c.hojaRuta).Where(c => c.idHojaRuta == id);
                return View(colaboradorHojaRuta.ToList());
            }
        }

        // GET: colaboradorHojaRutas/Details/5
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
                colaboradorHojaRuta colaboradorHojaRuta = db.colaboradorHojaRuta.Find(id);
                if (colaboradorHojaRuta == null)
                {
                    return HttpNotFound();
                }
                return View(colaboradorHojaRuta);
            }
        }
        // GET: colaboradorHojaRutas/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                ViewBag.run = db.colaborador.Where(p => p.activo == true)
           .Select(p => new SelectListItem
           {
               Text = p.nombre + " " + p.apellidoPaterno + " " + p.apellidoMaterno,
               Value = p.run.ToString()
           });
                int id = Convert.ToInt32(TempData["id"]);
                TempData["id"] = id;
                return View();
            }
        }
        public ActionResult Siguiente()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int id = Convert.ToInt32(TempData["id"]);
                TempData["id"] = id;
                return RedirectToAction("Index", "guias");
            }
        }
        public JsonResult GetCHRList()
        {
            
                int id = Convert.ToInt32(TempData["id"]);
                TempData["id"] = id;
                List<ColaboradorHojaRutaViewModel> chrList = db.colaboradorHojaRuta.Where(x => x.idHojaRuta == id).Select(x => new ColaboradorHojaRutaViewModel
                {
                    idColHojaRuta = x.idColHojaRuta,
                    idHojaRuta = x.idHojaRuta,
                    rut = x.colaborador.rut,
                    nombre = x.colaborador.nombre,
                    apellidoPaterno = x.colaborador.apellidoPaterno,
                    apellidoMaterno = x.colaborador.apellidoMaterno,
                    cargo = x.colaborador.cargo


                }).ToList();
                return Json(chrList, JsonRequestBehavior.AllowGet);
            
        }
    

    public JsonResult GuardarChoferInDB(ColaboradorHojaRutaViewModel model)
    {
        
            var result = false;
            try
            {
                int id = Convert.ToInt32(TempData["id"]);
                TempData["id"] = id;
                colaboradorHojaRuta col = new colaboradorHojaRuta();
                col.idHojaRuta = id;
                col.run = model.run;

                db.colaboradorHojaRuta.Add(col);
                db.SaveChanges();
                result = true;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        
    }
        


        /*
                // POST: colaboradorHojaRutas/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult AddChofer([Bind(Include = "idColHojaRuta, run")] colaboradorHojaRuta colaboradorHojaRuta)
                {
                    int id = Convert.ToInt32(TempData["id"]);
                    TempData["id"] = id;
                    if (ModelState.IsValid)
                    {
                        if (colaboradorHojaRuta.colaborador.cargo == "Chofer")
                        {
                            colaboradorHojaRuta.idHojaRuta = id;
                            db.colaboradorHojaRuta.Add(colaboradorHojaRuta);
                            db.SaveChanges();
                            return RedirectToAction("Create");

                        }
                        else
                        {
                            colaboradorHojaRuta.idHojaRuta = id;
                            db.colaboradorHojaRuta.Add(colaboradorHojaRuta);
                            db.SaveChanges();
                            return RedirectToAction("Create");
                        }
                    }

                    ViewBag.run = new SelectList(db.colaborador, "run", "rut", colaboradorHojaRuta.run);
                    ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", colaboradorHojaRuta.idHojaRuta);
                    return View(colaboradorHojaRuta);
                }
                */
        // GET: colaboradorHojaRutas/Edit/5
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
                colaboradorHojaRuta colaboradorHojaRuta = db.colaboradorHojaRuta.Find(id);
                if (colaboradorHojaRuta == null)
                {
                    return HttpNotFound();
                }
                ViewBag.run = new SelectList(db.colaborador, "run", "rut", colaboradorHojaRuta.run);
                ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", colaboradorHojaRuta.idHojaRuta);
                return View(colaboradorHojaRuta);
            }
        }

        // POST: colaboradorHojaRutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idColHojaRuta,idHojaRuta,run")] colaboradorHojaRuta colaboradorHojaRuta)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(colaboradorHojaRuta).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.run = new SelectList(db.colaborador, "run", "rut", colaboradorHojaRuta.run);
                ViewBag.idHojaRuta = new SelectList(db.hojaRuta, "idHojaRuta", "patente", colaboradorHojaRuta.idHojaRuta);
                return View(colaboradorHojaRuta);
            }
        }

        // GET: colaboradorHojaRutas/Delete/5
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
                colaboradorHojaRuta colaboradorHojaRuta = db.colaboradorHojaRuta.Find(id);
                if (colaboradorHojaRuta == null)
                {
                    return HttpNotFound();
                }
                return View(colaboradorHojaRuta);
            }
        }
        // POST: colaboradorHojaRutas/Delete/5
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
                colaboradorHojaRuta colaboradorHojaRuta = db.colaboradorHojaRuta.Find(id);
                db.colaboradorHojaRuta.Remove(colaboradorHojaRuta);
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
