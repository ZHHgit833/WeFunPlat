using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;
using UI.Filter;
using Newtonsoft.Json;

namespace UI.Areas.SystemArea.Controllers
{
    public class UserManagerController : Controller
    {

        // GET: SystemArea/UserManager
        //[LoginFilter]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<Users> uslist = new List<Users>();
            using (MyContext context = new MyContext())
            {
                uslist = context.Users.ToList();
            }
            var data = new
            {
                count = uslist.Count(),//数据总条数，用于分页
                code = 0,//code码是必须要的， 0 表述返回的数据没有问题
                data = uslist,//数据源
                msg = "a"//描述
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            Users info = new Users();
            using (MyContext db = new MyContext())
            {
                info = db.Users.Where(x => x.ID == id).FirstOrDefault();
            }
            return View(info);
        }

        public ActionResult AddUser(Users info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.Users.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult EditUser(Users info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Users usinfo = db.Users.Where(x=>x.ID == info.ID).FirstOrDefault();
                usinfo.UserName = info.UserName;
                usinfo.UserPwd = info.UserPwd;
                usinfo.NickName = info.NickName;
                usinfo.State = info.State;
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult DelUser(Users info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Users uinfo = db.Users.Where(x => x.ID == info.ID).FirstOrDefault();
                db.Users.Remove(uinfo);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
    }
}