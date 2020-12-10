﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using REW的空间BLL;
using REW的空间Model;

namespace REW的空间.MyClasses
{
    public class ClassCheckIn
    {
        private static BLL_checkin bll_checkin = new BLL_checkin();
        public static bool IsCheckedIn(string username)
        {
            //BLL_checkin bll_checkin = new BLL_checkin();
            return bll_checkin.IsCheckedIn(username);
        }
    }
}