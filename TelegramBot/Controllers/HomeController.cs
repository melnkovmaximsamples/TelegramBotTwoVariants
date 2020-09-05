using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelegramBot.Models;

namespace TelegramBot.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "It's my telega bot D:";
        }
    }
}
