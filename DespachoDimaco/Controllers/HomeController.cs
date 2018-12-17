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

        
        private dimacodevEntities db = new dimacodevEntities();
        public static class CustomRoles
        {
            public const string Administrador = "Administrador";
            public const string usuario = "Usuario";
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Inicio()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioViewModel user)
        {
            if (ModelState.IsValid)
            {
                using (dimacodevEntities db = new dimacodevEntities())
                {
                    var obj = db.usuario.Where(a => a.usuarioNombre.Equals(user.usuarioNombre) && a.usuarioClave.Equals(user.usuarioClave)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Login"] = obj.usuarioCargo.ToString();
                        Session["usuarioID"] = obj.usuarioID.ToString();
                        Session["usuarioNombre"] = obj.usuarioNombre.ToString();
                        Session["usuarioNombreCol"] = obj.usuarioNombreCol.ToString();
                        Session["usuarioApellido"] = obj.usuarioApellido.ToString();
                        DateTime date = DateTime.Now.AddDays(-7);
                        DateTime month = DateTime.Now.AddMonths(-1);
                        List<costosHojaRuta> list = db.costosHojaRuta.Where(x => x.fecha <= DateTime.Now && x.fecha > date).ToList();
                        List<costosHojaRuta> listMensual = db.costosHojaRuta.Where(x => x.fecha <=DateTime.Now && x.fecha > month).ToList();
                        List<costosHojaRuta> listTotal = db.costosHojaRuta.ToList();
                        int valorSemanal = list.Sum(x => x.monto);
                        int valorMensual = listMensual.Sum(x => x.monto);
                        int valorTotal = listTotal.Sum(x => x.monto);
                        Session["valorSemanal"] = valorSemanal;
                        Session["valorTotal"] = valorTotal;
                        Session["valorMensual"] = valorMensual;
                        return View("Inicio");
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
        public ActionResult GetData()
        {
            
            int pendiente = db.guias.Where(x => x.estado == "Pendiente").ToList().Count;
            int Entregada = db.guias.Where(x => x.estado == "Entregada").ToList().Count;
            int total = db.guias.ToList().Count;
            Ratio obj = new Ratio();
            obj.Pendiente = pendiente;
            obj.Entregada = Entregada;


            return Json(obj,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataBarChart()
        {
            DateTime enero = new DateTime(2018, 01, 31);
            DateTime febrero = new DateTime(2018, 02, 28);
            DateTime marzo = new DateTime(2018, 03, 31);
            DateTime abril = new DateTime(2018, 04, 30);
            DateTime mayo = new DateTime(2018, 05, 31);
            DateTime junio = new DateTime(2018, 06, 30);
            DateTime julio = new DateTime(2018, 07, 31);
            DateTime agosto = new DateTime(2018, 08, 31);
            DateTime septiembre = new DateTime(2018, 09, 30);
            DateTime octubre = new DateTime(2018, 10, 31);
            DateTime noviembre = new DateTime(2018, 11, 30);
            DateTime diciembre = new DateTime(2018, 12, 31);
            int Enero = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < enero).ToList().Count;
            int Febrero = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < febrero && x.fechaCreacion > enero).ToList().Count;
            int Marzo = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < marzo && x.fechaCreacion > febrero).ToList().Count;
            int Abril = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < abril && x.fechaCreacion > marzo).ToList().Count;
            int Mayo = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < mayo && x.fechaCreacion > abril).ToList().Count;
            int Junio = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < junio && x.fechaCreacion > mayo).ToList().Count;
            int Julio = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < julio && x.fechaCreacion > junio).ToList().Count;
            int Agosto = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < agosto && x.fechaCreacion > julio).ToList().Count;
            int Septiembre = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < septiembre && x.fechaCreacion > agosto).ToList().Count;
            int Octubre = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < octubre && x.fechaCreacion > septiembre).ToList().Count;
            int Noviembre = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion < noviembre && x.fechaCreacion > octubre).ToList().Count;
            int Diciembre = db.hojaRuta.Where(x => x.estado == false && x.fechaCreacion > noviembre).ToList().Count;
            Ratio obj = new Ratio();
            obj.Enero = Enero;
            obj.Febrero = Febrero;
            obj.Marzo = Marzo;
            obj.Abril = Abril;
            obj.Mayo = Mayo;
            obj.Junio = Junio;
            obj.Julio = Julio;
            obj.Agosto = Agosto;
            obj.Septiembre= Septiembre;
            obj.Octubre = Octubre;
            obj.Noviembre = Noviembre;
            obj.Diciembre = Diciembre;



            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int Pendiente { get; set; }
            public int Entregada { get; set; }
            public int Enero { get; set; }
            public int Febrero { get; set; }
            public int Marzo { get; set; }
            public int Abril { get; set; }
            public int Mayo { get; set; }
            public int Junio { get; set; }
            public int Julio { get; set; }
            public int Agosto { get; set; }
            public int Septiembre { get; set; }
            public int Octubre { get; set; }
            public int Noviembre { get; set; }
            public int Diciembre { get; set; }
            public int Valor { get; set; }
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