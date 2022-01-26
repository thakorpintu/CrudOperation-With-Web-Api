using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CrudOperation_With_WebApi.Models;
using CrudOperation_With_WebApi.Entity;
using System.Web.Http;
using System.Web;
using System.IO;

namespace CrudOperation_With_WebApi.WebApi
{
    public class ClientController : ApiController
    {
        UserDbContext db = new UserDbContext();

        [Route("api/Client/save")]
        [HttpPost]
        public int save()
        {

            UserMaster model = new UserMaster();
            var UserDetails = HttpContext.Current.Request;



            if (Convert.ToInt32(UserDetails["Id"])>0)
            {
                model.Id = Convert.ToInt32(UserDetails["Id"]);
                model.Name = Convert.ToString(UserDetails["Name"]);
                model.Email = Convert.ToString(UserDetails["Email"]);
                model.Password = Convert.ToString(UserDetails["Password"]);

                if (UserDetails.Files.Count > 0)
                {
                    foreach (string item in UserDetails.Files)
                    {
                        var postedfile = UserDetails.Files[item];
                        var ext = Path.GetExtension(item);
                        var Length = postedfile.ContentLength;
                        if (ext.ToLower() == ".jpg" || ext.ToLower() == ".png" || ext.ToLower() == ".jpeg")
                        {
                            if (Length <= 1000000)
                            {
                                postedfile.SaveAs(HttpContext.Current.Server.MapPath("~/Image/" + postedfile.FileName));
                                model.Image = "~/Image/" + UserDetails.Files[item].FileName;
                            }
                            else
                            {
                                return 4;
                            }

                        }
                        else
                        {
                            return 3;
                        }

                    }
                }
                else
                {
                    model.Image = Convert.ToString(UserDetails["Image"]);

                }
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
            else
            {
                model.Name = Convert.ToString(UserDetails["Name"].ToString());
                model.Email = Convert.ToString(UserDetails["Email"].ToString());
                model.Password = Convert.ToString(UserDetails["Password"].ToString());

                foreach (string item in UserDetails.Files)
                {
                    var postedfile = UserDetails.Files[item];
                    var ext =  new FileInfo(postedfile.FileName).Extension;
                    var Length = postedfile.ContentLength;

                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".png" || ext.ToLower() == ".jpeg")
                    {
                        if(Length<=1000000)
                        {
                            postedfile.SaveAs(HttpContext.Current.Server.MapPath("~/Image/" + postedfile.FileName));
                            model.Image = "~/Image/" + UserDetails.Files[item].FileName;
                        }
                        else
                        {
                            return 4;
                        }
                      
                    }
                    else
                    {
                        return 3;
                    }
                  
                }


                db.usermaster.Add(model);
                return db.SaveChanges();
            }


        }
        [Route("api/Client/Index")]
        [HttpGet]
        public List<UserMaster> Index()
        {
            return db.usermaster.ToList();
        }
        [Route("api/Client/Edit")]
        [HttpPost]
        public UserMaster Edit(UserMaster model)
        {
            UserMaster md = new UserMaster();
            if (model.Id > 0)
            {
                return db.usermaster.Where(m => m.Id == model.Id).FirstOrDefault();
            }
            return md;
        }
        [Route("api/Client/Delete")]
        [HttpPost]
        public int Delete(UserMaster model)
        {
            UserMaster md = new UserMaster();
            if (model.Id > 0)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
            return 0;
        }

    }
}