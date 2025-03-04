using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.Server.Attributes
{
    public class RouteControllerAttribute : RouteAttribute
    {
        public RouteControllerAttribute() : base("[controller]")
        {
        }
    }
}
