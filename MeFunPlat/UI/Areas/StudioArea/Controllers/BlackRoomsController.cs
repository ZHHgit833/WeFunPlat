using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;
using UI.Filter;
using Newtonsoft.Json;

namespace UI.Areas.StudioArea.Controllers
{
    public class BlackRoomsController : Controller
    {
        // GET: StudioArea/BlackRooms
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<BlackRoom> uslist = new List<BlackRoom>();
            using (MyContext context = new MyContext())
            {
                uslist = context.BlackRoom.Where(x=>x.State==0).ToList();
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
        public ActionResult Edit(int? id)
        {
            BlackRoom info = new BlackRoom();
            using (MyContext db = new MyContext())
            {
                info = db.BlackRoom.Where(x => x.BRID == id).FirstOrDefault();
            }
            return View(info);
        }
        public ActionResult EditUser(BlackRoom info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                BlackRoom usinfo = db.BlackRoom.Where(x => x.BRID == info.BRID).FirstOrDefault();
                usinfo.ID = info.ID;
                usinfo.Remake = info.Remake;
                usinfo.BeginDate = info.BeginDate;
                usinfo.EndDate = info.EndDate;
                usinfo.State = info.State;
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