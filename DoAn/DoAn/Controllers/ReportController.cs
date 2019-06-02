using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class ReportController : Controller
    {
        
        /* BAT CHART OF VISITING EACH MONTH  */
        public ActionResult ChartView()
        {
            if ((string)Session["role"] == "admin")
            {
                return View();
            }
            else
                return Redirect("~/Book/ShowBook");
            
        }


        /* TOP BOOKS WERE BOUGHT */
        public ActionResult BestSellBook()
        {
            if ((string)Session["role"] == "admin")
            {
                ViewBag.Report = ReportAction.BestSeller();
                ViewBag.Customer = ReportAction.TopCustomer();
                return View();
            }
            else
                return Redirect("~/Book/ShowBook");
            
        }


        /* JSON - GET DATA COUNT VISITORS EACH MONTH */
        public JsonResult GetChartData()
        {
            return Json(ReportAction.Get_Visit(), JsonRequestBehavior.AllowGet);
        }


        /* JSON - GET DATA PROFIT EVERY DAY */
        public JsonResult GetDataProfit()
        {
            return Json(ReportAction.Profits(), JsonRequestBehavior.AllowGet);
        }

    }
}