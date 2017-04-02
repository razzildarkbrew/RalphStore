﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RalphStore.Models;

namespace RalphStore.Controllers
{
    //[Authorize]
    public class ProductsAdminController : Controller
    {
        private Entities db = new Entities();

        // GET: ProductsAdmin
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: ProductsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Price,Active,Inventory,Created,Modified")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //db.Products.Add(product);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                product.Created = DateTime.UtcNow;
                product.Modified = DateTime.UtcNow;
                string fileName = image.FileName;
                if (ConfigurationManager.AppSettings["UseLocalStorage"] == "true")
                {
                    int i = 1;
                    while (System.IO.File.Exists(Server.MapPath("/Content/" + fileName)))
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileName) + i.ToString() +
                                   System.IO.Path.GetExtension(fileName);
                        i++;
                    }
                    image.SaveAs(Server.MapPath("/Content/" + fileName));
                    fileName = "/Content/" + fileName;
                }
                
                 else
                {
                    //Use Blob storage
                    //TODO: blob storage account value is null
                    CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    var blobClient = account.CreateCloudBlobClient();
                    var rootContainer = blobClient.GetRootContainerReference();
                    rootContainer.CreateIfNotExists();
                    rootContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                    var blob = rootContainer.GetBlockBlobReference(fileName);
                    blob.UploadFromStream(image.InputStream);

                    fileName = blob.Uri.ToString();
                }
                if (db.ProductImages.Any(x => x.ProductID == product.ID))
                {
                    ProductImage pi = db.ProductImages.FirstOrDefault(x => x.ProductID == product.ID);
                    pi.Path = fileName;
                    pi.Modified = DateTime.UtcNow;
                }
                else
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        Path = fileName,
                        Created = DateTime.UtcNow,
                        Modified = DateTime.UtcNow
                    });
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Price,Active,Inventory,Created,Modified")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                product.Modified = DateTime.UtcNow;
                db.Entry(product).State = EntityState.Modified;


                //TODO: Need to save a product image here, if an image is supplied
                string fileName = image.FileName;
                if (ConfigurationManager.AppSettings["UseLocalStorage"] == "true")
                {

                    int i = 1;
                    while (System.IO.File.Exists(Server.MapPath("/Content/Images/" + fileName)))
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileName) + i.ToString() + System.IO.Path.GetExtension(fileName);
                        i++;
                    }
                    image.SaveAs(Server.MapPath("/Content/Images/" + fileName));
                    fileName = "/Content/Images/" + fileName;
                }
                else
                {
                    //Use Blob storage
                    CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    var blobClient = account.CreateCloudBlobClient();
                    var rootContainer = blobClient.GetRootContainerReference();
                    rootContainer.CreateIfNotExists();
                    rootContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                    var blob = rootContainer.GetBlockBlobReference(fileName);
                    blob.UploadFromStream(image.InputStream);

                    fileName = blob.Uri.ToString();
                }

                if (db.ProductImages.Any(x => x.ProductID == product.ID))
                {
                    ProductImage pi = db.ProductImages.FirstOrDefault(x => x.ProductID == product.ID);
                    pi.Path = fileName;
                    pi.Modified = DateTime.UtcNow;
                }
                else
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        Path = fileName,
                        Created = DateTime.UtcNow,
                        Modified = DateTime.UtcNow
                    });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
