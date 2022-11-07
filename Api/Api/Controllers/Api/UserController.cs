using Api.Common.Constants;
using Api.Common.Enums;
using Api.Common.Services;
using Api.Common.ViewModels.Common;
using Api.Models.Dto;
using Api.Persistence;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Entities;
using Api.Models.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly ILog log = LogMaster.GetLogger("function", "function");
        private static string functionCode = "USER_MANAGEMENT";
        private readonly ApiDbContext _context;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(ApiDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        #region user management
        [HttpGet("GetByPage")]
        public IActionResult GetByPage([FromQuery] FilteredPagination paging)
        {
            DefaultResponse def = new DefaultResponse();

            var identity = (ClaimsIdentity)User.Identity;
            string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.VIEW))
            {
                def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_VIEW_MESSAGE);
                return Ok(def);
            }

            try
            {
                if (paging != null)
                {
                    def.meta = new Meta(200, ApiConstants.MessageResource.ACCTION_SUCCESS);
                    IQueryable<User> data = _context.Users.Where(c => c.Status != AppEnums.EntityStatus.DELETED);
                    if (paging.query != null)
                    {
                        paging.query = HttpUtility.UrlDecode(paging.query);
                    }

                    data = data.Where(paging.query);
                    def.metadata = data.Count();

                    if (paging.page_size > 0)
                    {
                        if (paging.order_by != null)
                        {
                            data = data.OrderBy(paging.order_by).Skip((paging.page - 1) * paging.page_size).Take(paging.page_size);
                        }
                        else
                        {
                            data = data.OrderBy("Id desc").Skip((paging.page - 1) * paging.page_size).Take(paging.page_size);
                        }
                    }
                    else
                    {
                        if (paging.order_by != null)
                        {
                            data = data.OrderBy(paging.order_by);
                        }
                        else
                        {
                            data = data.OrderBy("Id desc");
                        }
                    }

                    if (paging.select != null && paging.select != "")
                    {
                        paging.select = "new(" + paging.select + ")";
                        paging.select = HttpUtility.UrlDecode(paging.select);
                        def.data = data.Select(paging.select);
                    }
                    else
                    {
                        List<UserData> res = _mapper.Map<List<UserData>>(data.ToList());
                        foreach (UserData item in res)
                        {
                            item.userRoles = _context.UserRoles.Where(e => e.UserId == item.Id && e.Status != AppEnums.EntityStatus.DELETED).ToList();
                        }

                        def.data = res;
                    }

                    return Ok(def);
                }
                else
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }
            }
            catch (Exception ex)
            {
                log.Error("GetByPage Error:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            DefaultResponse def = new DefaultResponse();
            
            var identity = (ClaimsIdentity)User.Identity;
            string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.VIEW))
            {
                def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_VIEW_MESSAGE);
                return Ok(def);
            }

            try
            {
                User data = await _context.Users.FindAsync(id);

                if (data == null)
                {
                    def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_VIEW_MESSAGE);
                    return Ok(def);
                }

                UserData res = _mapper.Map<UserData>(data);
                res.userRoles = _context.UserRoles.Where(e => e.UserId == res.Id && e.Status != AppEnums.EntityStatus.DELETED).ToList();

                def.meta = new Meta(200, ApiConstants.MessageResource.ACCTION_SUCCESS);
                def.data = res;
                return Ok(def);
            }
            catch (Exception ex)
            {
                log.Error("GetByPage Error:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post(UserData input)
        {
            DefaultResponse def = new DefaultResponse();

            var identity = (ClaimsIdentity)User.Identity;
            int userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            string fullName = identity.Claims.Where(c => c.Type == "FullName").Select(c => c.Value).SingleOrDefault() ?? string.Empty;
            string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.CREATE))
            {
                def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_CREATE_MESSAGE);
                return Ok(def);
            }

            try
            {
                input = (UserData)UtilsService.TrimStringPropertyTypeObject(input);

                if (!ModelState.IsValid)
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }

                if (input.userRoles == null)
                {
                    def.meta = new Meta(400, "Danh sách nhóm quyền đang trống!");
                    return Ok(def);
                }

                if (input.userRoles.Count == 0)
                {
                    def.meta = new Meta(400, "Danh sách nhóm quyền đang trống!");
                    return Ok(def);
                }

                User usernameExist = _context.Users.Where(f => f.UserName == input.UserName && f.Status != AppEnums.EntityStatus.DELETED).FirstOrDefault();
                if (usernameExist != null)
                {
                    def.meta = new Meta(211, "Tài khoản đã tồn tại!");
                    return Ok(def);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    input.CreatedById = userId;
                    input.CreatedBy = fullName;
                    _context.Users.Add(input);

                    try
                    {
                        await _context.SaveChangesAsync();

                        if (input.userRoles != null)
                        {
                            foreach (UserRole userRole in input.userRoles)
                            {
                                userRole.UserId = input.Id;
                                userRole.CreatedBy = fullName;
                                userRole.CreatedById = userId;

                                _context.UserRoles.Add(userRole);
                            }
                            await _context.SaveChangesAsync();
                        }

                        if (input.Id > 0)
                            transaction.Commit();
                        else
                            transaction.Rollback();

                        def.meta = new Meta(200, ApiConstants.MessageResource.ADD_SUCCESS);
                        def.data = input;
                        return Ok(def);
                    }
                    catch (DbUpdateException e)
                    {
                        log.Error("DbUpdateException:" + e);
                        transaction.Rollback();
                        if (UserExists(input.Id))
                        {
                            def.meta = new Meta(212, "Đã tồn tại Id trên hệ thống!");
                            return Ok(def);
                        }
                        else
                        {
                            def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                            return Ok(def);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Exception:" + e);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserData input)
        {
            DefaultResponse def = new DefaultResponse();

            var identity = (ClaimsIdentity)User.Identity;
            int userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            string fullName = identity.Claims.Where(c => c.Type == "FullName").Select(c => c.Value).SingleOrDefault() ?? string.Empty;
            string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.UPDATE))
            {
                def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_UPDATE_MESSAGE);
                return Ok(def);
            }

            try
            {
                input = (UserData)UtilsService.TrimStringPropertyTypeObject(input);

                if (!ModelState.IsValid)
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }

                if (input.userRoles == null)
                {
                    def.meta = new Meta(400, "Danh sách chức năng đang trống!");
                    return Ok(def);
                }

                if (input.userRoles.Count == 0)
                {
                    def.meta = new Meta(400, "Danh sách chức năng đang trống!");
                    return Ok(def);
                }

                if (id != input.Id)
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }

                User data = await _context.Users.FindAsync(id);
                if (data == null)
                {
                    def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_UPDATE_MESSAGE);
                    return Ok(def);
                }

                User usernameExist = _context.Users.Where(f => f.UserName == input.UserName && f.Status != AppEnums.EntityStatus.DELETED && f.Id != id).FirstOrDefault();
                if (usernameExist != null)
                {
                    def.meta = new Meta(211, "Tài khoản đã tồn tại!");
                    return Ok(def);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    input.UpdatedAt = DateTime.Now;
                    input.UpdatedById = userId;
                    input.UpdatedBy = fullName;
                    input.CreatedAt = data.CreatedAt;
                    input.CreatedBy = data.CreatedBy;
                    input.CreatedById = data.CreatedById;
                    input.Status = data.Status;

                    _context.Update(input);

                    try
                    {
                        List<UserRole> userRoles = _context.UserRoles.Where(e => e.UserId == input.Id && e.Status != AppEnums.EntityStatus.DELETED).ToList();
                        if (input.userRoles != null)
                        {
                            foreach (UserRole userRole in input.userRoles)
                            {
                                UserRole userRoleExist = userRoles.Where(e => e.RoleId == userRole.RoleId).FirstOrDefault();
                                if (userRoleExist == null)
                                {
                                    userRoleExist.UserId = input.Id;
                                    userRoleExist.CreatedBy = fullName;
                                    userRoleExist.CreatedById = userId;
                                    _context.UserRoles.Add(userRoleExist);
                                }
                                else
                                {
                                    userRoles.Remove(userRole);
                                }
                            }
                        }

                        userRoles.ForEach(item => {
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedById = userId;
                            item.UpdatedBy = fullName;
                            item.Status = AppEnums.EntityStatus.DELETED;
                        });
                        _context.UpdateRange(userRoles);

                        await _context.SaveChangesAsync();

                        if (data.Id > 0)
                            transaction.Commit();
                        else
                            transaction.Rollback();

                        def.meta = new Meta(200, ApiConstants.MessageResource.UPDATE_SUCCESS);
                        def.data = data;
                        return Ok(def);
                    }
                    catch (DbUpdateException e)
                    {
                        transaction.Rollback();
                        log.Error("DbUpdateException:" + e);
                        if (UserExists(data.Id))
                        {
                            def.meta = new Meta(212, "Đã tồn tại Id trên hệ thống!");
                            return Ok(def);
                        }
                        else
                        {
                            def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                            return Ok(def);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Exception:" + e);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DefaultResponse def = new DefaultResponse();

            var identity = (ClaimsIdentity)User.Identity;
            int userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            string fullName = identity.Claims.Where(c => c.Type == "FullName").Select(c => c.Value).SingleOrDefault() ?? string.Empty;
            string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.DELETED))
            {
                def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_DELETE_MESSAGE);
                return Ok(def);
            }

            try
            {
                User data = await _context.Users.FindAsync(id);
                if (data == null)
                {
                    def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_DELETE_MESSAGE);
                    return Ok(def);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedById = userId;
                    data.UpdatedBy = fullName;
                    data.Status = AppEnums.EntityStatus.DELETED;
                    _context.Update(data);

                    List<UserRole> ur = _context.UserRoles.Where(e => e.UserId == data.Id && e.Status != AppEnums.EntityStatus.DELETED).ToList();
                    ur.ForEach(item => {
                        item.UpdatedAt = DateTime.Now;
                        item.UpdatedById = userId;
                        item.UpdatedBy = fullName;
                        item.Status = AppEnums.EntityStatus.DELETED;
                    });
                    _context.UpdateRange(ur);

                    try
                    {
                        await _context.SaveChangesAsync();
                        if (data.Id > 0)
                            transaction.Commit();
                        else
                            transaction.Rollback();

                        def.meta = new Meta(200, ApiConstants.MessageResource.DELETE_SUCCESS);
                        def.data = data;
                        return Ok(def);
                    }
                    catch (DbUpdateException e)
                    {
                        transaction.Rollback();
                        log.Error("DbUpdateException:" + e);
                        if (!UserExists(data.Id))
                        {
                            def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_DELETE_MESSAGE);
                            return Ok(def);
                        }
                        else
                        {
                            def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                            return Ok(def);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Count(e => e.Id == id) > 0;
        }
        #endregion

        #region actions by user
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            DefaultResponse def = new DefaultResponse();
            try
            {
                if (loginModel != null)
                {
                    if (loginModel.username != null && loginModel.password != null && loginModel.username != "" && loginModel.password != "")
                    {
                        string username = loginModel.username.Trim();
                        Entities.User user = _context.Users.Where(e => e.UserName.Equals(username) && e.Status != AppEnums.EntityStatus.DELETED).FirstOrDefault();

                        if (user != null)
                        {
                            string password = user.KeyLock.Trim() + user.RegEmail.Trim() + user.Id + loginModel.password.Trim();
                            password = UtilsService.GetMD5Hash(password);
                            Entities.User userLogin = _context.Users.Where(e => e.UserName.Equals(username) && e.Password == password && e.Status != AppEnums.EntityStatus.DELETED).FirstOrDefault();
                            if (userLogin != null)
                            {
                                UserLoginData userLoginData = _mapper.Map<UserLoginData>(userLogin);

                                if (userLoginData.Status == AppEnums.EntityStatus.LOCK)
                                {
                                    def.meta = new Meta(223, "Tài khoản bị khóa!");
                                    return Ok(def);
                                }

                                Role roleMax = _context.Roles.Where(r => r.Id == userLogin.RoleMax && r.Status != AppEnums.EntityStatus.DELETED).FirstOrDefault();
                                userLoginData.RoleCode = roleMax != null ? roleMax.Code : "";

                                int userId = userLoginData.Id;
                                List<MenuData> listFunctionRole = new List<MenuData>();
                                List<UserRole> userRole = _context.UserRoles.Where(e => e.UserId == userId && e.Status == AppEnums.EntityStatus.NORMAL).ToList();

                                foreach (var item in userRole)
                                {
                                    var listFRR = (from fr in _context.FunctionRoles
                                                   join f in _context.Functions on fr.FunctionId equals f.Id
                                                   where fr.TargetId == item.RoleId
                                                      && fr.Type == (int)AppEnums.TypeFunction.FUNCTION_ROLE
                                                      && fr.Status == AppEnums.EntityStatus.NORMAL
                                                      && f.Status == AppEnums.EntityStatus.NORMAL
                                                   select new
                                                   {
                                                       fr.Id,
                                                       fr.FunctionId,
                                                       fr.Type,
                                                       fr.Status,
                                                       fr.ActiveKey,
                                                       f.Location,
                                                       f.Code,
                                                       f.Name,
                                                       f.Url,
                                                       f.Icon,
                                                       f.FunctionParentId,
                                                       f.IsSpecialFunc
                                                   }).OrderBy(e => e.Location).ToList();

                                    foreach (var itemFR in listFRR)
                                    {
                                        var fr = listFunctionRole.Where(e => e.MenuId == itemFR.FunctionId).ToList();
                                        if (fr.Count > 0)
                                        {
                                            string key1 = fr.FirstOrDefault().ActiveKey;
                                            if (fr.FirstOrDefault().ActiveKey != itemFR.ActiveKey)
                                            {
                                                key1 = plusActiveKey(fr.FirstOrDefault().ActiveKey, itemFR.ActiveKey);
                                            }
                                            fr.FirstOrDefault().ActiveKey = key1;
                                        }
                                        else
                                        {
                                            Function function = _context.Functions.Where(e => e.Id == itemFR.FunctionId).FirstOrDefault();
                                            if (function != null)
                                            {
                                                MenuData menu = new MenuData();
                                                menu.MenuId = itemFR.FunctionId;
                                                menu.Code = function.Code;
                                                menu.Name = function.Name;
                                                menu.Url = function.Url;
                                                menu.Icon = function.Icon;
                                                menu.MenuParent = (int)function.FunctionParentId;
                                                menu.ActiveKey = itemFR.ActiveKey;
                                                menu.IsSpecialFunc = function.IsSpecialFunc;
                                                listFunctionRole.Add(menu);
                                            }
                                        }
                                    }
                                }

                                string access_key = "";
                                int count = listFunctionRole.Count;
                                if (count > 0)
                                {
                                    for (int i = 0; i < count - 1; i++)
                                    {
                                        if (listFunctionRole[i].ActiveKey != "000000000")
                                        {
                                            access_key += listFunctionRole[i].Code + ":" + listFunctionRole[i].ActiveKey + "-";
                                        }
                                    }

                                    access_key = access_key + listFunctionRole[count - 1].Code + ":" + listFunctionRole[count - 1].ActiveKey;
                                }

                                userLoginData.AccessKey = access_key;
                                userLoginData.listMenus = CreateMenu(listFunctionRole, 0);
                                var claims = new List<Claim>
                                {
                                    new Claim(JwtRegisteredClaimNames.Email, userLogin.Email),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                                    new Claim(ClaimTypes.Name, userLogin.FullName),
                                    new Claim("Id", userLogin.Id.ToString()),
                                    new Claim("FullName", userLogin.FullName != null ? userLogin.FullName.ToString() : ""),
                                    new Claim("Phone", userLogin.Phone != null ? userLogin.Phone.ToString() : ""),
                                    new Claim("Avatar", userLogin.Avatar != null ? userLogin.Avatar.ToString() : ""),
                                    new Claim("RoleMax", userLogin.RoleMax != null ? userLogin.RoleMax.ToString() : ""),
                                    new Claim("RoleLevel", userLogin.RoleLevel != null ? userLogin.RoleLevel.ToString() : ""),
                                    new Claim("AccessKey", access_key != null ? access_key : "")
                                };

                                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:JwtKey"]));
                                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["AppSettings:JwtExpireDays"]));

                                var token = new JwtSecurityToken(
                                    _configuration["AppSettings:JwtIssuer"],
                                    _configuration["AppSettings:JwtIssuer"],
                                    claims,
                                    expires: expires,
                                    signingCredentials: creds
                                );

                                userLoginData.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                                userLoginData.BaseApi = _configuration["AppSettings:BaseApi"];
                                userLoginData.BaseUrlImg = _configuration["AppSettings:BaseUrlImg"];
                                userLoginData.BaseUrlImgThumb = _configuration["AppSettings:BaseUrlImgThumb"];
                                userLoginData.BaseUrlFile = _configuration["AppSettings:BaseUrlFile"];

                                def.data = userLoginData;
                                def.meta = new Meta(200, "Đăng nhập thành công!");
                                return Ok(def);
                            }
                            else
                            {
                                def.meta = new Meta(404, "Tài khoản hoặc mật khẩu không chính xác!");
                                return Ok(def);
                            }
                        }
                        else
                        {
                            def.meta = new Meta(404, "Tài khoản hoặc mật khẩu không chính xác!");
                            return Ok(def);
                        }
                    }
                    else
                    {
                        def.meta = new Meta(400, ApiConstants.MessageResource.ERROR_400_MESSAGE);
                        return Ok(def);
                    }
                }
                else
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.ERROR_400_MESSAGE);
                    return Ok(def);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        private string plusActiveKey(string key1, string key2)
        {
            string str = "";
            char[] str1 = key1.ToCharArray();
            char[] str2 = key2.ToCharArray();
            for (int i = 0; i < str1.Length; i++)
            {
                int k = int.Parse(str1[i].ToString()) + int.Parse(str2[i].ToString());
                if (k > 1) k = 1;
                str += k;
            }
            return str;
        }

        private List<MenuData> CreateMenu(List<MenuData> list, int k)
        {
            var listMenu = list.Where(e => e.MenuParent == k).ToList();
            if (listMenu.Count > 0)
            {
                List<MenuData> menus = new List<MenuData>();
                foreach (var item in listMenu)
                {
                    char[] str = item.ActiveKey.ToCharArray();
                    if (int.Parse(str[8].ToString()) == 1)
                    {
                        MenuData menu = new MenuData();
                        menu.MenuId = item.MenuId;
                        menu.Code = item.Code;
                        menu.Name = item.Name;
                        menu.Url = item.Url;
                        menu.Icon = item.Icon;
                        menu.IsSpecialFunc = item.IsSpecialFunc;
                        menu.MenuParent = item.MenuParent;
                        menu.ActiveKey = item.ActiveKey;
                        menu.listMenus = CreateMenu(list, item.MenuId);
                        menus.Add(menu);
                    }
                }
                return menus;
            }
            return new List<MenuData>();
        }
        #endregion
    }
}
