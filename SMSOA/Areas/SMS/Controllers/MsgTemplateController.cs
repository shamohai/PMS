﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.IBLL;
using PMS.Model;

namespace SMSOA.Areas.SMS.Controllers
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class MsgTemplateController : Admin.Controllers.BaseController
    {
       IS_SMSMsgContentBLL smsMsgContentBLL { get; set; }

        // GET: SMS/MsgTemplate
        public ActionResult Index()
        {
            ViewBag.GetInfo = "/SMS/MsgTemplate/GetAllMsgContent";
            
            ViewBag.ShowAdd = "/SMS/MsgTemplate/ShowAddTemplate";
            ViewBag.ShowEdit= "/SMS/MsgTemplate/ShowEditTemplate";
            
            //ViewBag.ShowEdit = "/Admin/Action/ShowEditActionInfo";
            //ViewBag.GetInfo = "/Admin/Action/GetActionInfo";
            return View();
        }

        public ActionResult ShowEditTemplate(int id)
        {
            //int id = int.Parse(Request["id"]);
            //若有传入的id
            if (id != 0)
            {
                //1 找到指定id的action对象
                var model = smsMsgContentBLL.GetListBy(t => t.TID == id).FirstOrDefault();   //注意记得加FirstOrDefault否则model就是一个集合 
                ViewBag.LoginUserID = -999;
                //若父控制器中的登录用户不为空
                if (base.LoginUser != null)
                {
                    //获取登录用户的id
                    ViewBag.LoginUserID = base.LoginUser.ID;
                }
                ViewBag.TID = model.TID;
                ViewBag.GetAllMission_combogrid = "/SMS/Send/GetMissionByUser";
                ViewBag.MsgName = model.MsgName;
                ViewBag.SMID = model.SMID;
                ViewBag.MsgContent = model.MsgContent;
                //5 提供显示页面提交时跳转到的权限名称
                //修改即跳转至修改方法
                ViewBag.backAction_jqSub = "/SMS/MsgTemplate/DoEditTemplate";
                //ViewData["actionInfo"] = model;
                //return PartialView("EditActionWindow");
                return View();
            }
            return Content("no");
        }

        public ActionResult ShowAddTemplate()
        {
            ViewBag.LoginUserID = -999;
            //若父控制器中的登录用户不为空
            if (base.LoginUser != null)
            {
                //获取登录用户的id
                ViewBag.LoginUserID = base.LoginUser.ID;
            }
            ViewBag.GetAllMission_combogrid = "/SMS/Send/GetMissionByUserUnChecked";
            ViewBag.backAction_jqSub = "/SMS/MsgTemplate/DoAddTemplate";
            return View();
        }

        /// <summary>
        /// 分页查询全部短信模板
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllMsgContent()
        {
            int pageSize = int.Parse(Request.Form["rows"]);
            int pageIndex = int.Parse(Request.Form["page"]);
            int rowCount = 0;
           
            
            var userId = base.LoginUser.ID;
            //1 查询全部的短信内容模板，并根据sort进行排序
            var list_allMsgContent = smsMsgContentBLL.GetPageList(pageIndex, pageSize, ref rowCount, m => m.isDel == false&&m.UID==userId, m => m.Sort, true).ToList().Select(m => m.ToMiddleModel()).ToList();


            //2 转成easyui DataGrid
            PMS.Model.EasyUIModel.EasyUIDataGrid dgModel = new PMS.Model.EasyUIModel.EasyUIDataGrid()
            {
                total = rowCount,
                rows = list_allMsgContent,
                footer = null
            };
            //3 序列化
            return Content(Common.SerializerHelper.SerializerToString(dgModel));
        }

        public ActionResult DoAddTemplate(S_SMSMsgContent templateModel)
        {
            //创建一个新的Action方法，需要对未提交的属性进行初始化赋值
            templateModel.isDel = false;
            //departmentModel.ModifiedOnTime = DateTime.Now;
            
            try
            {
                smsMsgContentBLL.Create(templateModel);
                return Content("ok");
            }
            catch
            {
                return Content("error");
            }
        }

        public ActionResult DoEditTemplate(S_SMSMsgContent templateModel)
        {
            //创建一个新的Action方法，需要对未提交的属性进行初始化赋值
            templateModel.isDel = false;
            //departmentModel.ModifiedOnTime = DateTime.Now;

            try
            {
                smsMsgContentBLL.Update(templateModel);
                return Content("ok");
            }
            catch(Exception ex)
            {
                return Content("error");
            }
        }
    }
}