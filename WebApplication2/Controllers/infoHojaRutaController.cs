using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class infoHojaRutaController : Controller
    {
        private dimacodevEntities1 db = new dimacodevEntities1();
        // GET: infoHojaRuta
        public ActionResult Index(int? id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var model = new ResumenHojaRutaViewModel
                {
                    guiasID = db.guias.ToList().Where(g => g.idHojaRuta == id),
                    colaboradorHojaRutaID = db.colaboradorHojaRuta.ToList().Where(ch => ch.idHojaRuta == id)
                    //colaboradorID = db.colaborador.ToList()
                };
                return View(model);
            }
        }
    }
}