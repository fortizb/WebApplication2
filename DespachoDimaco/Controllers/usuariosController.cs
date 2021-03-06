﻿using System;
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
    public class usuariosController : Controller
    {
        private dimacodevEntities db = new dimacodevEntities();

        // GET: usuarios
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View(db.usuario.ToList());
            }
        }

        // GET: usuarios/Details/5
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
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuarioID,usuarioNombre,usuarioClave,usuarioActivo,usuarioNombreCol,usuarioApellido,usuarioTelefono,usuarioCargo")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.usuarioActivo = true;
                db.usuario.Add(usuario);
                db.SaveChanges();
                TempData["alerta"] = "Agregar usuario";
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: usuarios/Edit/5
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
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // POST: usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usuarioID,usuarioNombre,usuarioClave,usuarioNombreCol,usuarioApellido,usuarioTelefono,usuarioCargo")] usuario usuario)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["alerta"] = "Editar usuario";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
        }

        // GET: usuarios/Delete/5
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
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // POST: usuarios/Delete/5
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
                usuario usuario = db.usuario.Find(id);
                db.usuario.Remove(usuario);
                db.SaveChanges();
                TempData["alerta"] = "Borrar usuario";
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
