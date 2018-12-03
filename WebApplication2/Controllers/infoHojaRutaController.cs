﻿using System;
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
        private dimacodevEntities db = new dimacodevEntities();
        // GET: infoHojaRuta
        public ActionResult Index(int? id)
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