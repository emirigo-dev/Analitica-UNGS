﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AnaliticaWS.Models;

namespace AnaliticaWS.Controllers
{
    [AllowAnonymous]
    public class UserController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(LoginDTO loginDTO) {

            ResponseToken res = new ResponseToken();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            bool isCredentialValid = (loginDTO.Username == "Boletin" && loginDTO.Password == "S1@2dS82OjZz");
            if (isCredentialValid) {
                DateTime localDate = DateTime.Now;
                var token = TokenGenerator.GenerateTokenJwt(loginDTO.Username);
                res.message = "OK.";
                res.statusCode = 200;
                res.token = token;
                res.expirateDate =  localDate.AddMinutes(Double.Parse(ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"])).ToString("dd/MM/yyyy H:mm:ss");
                return Ok(res);
            }

            res.message = "Error unauthorized";
            res.statusCode = 401;
            return Content(HttpStatusCode.Unauthorized, res);// status code 401 que mandamos

            
        }
    }
}
