using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private dimacodevEntities1 db = new dimacodevEntities1();
        public static class CustomRoles
        {
            public const string Administrador = "Administrador";
            public const string usuario = "Usuario";
        }
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(usuario user)
        {
            if (ModelState.IsValid)
            {
                using (dimacodevEntities1 db = new dimacodevEntities1())
                {
                    var obj = db.usuario.Where(a => a.usuarioNombre.Equals(user.usuarioNombre) && a.usuarioClave.Equals(user.usuarioClave)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Login"] = obj.usuarioCargo.ToString();
                        Session["usuarioID"] = obj.usuarioID.ToString();
                        Session["usuarioNombre"] = obj.usuarioNombre.ToString();
                        Session["usuarioNombreCol"] = obj.usuarioNombreCol.ToString();
                        Session["usuarioApellido"] = obj.usuarioApellido.ToString();
                       
                       
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Msg = "Usuario o contraseña Invalidos";
                    }
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            Session["login"] = null;
            Session.Abandon();
            ViewBag.Msg = "Sesion Finalizada";
            return RedirectToAction("Login");
        }

        public ActionResult DashboardUsuario()
        {
            if (Session["usuarioID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult ChartPie()
        {
            var _context = new dimacodevEntities1();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in _context.guias select c);       
            results.ToList().ForEach(ss => xValue.Add(ss.hojaRuta.fechaCreacion));
            results.ToList().ForEach(rs => yValue.Add(rs.hojaRuta.idHojaRuta));

            new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
                .AddTitle("Hojas de Rutas")
                .AddSeries("Default", chartType: "Pie", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }

        //public JsonResult GetGuiaList()
        //{
        //    List<GuiasViewModel> guiaList = db.guias.Where(x => x.estado == "pendiente").Select(x => new GuiasViewModel
        //    {
        //        numeroGuia = x.numeroGuia,
        //        nombre = x.nombre,
        //        rut = x.rut,
        //        ciudad = x.ciudad,
        //        direccion = x.direccion,
        //        telefono = x.telefono,
        //        fechaIngreso = x.fechaIngreso,
        //        observacion = x.observacion
        //    }).ToList();
        //    return Json(guiaList, JsonRequestBehavior.AllowGet);
        //}
    }
}