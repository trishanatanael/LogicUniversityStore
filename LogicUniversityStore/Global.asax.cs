﻿using LogicUniversityStore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace LogicUniversityStore
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
           // Database.SetInitializer<LogicUniStoreModel>(new LogicUniStoreModelInitializer());
        }
    }
}