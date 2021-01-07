using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;
using Newtonsoft.Json;

namespace UI.Areas.LabourArea.Controllers
{
    public class ContractController : Controller
    {
        // GET: LabourArea/Contract
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<Contract> anlist = new List<Contract>();
            using (MyContext context = new MyContext())
            {
                anlist = context.Contract.ToList();
            }
            var data = new
            {
                count = anlist.Count(),//数据总条数，用于分页
                code = 0,//code码是必须要的， 0 表述返回的数据没有问题
                data = anlist,//数据源
                msg = "人员信息"//描述
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddAnnouncer(Contract info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.Contract.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
        public ActionResult DelAnnouncer(Contract info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Contract aninfo = db.Contract.Where(x => x.ID == info.ID).FirstOrDefault();
                db.Contract.Remove(aninfo);
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
            Contract info = new Contract();
            using (MyContext db = new MyContext())
            {
                info = db.Contract.Where(x => x.ConID == id).FirstOrDefault();
            }
            return View(info);
        }
        public ActionResult EditAnnouncer(Contract info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Contract uinfo = db.Contract.Where(x => x.ConID == info.ConID).FirstOrDefault();
                uinfo.ID = info.ID;
                uinfo.JoinDate = info.JoinDate;
                uinfo.LeaveDate = info.LeaveDate;
                uinfo.ConBeginDate = info.ConBeginDate;
                uinfo.ConEndDate = info.ConEndDate;
                uinfo.ConRemake = info.ConRemake;
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