﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestProject.Data;

namespace TestProject.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the TestProjectUser class
    public class TestProjectUser : IdentityUser<int>
    {
        public TestProjectUser():base()
        {

        }
      
    }


    
}
