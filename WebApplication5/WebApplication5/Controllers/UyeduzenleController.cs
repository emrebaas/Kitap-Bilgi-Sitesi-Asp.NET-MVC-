using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class UyeduzenleController : Controller
    {
        private Veritabani db = new Veritabani();

        // GET: Uyeduzenle
        public ActionResult Index()
        {
            var kullanicis = db.Kullanicis.Include(k => k.Yetki);
            return View(kullanicis.ToList());
        }

        // GET: Uyeduzenle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = db.Kullanicis.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // GET: Uyeduzenle/Create
        public ActionResult Create()
        {
            ViewBag.kul_yetki = new SelectList(db.Yetkis, "yetki_id", "yetki_adi");
            return View();
        }

        // POST: Uyeduzenle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kul_id,kullanici_adi,kul_sifre,kul_isim,kul_soyisim,kul_mail,kul_yetki")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Kullanicis.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kul_yetki = new SelectList(db.Yetkis, "yetki_id", "yetki_adi", kullanici.kul_yetki);
            return View(kullanici);
        }

        // GET: Uyeduzenle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = db.Kullanicis.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            ViewBag.kul_yetki = new SelectList(db.Yetkis, "yetki_id", "yetki_adi", kullanici.kul_yetki);
            return View(kullanici);
        }

        // POST: Uyeduzenle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kul_id,kullanici_adi,kul_sifre,kul_isim,kul_soyisim,kul_mail,kul_yetki")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullanici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kul_yetki = new SelectList(db.Yetkis, "yetki_id", "yetki_adi", kullanici.kul_yetki);
            return View(kullanici);
        }

        // GET: Uyeduzenle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = db.Kullanicis.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // POST: Uyeduzenle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanici kullanici = db.Kullanicis.Find(id);
            db.Kullanicis.Remove(kullanici);
            db.SaveChanges();
            return RedirectToAction("Index");
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
