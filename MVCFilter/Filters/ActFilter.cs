using MVCFilter.DesignPatterns.SingletonPattern;
using MVCFilter.Models.Context;
using MVCFilter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVCFilter.Models.Entities.Log;

namespace MVCFilter.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        MyContext _db;
        public ActFilter()
        {
            _db = DBTool.DBInstance;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log logger = new Log();

            if (filterContext.HttpContext.Session["oturum"] == null) logger.UserName = "Anonim kullanıcı";
            else logger.UserName = (filterContext.HttpContext.Session["oturum"] as AppUser).UserName;
            logger.ActionName = filterContext.ActionDescriptor.ActionName;
            logger.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            logger.Information = "Action tetiklenme aşamasında";
            logger.Description = Keyword.Entry;

            _db.Logs.Add(logger);
            _db.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log logger = new Log();
            if (filterContext.HttpContext.Session["oturum"] == null) logger.UserName = "Anonim kullanıcı";
            else logger.UserName = (filterContext.HttpContext.Session["oturum"] as AppUser).UserName;
            logger.ActionName = filterContext.ActionDescriptor.ActionName;
            logger.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            logger.Information = "Action geriye değer dondurmus durumdadır";
            logger.Description = Keyword.Exit;
            _db.Logs.Add(logger);
            _db.SaveChanges();
        }
    }
}