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
    public class ResFilter:FilterAttribute,IResultFilter
    {
        MyContext _db;
        public ResFilter()
        {
            _db = DBTool.DBInstance;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log logger = new Log();
            if (filterContext.HttpContext.Session["oturum"] != null) logger.UserName = (filterContext.HttpContext.Session["oturum"] as AppUser).UserName;
            else logger.UserName = "Anonim kullanıcı";
            logger.ActionName = filterContext.RouteData.Values["Action"].ToString();
            logger.ControllerName = filterContext.RouteData.Values["Controller"].ToString();
            logger.Description = Keyword.Entry;
            logger.Information = "Sayfa render edilmek üzere";
            _db.Logs.Add(logger);
            _db.SaveChanges();

        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log logger = new Log();
            if (filterContext.HttpContext.Session["oturum"] != null) logger.UserName = (filterContext.HttpContext.Session["oturum"] as AppUser).UserName;
            else logger.UserName = "Anonim kullanıcı";
            logger.ActionName = filterContext.RouteData.Values["Action"].ToString();
            logger.ControllerName = filterContext.RouteData.Values["Controller"].ToString();
            logger.Description = Keyword.Exit;
            logger.Information = "Sayfa render edildi";
            _db.Logs.Add(logger);
            _db.SaveChanges();

        }
    }
}