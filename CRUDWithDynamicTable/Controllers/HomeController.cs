using CRUDWithDynamicTable.Entities;
using CRUDWithDynamicTable.EntityFramework;
using CRUDWithDynamicTable.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace CRUDWithDynamicTable.Controllers
{
    public class HomeController : Controller
    {
        IUserDetails userDetails = new UserDetailsRepository();
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var udetails = userDetails.GetUserDetails();
            return Json(new { data = udetails }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult InsertuS(UserDetails insert, string ProfilePic)
        {
            // var t = ProfilePic.Substring(22);  // remove data:image/png;base64,
            string t = ProfilePic.Substring(ProfilePic.IndexOf(',') + 1);


            byte[] bytes = Convert.FromBase64String(t);

            Image image;
            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                image = Image.FromStream(ms, true);
            }
            var randomFileName = Guid.NewGuid().ToString().Substring(0, 4) + ".png";
            var fullPath = Path.Combine(Server.MapPath("~/Images/"), randomFileName);
            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
            insert.ProfilePic = randomFileName;
            if (ModelState.IsValid)
            {
                userDetails.GetInsertDetail(insert);
            }
            return Json(new { data = true });
            
        }

        public ActionResult Delete(int? id)
        {
            userDetails.GetDeleteDetail(id);

            return RedirectToAction("Index");
        }



        //public JsonResult Update(Users user)
        //{
        //    var data = Mul.User.FirstOrDefault(x => x.Id == user.Id);
        //    if (data != null)
        //    {
        //        data.Name = user.Name;
        //        data.State = user.State;
        //        data.Country = user.Country;
        //        data.Age = user.Age;
        //        MultiplesEntities.SaveChanges();
        //    }
        //    return Json(JsonRequestBehavior.AllowGet);
        //}



        public ActionResult Edit(int Id)
        {
            var user = userDetails.GetEditDetails(Id);
            //CarDetails = user.CarDetails
            return Json(new { UserId = user.UserId, FullName = user.FullName, UserEmail = user.UserEmail, CivilIdNumber = user.CivilIdNumber, CarDetails = user.CarDetails }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public ActionResult EdituS(UserDetails insert)
        {

            userDetails.EditUserDetails(insert);
            return Json(new { data = true });
        }

    }
}
