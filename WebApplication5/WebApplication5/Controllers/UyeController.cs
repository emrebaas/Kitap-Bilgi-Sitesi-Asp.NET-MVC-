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
    public class UyeController : Controller
    {
        private Veritabani db = new Veritabani();

        // GET: Uye
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Uye/Details/5
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

        // GET: Uye/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Uye/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kul_id,kullanici_adi,kul_sifre,kul_isim,kul_soyisim,kul_mail,kul_yetki")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                kullanici.kul_yetki=1;
                db.Kullanicis.Add(kullanici);
                Session["username"] = kullanici.kullanici_adi;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            
            return View(kullanici);
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index","Home");
        }


        // GET: Uye/Edit/5
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

        // POST: Uye/Edit/5
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

        // GET: Uye/Delete/5
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

        // POST: Uye/Delete/5
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



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici model)
        {
            try
            {
                var varmı = db.Kullanicis.Where(i => i.kullanici_adi == model.kullanici_adi).SingleOrDefault();
                if (varmı == null)
                {
                    return View();
                }

                if (varmı.kul_yetki == 2)
                {
                    Session["username"] = model.kullanici_adi;
                    Session["kullaniciid"] = model.kul_id;
                    return RedirectToAction("Index", "Admin");
                }

                if (varmı.kul_sifre == model.kul_sifre)
                {
                    Session["username"] = model.kullanici_adi;
                    Session["kullaniciid"] = model.kul_id;
                    return RedirectToAction("Index", "KayitliAnasayfa");
                }
                else
                {
                    return View();
                }
            }

            catch
            {
                return View();
            }

        }


    }
}
