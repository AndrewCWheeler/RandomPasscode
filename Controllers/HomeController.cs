using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode
{
    public class HomeController : Controller
    {
        static int count = 0;
        static object lockObj = new object();
        // Requests
        // localhost:5000/
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Generate
        // localhost:5000/generate
        [Route("Generate")]
        [HttpGet]

        public IActionResult Generate()
        {
            lock(lockObj)
            {
                count++;
            }
            Random rand = new Random();
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var builder = new StringBuilder();
            for (var i = 0; i < 14; i++)
            { 
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
            }
            var passcode = builder.ToString();
            ViewBag.Passcode = passcode;
            ViewBag.Count = count;
            return View("Index", passcode);
        }
    }
}