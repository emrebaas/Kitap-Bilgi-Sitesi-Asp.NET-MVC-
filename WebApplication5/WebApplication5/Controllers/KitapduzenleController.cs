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
    public class KitapduzenleController : Controller
    {
        private Veritabani db = new Veritabani();

        // GET: Kitapduzenle
        public ActionResult Index()
        {
            var kitaps = db.Kitaps.Include(k => k.Kategori).Include(k => k.Kullanici);
            return View(kitaps.ToList());
        }

        // GET: Kitapduzenle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitaps.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View(kitap);
        }

        // GET: Kitapduzenle/Create
        public ActionResult Create()
        {
            ViewBag.kitap_kategori_id = new SelectList(db.Kategoris, "kategori_id", "kategori_adi");
            ViewBag.kitap_kul_id = new SelectList(db.Kullanicis, "kul_id", "kullanici_adi");
            return View();
        }

        // POST: Kitapduzenle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kitap_id,Kitap_resim,kitap_adi,kitap_yazar,kitap_yayinevi,kitap_ozet,kitap_icerik,kitap_kategori_id,kitap_kul_id")] Kitap kitap , HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {


                if (image != null)
                {
                    //Sunucuya dosya kaydedilirken, sunucunun dosya sistemini, yolunu bilemeyeceğimiz için
                    //Server.MapPath() ile sitemizin ana dizinine gelmiş oluruz. Devamında da sitemizdeki
                    //yolu tanımlarız.
                    image.SaveAs(Server.MapPath("~/Content/img/") + image.FileName);
                    kitap.Kitap_resim = "/Content/img/" + image.FileName;
                }

                string kuladi= Session["username"].ToString();

                var kullanici = db.Kullanicis.Where(i => i.kullanici_adi == kuladi).SingleOrDefault();

                /* burada kullanıcı idsini çekiyoruz*/
                kitap.kitap_kul_id = kullanici.kul_id;

                db.Kitaps.Add(kitap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kitap_kategori_id = new SelectList(db.Kategoris, "kategori_id", "kategori_adi", kitap.kitap_kategori_id);
            ViewBag.kitap_kul_id = new SelectList(db.Kullanicis, "kul_id", "kullanici_adi", kitap.kitap_kul_id);
            return View(kitap);
        }

        // GET: Kitapduzenle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitaps.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            ViewBag.kitap_kategori_id = new SelectList(db.Kategoris, "kategori_id", "kategori_adi", kitap.kitap_kategori_id);
            ViewBag.kitap_kul_id = new SelectList(db.Kullanicis, "kul_id", "kullanici_adi", kitap.kitap_kul_id);
            return View(kitap);
        }

        // POST: Kitapduzenle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kitap_id,Kitap_resim,kitap_adi,kitap_yazar,kitap_yayinevi,kitap_ozet,kitap_icerik,kitap_kategori_id,kitap_kul_id")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kitap_kategori_id = new SelectList(db.Kategoris, "kategori_id", "kategori_adi", kitap.kitap_kategori_id);
            ViewBag.kitap_kul_id = new SelectList(db.Kullanicis, "kul_id", "kullanici_adi", kitap.kitap_kul_id);
            return View(kitap);
        }

        // GET: Kitapduzenle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitaps.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View(kitap);
        }

        // POST: Kitapduzenle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitap kitap = db.Kitaps.Find(id);
            db.Kitaps.Remove(kitap);
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
