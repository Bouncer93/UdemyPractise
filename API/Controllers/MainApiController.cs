﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MainApiController : ControllerBase
    {
        
    }
}
