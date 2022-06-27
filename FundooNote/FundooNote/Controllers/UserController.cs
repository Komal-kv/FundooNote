﻿using BusinessLayer.Interfaces;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        IUserBL userBL;
        FundooContext fundooContext;
        public UserController(IUserBL userBL, FundooContext fundooContext)
        {
            this.userBL = userBL;
            this.fundooContext = fundooContext;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserPostModel userPostModel)
        {
            try
            {
                
                var user = fundooContext.Users.FirstOrDefault(u => u.Email == userPostModel.Email);
                if (user != null)
                {
                    return this.BadRequest(new { success = false, message = "Email Already Exits" });
                    
                }
                this.userBL.AddUser(userPostModel);

                return this.Ok(new { success = true, message = "Registration Successfull" });


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn(string email, string password)
        {
            try
            {

                var user = fundooContext.Users.FirstOrDefault(u => u.Email == email);
                string Password = PwdEncryptDecryptService.DecryptPassword(user.Password);
                if (user == null)
                {
                    return this.BadRequest(new { success = false, message = "Email doesn't Exits" });
                   
                }
                var userdata1 = fundooContext.Users.FirstOrDefault(u => u.Email == email && Password == password);

                if (userdata1 == null)
                {
                    return this.BadRequest(new { success = false, message = "Password is Invalid" });
                }
                    string token = this.userBL.LogInUser(email, password);
                return this.Ok(new { success = true, message = "LogIn Successfull", data = token });

            }
            catch (Exception e)
            {
                throw e;    
            }
        }
    }
}
