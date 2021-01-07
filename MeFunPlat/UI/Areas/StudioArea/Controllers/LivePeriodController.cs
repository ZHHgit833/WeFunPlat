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
    public class LivePeriodController : Controller
    {
        // GET: StudioArea/LivePeriod
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<LivePeriod> uslist = new List<LivePeriod>();
            using (MyContext context = new MyContext())
            {
                uslist = context.LivePeriods.ToList();
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
        public ActionResult AddAnnouncer(LivePeriod info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.LivePeriods.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }

        public ActionResult DelAnnouncer(LivePeriod info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                LivePeriod aninfo = db.LivePeriods.Where(x => x.RoomID == info.RoomID).FirstOrDefault();
                db.LivePeriods.Remove(aninfo);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
        public ActionResult Edit(int? id)
        {
            LivePeriod info = new LivePeriod();
            using (MyContext db = new MyContext())
            {
                info = db.LivePeriods.Where(x => x.LPID == id).FirstOrDefault();
            }
            return View(info);
        }
        public ActionResult EditAnnouncer(LivePeriod info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                LivePeriod uinfo = db.LivePeriods.Where(x => x.LPID == info.LPID).FirstOrDefault();
                uinfo.RoomID = info.RoomID;
                uinfo.TimeLong = info.TimeLong;
                uinfo.TimeBegin = info.TimeBegin;
                uinfo.TimeEnd = info.TimeEnd;
                uinfo.Remake = info.Remake;               
                int i = db.SaveChanges();
                if (i > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
    }
}