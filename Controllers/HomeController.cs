using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;


namespace Project.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Database1Entities3 db = new Database1Entities3();

        public ActionResult index()
        {
            //Sidebar
            var result = db.Products.Select(m => m.Brand).Distinct();
            ViewBag.Brands = result;

            return View(db.Products.ToList());
        }

        public ActionResult UserProfile()
        {
            try
            {
                if (Session["Username"] != null)
                {
                    return View(db.Users.Find(Session["Username"]));
                }
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }      
        }

        public ActionResult EditProfile()
        {
            try
            {
                if (Session["Username"] != null)
                {
                    User p = db.Users.Find(Session["Username"]);
                    ViewBag.ImagePath = p.ImagePath;
                    return View(db.Users.Find(Session["Username"]));
                }
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult signup()
        {
            try
            {
                if (Session["Username"] != null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                return View();
            } 
        }

        public ActionResult ResetPassword(string Username)
        {
            return View();
        }

        public ActionResult checkout()
        {
            return View();
        }

        public ActionResult contactus()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult blog()
        {
            //Sidebar
            var result = db.Products.Select(m => m.Brand).Distinct();
            ViewBag.Brands = result;

            return View();
        }

        public ActionResult blogSingle()
        {
            //Sidebar
            var result = db.Products.Select(m => m.Brand).Distinct();
            ViewBag.Brands = result;

            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                if (Session["Username"] != null)
                {
                    this.Session["Username"] = null;
                    this.Session["Firstname"] = null;
                    this.Session["Lastname"] = null;
                    this.Session["Password"] = null;
                    this.Session["Email"] = null;
                    this.Session["Secret_Question"] = null;
                    this.Session["Answer"] = null;
                    this.Session["ImagePath"] = null;

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

        public ActionResult productdetails()
        {
            //Sidebar
            var result = db.Products.Select(m => m.Brand).Distinct();
            ViewBag.Brands = result;

            string id = Request["Id"];
            return View(db.Products.ToList());
        }

        public ActionResult Men()
        {
            var result = db.Products.Where(m => m.Category == "MEN").Select(m => m.Brand).Distinct();
            ViewBag.Brands = result;
            var result2 = db.Products.Where(m => m.Category == "MEN").Select(m => m.Color).Distinct();
            ViewBag.Colors = result2;

            String Brand = Request["Brand"];
            String Color = Request["Color"];

            if (Brand == "" || Brand == null)
            {
                if (Color == "" || Color == null)
                {
                    return View(db.Products.Where(m => m.Category == "MEN").ToList());
                }
                else
                {
                    var SelectiveProduct = db.Products.Where(m => m.Color == Color && m.Category == "MEN").ToList();
                    return View(SelectiveProduct);
                }                  
            }
            else
            {
                var SelectiveProduct = db.Products.Where(m => m.Brand == Brand && m.Category == "MEN").ToList();
                return View(SelectiveProduct);
            }
        }

        public ActionResult Women()
        {
            var result = db.Products.Where(m => m.Category == "WOMEN").Select(m => m.Brand).Distinct();
            ViewBag.Brands = result;
            var result2 = db.Products.Where(m => m.Category == "WOMEN").Select(m => m.Color).Distinct();
            ViewBag.Colors = result2;

            String Brand = Request["Brand"];
            String Color = Request["Color"];

            if (Brand == "" || Brand == null)
            {
                if (Color == "" || Color == null)
                {
                    return View(db.Products.Where(m => m.Category == "WOMEN").ToList());
                }
                else
                {
                    var SelectiveProduct = db.Products.Where(m => m.Color == Color && m.Category == "WOMEN").ToList();
                    return View(SelectiveProduct);
                }
            }
            else
            {
                var SelectiveProduct = db.Products.Where(m => m.Brand == Brand && m.Category == "WOMEN").ToList();
                return View(SelectiveProduct);
            }
        }

        public ActionResult Kids()
        {
            var result1 = db.Products.Where(m => m.Category == "KIDS").Select(m => m.Brand).Distinct();
            ViewBag.Brands = result1;

            var result2 = db.Products.Where(m => m.Category == "KIDS").Select(m => m.Color).Distinct();
            ViewBag.Colors = result2;

            String Brand = Request["Brand"];
            String Color = Request["Color"];

            if (Brand == "" || Brand == null)
            {
                if (Color == "" || Color == null)
                {
                    return View(db.Products.Where(m => m.Category == "KIDS").ToList());
                }
                else
                {
                    var SelectiveProduct = db.Products.Where(m => m.Color == Color && m.Category == "KIDS").ToList();
                    return View(SelectiveProduct);
                }
            }
            else
            {
                var SelectiveProduct = db.Products.Where(m => m.Brand == Brand && m.Category == "KIDS").ToList();
                return View(SelectiveProduct);
            }
        }


        public ActionResult cart()
        {
            try{
                if (this.Session["Username"] != null)
                {
                    List<Product> list = new List<Product>();
                    int noOfProd = Convert.ToInt32(this.Session["NoOfProduct"]);
                    for (int i = 1; i <= noOfProd; i++)
                    {
                        String prodVar = "Product" + i.ToString();
                        Product p = db.Products.Find(this.Session[prodVar]);

                        String prodQuantity = "ProductQuantity" + i.ToString();
                        int quantity = Convert.ToInt32(this.Session[prodQuantity]);

                        p.Quantity = quantity;
                        list.Add(p);
                    }
                    return View(list);
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("login", "Home");
            }

        }
      
        [HttpPost]
        public ActionResult Signup(User u)
        {

               HttpPostedFileBase file = Request.Files[0];
               if (file.FileName != "" && file.FileName != null)
               {
                   for (int i = 0; i < Request.Files.Count; i++)
                   {

                       file.SaveAs(Server.MapPath(@"~/UserProfilePicture/" + u.Username + System.IO.Path.GetExtension(file.FileName)));
                       u.ImagePath = "/UserProfilePicture/" + u.Username + System.IO.Path.GetExtension(file.FileName);
                   }
               }
               else
                   u.ImagePath = "/UserProfilePicture/generic.png";

            u.Secret_Question = Request["SecretQuestion"];

            if (ModelState.IsValid)
            {
                db.Users.Add(u);
                db.SaveChanges();            
                return RedirectToAction("Index");
            }
           return View(u);
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            List<User> list = db.Users.ToList();
            foreach (var x in list)
            {
                if (x.Username.Equals(u.Username))
                    if (x.Password.Equals(u.Password))
                    {
                        this.Session["Username"] = x.Username;
                        this.Session["Firstname"] = x.Firstname;
                        this.Session["Lastname"] = x.Lastname;
                        this.Session["Password"] = x.Password;
                        this.Session["Email"] = x.Email;
                        this.Session["Secret_Question"] = x.Secret_Question;
                        this.Session["Answer"] = x.Answer;
                        this.Session["ImagePath"] = x.ImagePath;
                        this.Session["NoOfProduct"] = "0";
                        return RedirectToAction("Index");
                    }
            }
            return View("Login");
        }

        public JsonResult CheckUserName()
        {

            string userName = Request["Username"];
            userName = userName.Trim();
            if (userName == "")
                return this.Json(true, JsonRequestBehavior.AllowGet);
            else
            {
                User u;

                try
                {
                    u = db.Users.First(x => x.Username == userName);
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
        }

        [HttpPost]
        public ActionResult ForgetPassword(User u)
        {
            List<User> list = db.Users.ToList();
            string s_q = Request["SecretQuestion"];
            ViewBag.Result = false; 
            foreach (var x in list)
            {
                if (x.Username.Equals(u.Username))
                    if (x.Secret_Question.Equals(s_q))
                        if (x.Answer.Equals(u.Answer))
                        {
                            ViewBag.Result = true;
                            ViewBag.Firstname = x.Firstname;
                            ViewBag.Lastname = x.Lastname;
                            ViewBag.Email = x.Email;
                            ViewBag.Secret_Question = x.Secret_Question;
                            ViewBag.Answer = x.Answer;
                            ViewBag.ImagePath = x.ImagePath;
                        }
            }
            return View("ForgetPassword");

        }

        [HttpPost]
        public ActionResult ResetPassword(User u)
        {
            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(u);
        }

        public ActionResult ForgetPassword()
        {

            return View("ForgetPassword");
        }

        [HttpPost]
        public ActionResult UpdateProfile(User u)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file.FileName != "" && file.FileName != null)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    file.SaveAs(Server.MapPath(@"~/UserProfilePicture/" + u.Username + System.IO.Path.GetExtension(file.FileName)));
                    u.ImagePath = "/UserProfilePicture/" + u.Username + System.IO.Path.GetExtension(file.FileName);                  
                }
            }

            u.Secret_Question = Request["SecretQuestion"];

            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(u);

        }

        public ActionResult AddProductToCart(int productId, string returnUrl)
        {
            ViewBag.QuantityMessage = true;
            try{
                if (this.Session["Username"] != null)
                {
                    int noOfProd = Convert.ToInt32(this.Session["NoOfProduct"]);
                    String prodVar = null, prodQuantity = null;

                    for (int i = 1; i <= noOfProd; i++)
                    {
                        prodVar = "Product" + i.ToString();
                        if (this.Session[prodVar].ToString() == productId.ToString())
                        {
                            Product p = db.Products.Find(this.Session[prodVar]);

                            prodQuantity = "ProductQuantity" + i.ToString();
                            int quantity = Convert.ToInt32(this.Session[prodQuantity]);
                            if (p.Quantity > quantity)
                                quantity++;
                            else
                                TempData["QuantityMessage"] = false;

                            this.Session[prodQuantity] = quantity;
                            return Redirect(returnUrl);
                        }     
                    }
                    
                    noOfProd++;
                    this.Session["NoOfProduct"] = noOfProd.ToString();
                    prodVar = "Product" + noOfProd.ToString();
                    this.Session[prodVar] = productId;
                    prodQuantity = "ProductQuantity" + noOfProd.ToString();
                    this.Session[prodQuantity] = "1";
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("login", "Home");
            }

        }

        public ActionResult DecrementQuantity(int productId, string returnUrl)
        {
            try
            {
                if (this.Session["Username"] != null)
                {
                    int noOfProd = Convert.ToInt32(this.Session["NoOfProduct"]);
                    String prodVar = null, prodQuantity = null;

                    for (int i = 1; i <= noOfProd; i++)
                    {
                        prodVar = "Product" + i.ToString();
                        if (this.Session[prodVar].ToString() == productId.ToString())
                        {
                            prodQuantity = "ProductQuantity" + i.ToString();
                            int quantity = Convert.ToInt32(this.Session[prodQuantity]);
                            quantity--;
                            this.Session[prodQuantity] = quantity;
                            return Redirect(returnUrl);
                        }
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("login", "Home");
            }

        }

        public ActionResult IncrementQuantity(int productId, string returnUrl)
        {
            ViewBag.QuantityMessage = "True";
            try
            {
                if (this.Session["Username"] != null)
                {
                    int noOfProd = Convert.ToInt32(this.Session["NoOfProduct"]);
                    String prodVar = null, prodQuantity = null;

                    for (int i = 1; i <= noOfProd; i++)
                    {
                        prodVar = "Product" + i.ToString();
                        if (this.Session[prodVar].ToString() == productId.ToString())
                        {
                            Product p = db.Products.Find(this.Session[prodVar]);

                            prodQuantity = "ProductQuantity" + i.ToString();
                            int quantity = Convert.ToInt32(this.Session[prodQuantity]);
                            if (p.Quantity > quantity)
                                quantity++;
                            else
                                TempData["QuantityMessage"] = "False";

                            this.Session[prodQuantity] = quantity;
                            return Redirect(returnUrl);
                        }
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("login", "Home");
            }

        }

        public ActionResult DeleteFromCart(int productId, string returnUrl)
        {
            try
            {
                if (this.Session["Username"] != null)
                {
                    int noOfProd = Convert.ToInt32(this.Session["NoOfProduct"]);
                    String prodVar = null, prodQuantity = null;

                    for (int i = 1; i <= noOfProd; i++)
                    {
                        prodVar = "Product" + i.ToString();
                        if (this.Session[prodVar].ToString() == productId.ToString())
                        {
                            String LastProduct = "Product" + noOfProd.ToString();
                            String LastProductQuantity = "ProductQuantity" + noOfProd.ToString();

                            int lastQuantity = Convert.ToInt32(this.Session[LastProductQuantity]);
                            int lastId = Convert.ToInt32(this.Session[LastProduct]);
                                 
                            prodQuantity = "ProductQuantity" + i.ToString();
                            this.Session[prodQuantity] = lastQuantity;
                            this.Session[prodVar] = lastId;

                            int var1 = noOfProd - 1;
                            this.Session["NoOfProduct"] = var1;

                            return Redirect(returnUrl);
                        }
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("login", "Home");
            }

        }
    }
}