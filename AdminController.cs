using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        Database1Entities3 db = new Database1Entities3();

        public ActionResult Index()
        {
            try
            {
                if (Session["Username"] != null)
                    return RedirectToAction("Error" , "Home");
                else
                    return View();
            }
            catch (Exception e)
            {
                return View();
            }
                
        }

        public ActionResult Logout()
        {
            try
            {
                if (Session["AdminUsername"] != null)
                {
                    this.Session["AdminUsername"] = null;
                    this.Session["AdminPassword"] = null;

                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }            
        }

        public ActionResult AddProduct()
        {
            try
            {
                if (Session["AdminUsername"] != null)
                    return View();
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ViewAll()
        {
            try
            {
                if (Session["AdminUsername"] != null)
                    return View(db.Products.ToList());
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (Session["AdminUsername"] != null)
                    return View(db.Products.Find(id));
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }           
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (Session["AdminUsername"] != null)
                {
                    Product p = db.Products.Find(id);
                    ViewBag.ImagePath = p.ImagePath;
                    return View(db.Products.Find(id));
                }
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }

        [HttpPost]
        public ActionResult Login(Admin u)
        {
            List<Admin> list = db.Admins.ToList();
            foreach (var x in list)
            {
                if (x.Username.Equals(u.Username))
                    if (x.Password.Equals(u.Password))
                    {
                        this.Session["AdminUsername"] = x.Username;
                        this.Session["AdminPassword"] = x.Password;
                        return RedirectToAction("ViewAll");
                    }
            }
            return View("index");
        }

        [HttpPost]
        public ActionResult AddProduct(Product u)
        {
            HttpPostedFileBase file = Request.Files[0];
               if (file.FileName != "" && file.FileName != null)
               {
                   for (int i = 0; i < Request.Files.Count; i++)
                   {

                       file.SaveAs(Server.MapPath(@"~/ProductImages/" + u.Id + System.IO.Path.GetExtension(file.FileName)));
                       u.ImagePath = "/ProductImages/" + u.Id + System.IO.Path.GetExtension(file.FileName); ;
                   }
               }
               else
                   u.ImagePath = "/ProductImages/generic.png";

            u.Category = Request["Category"];
            u.Type = Request["Type"];

            if (ModelState.IsValid)
            {
                db.Products.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(u);

        }

        [HttpPost]
        public ActionResult Edit(Product u)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file.FileName != "" && file.FileName != null)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {

                    file.SaveAs(Server.MapPath(@"~/ProductImages/" + u.Id + System.IO.Path.GetExtension(file.FileName)));
                    u.ImagePath = "/ProductImages/" + u.Id + System.IO.Path.GetExtension(file.FileName); ;
                }
            } 

            u.Category = Request["Category"];
            u.Type = Request["Type"];

            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;               
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            return View(u);

        }

        public ActionResult DeleteConfirm(int id)
        {
            Product u = db.Products.Find(id);
            db.Products.Remove(u);
            db.SaveChanges();

            return RedirectToAction("ViewAll");
        }

        public JsonResult CheckProductCode()
        {

            string ID = Request["Id"];            
            if (ID.Trim() != "")
            {
                try
                {
                    int Id = Convert.ToInt32(ID);
                    Product u;

                    try
                    {
                        u = db.Products.First(x => x.Id == Id);
                    }
                    catch (Exception e)
                    {
                        u = null;
                    }
                    if (u == null)
                        return this.Json(false, JsonRequestBehavior.AllowGet);
                    else
                        return this.Json(true, JsonRequestBehavior.AllowGet);
                }
                catch(Exception e)
                {
                    return this.Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
