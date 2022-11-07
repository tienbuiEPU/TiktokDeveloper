﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Api.Common.Services
{
    public class CheckRole
    {
        public static bool CheckRoleByCode(string access_key, string key, int type)
        {
            var check = false;

            var functionRole = access_key.Split('-');
            for (var i = 0; i < functionRole.Count(); i++)
            {
                var code = functionRole[i].Split(':')[0];
                var role = functionRole[i].Split(':')[1];
                if (code == key)
                {
                    check = role.Substring(type,1) == "1" ? true : false;
                    break;
                }
            }

            return check;
        }

    }
}