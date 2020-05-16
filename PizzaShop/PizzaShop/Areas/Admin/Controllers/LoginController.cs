﻿using Model.Dao;
using PizzaShop.Areas.Admin.Models;
using PizzaShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();

                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();

                    userSession.Username = user.TenDangNhap;
                    userSession.UserId = user.MaNguoiDung;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tai khoan khong ton tai");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tai khoan dang bi khoa");
                }

                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mat khau sai");
                }
                else
                {
                    ModelState.AddModelError("", "Dang nhap khong dung");
                }

            }
            return View("Index");
        }


    }
}