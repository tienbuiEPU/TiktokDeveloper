using Api.Entities;
using System.Collections.Generic;

namespace Api.Models.Data
{
    public class UserLoginData : User
    {
        public string AccessKey { get; set; }
        public string AccessToken { get; set; }
        public string RoleCode { get; set; }
        public string BaseUrlImg { get; set; }
        public string BaseUrlFile { get; set; }
        public List<MenuData> listMenus { get; set; }
    }

    public class UserData : User
    {
        public List<UserRole> userRoles { get; set; }
    }
}
