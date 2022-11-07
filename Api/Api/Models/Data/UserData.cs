using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Data
{
    public class UserLoginData : User
    {
        public string AccessKey { get; set; }
        public string AccessToken { get; set; }
        public string RoleCode { get; set; }
        public string BaseApi { get; set; }
        public string BaseUrlImg { get; set; }
        public string BaseUrlImgThumb { get; set; }
        public string BaseUrlFile { get; set; }
        public List<MenuData> listMenus { get; set; }
    }

    public class UserData : User
    {
        public List<UserRole> userRoles { get; set; }
    }
}
