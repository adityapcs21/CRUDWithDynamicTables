using CRUDWithDynamicTable.Entities;
using CRUDWithDynamicTable.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUDWithDynamicTable.Repository
{
    public class UserDetailsRepository:IUserDetails
    {


        public IEnumerable<UserDetails> GetUserDetails()
        {
            using (var dbContext = new MainEntities())
            {
                //List<UserDetail> userDetails = dbContext.UserDetails.Where(x=> x.IsActive== true).ToList();
                List<UserDetail> userDetails = dbContext.UserDetails.ToList();
                List<CarDetail> carDetails = dbContext.CarDetails.ToList();
                List<UserDetails> userViewModels = new List<UserDetails>();
                foreach (var user in userDetails)
                {
                    var data = new UserDetails
                    {

                        UserId = user.UserId,
                        FullName = user.FullName,
                        UserEmail = user.UserEmail,
                        CivilIdNumber = user.CivilIdNumber,
                    };

                    var cardetails = string.Join(",", carDetails.Where(x => x.UserId == user.UserId).Select(y => y.CarLicense).ToList());
                    data.CarLicense = cardetails;

                    userViewModels.Add(data);
                }

                var c = userViewModels.Where(X => X.CarLicense != "").ToList();
                return c;
            }



        }

        public void GetInsertDetail(UserDetails insert)
        {
            using (var dbContext = new MainEntities())
            {

                //string FileName = Path.GetFileNameWithoutExtension(insert.ImageFile.FileName);
                //string extension = Path.GetExtension(insert.ImageFile.FileName);
                //FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + extension;
                //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                //insert.ProfilePic = UploadPath + FileName;
                //insert.ImageFile.SaveAs(insert.ProfilePic);

                //string fileName = Path.GetFileNameWithoutExtension(insert.ImageFile.FileName);
                //string extension = Path.GetExtension(insert.ImageFile.FileName);
                //fileName = fileName + DateTime.Now.ToString("yymmssfff") + "-" + fileName.Trim() + extension;
                //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                //insert.ProfilePic = UploadPath + fileName;
                //insert.ImageFile.SaveAs(insert.ProfilePic);

                var user = new UserDetail()
                {
                    FullName = insert.FullName,
                    UserEmail = insert.UserEmail,
                    PasswordHash = insert.PasswordHash,
                    CivilIdNumber = insert.CivilIdNumber,
                    DOB = insert.DOB,
                    MobileNo = insert.MobileNo,
                    Address = insert.Address,
                    //RoleId = insert.RoleId,
                    ProfilePic = insert.ProfilePic,
                    CreatedDate = insert.CreatedDate,
                    ModifiedDate = insert.ModifiedDate,
                    IsNotificationActive = insert.IsNotificationActive,
                    IsActive = insert.IsActive,
                    DeviceId = insert.DeviceId,
                    DeviceType = insert.DeviceType,
                    FcmToken = insert.FcmToken,
                    verify = insert.verify,
                    VerifiedBy = insert.VerifiedBy,
                    Area = insert.Area,
                    Block = insert.Block,
                    Street = insert.Street,
                    Housing = insert.Housing,
                    Floor = insert.Floor,
                    NewPass = insert.NewPass,
                    ConPass = insert.ConPass,
                    Jadda = insert.Jadda,
                    Reason = insert.Reason,
                    ActivatedBy = insert.ActivatedBy,
                    ActivatedDate = insert.ActivatedDate


                };

                var car = new CarDetail()
                {
                    CarLicense = insert.CarLicense,
                    UserId = insert.UserId

                };
                dbContext.UserDetails.Add(user);
                dbContext.CarDetails.Add(car);
                dbContext.SaveChanges();
            }
        }


        public void GetDeleteDetail(int? id)
        {
            using (var dbContext = new MainEntities())
            {
                var user = dbContext.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
                var car = dbContext.CarDetails.Where(x => x.UserId == id).ToList();
                user.IsActive = false;
                dbContext.Entry(user).State = EntityState.Modified;
                if (car.Count() > 0)
                {
                    dbContext.CarDetails.RemoveRange(car)
;
                }
                dbContext.SaveChanges();
            }


        }


        public EditViewModel GetEditDetails(int Id)
        {
            using (var us = new MainEntities())
            {
                var viewModel = us.UserDetails.Where(x => x.UserId == Id).FirstOrDefault();
                var cardetails = us.CarDetails.Where(x => x.UserId == Id).Select(y => new CarDetailsInfo
                {
                    Id = y.Id,
                    CarLicense = y.CarLicense,

                }).ToList();

                var userDetails = new EditViewModel
                {
                    UserId = viewModel.UserId,
                    UserEmail = viewModel.UserEmail,
                    FullName = viewModel.FullName,
                    CivilIdNumber = viewModel.CivilIdNumber


                };


                userDetails.CarDetails.AddRange(cardetails);


                return userDetails;

            }


        }

        public void EditUserDetails(UserDetails insert)
        {
            using (var dbContext = new MainEntities())
            {


                var dtls = dbContext.UserDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();
                //var newcardetail = dbContext.CarDetails.Where(x => x.Id == insert.UserId);

                dtls.UserId = insert.UserId;
                dtls.FullName = insert.FullName;
                dtls.UserEmail = insert.UserEmail;
                dtls.Address = insert.Address;
                dtls.PasswordHash = insert.PasswordHash;
                dtls.CivilIdNumber = insert.CivilIdNumber;



                var cars = dbContext.CarDetails.Where(x => x.UserId == insert.UserId).SingleOrDefault();
                cars.CarLicense = insert.CarLicense;
                dbContext.Entry(dtls).State = EntityState.Modified;
                //if (cars.Count() > 0)
                //{
                dbContext.Entry(cars).State = EntityState.Modified;
                //}


                dbContext.SaveChanges();



            }


        }

    }
}