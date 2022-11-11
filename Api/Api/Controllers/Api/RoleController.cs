using Api.Common.Constants;
using Api.Common.Enums;
using Api.Common.Services;
using Api.Common.ViewModels.Common;
using Api.Entities;
using Api.Models.Data;
using Api.Persistence;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Api.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private static readonly ILog log = LogMaster.GetLogger("role", "role");
        private static string functionCode = "ROLE_MANAGEMENT";
        private readonly ApiDbContext _context;
        private IMapper _mapper;

        public RoleController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

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
                    IQueryable<Role> data = _context.Roles.Where(c => c.Status != AppEnums.EntityStatus.DELETED);
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
                        List<RoleData> res = _mapper.Map<List<RoleData>>(data.ToList());
                        foreach(RoleData item in res)
                        {
                            item.functionRoles = _context.FunctionRoles.Where(e => e.TargetId == item.Id && e.Type == (int)AppEnums.TypeFunction.FUNCTION_ROLE && e.Status != AppEnums.EntityStatus.DELETED).ToList();
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
            catch(Exception ex)
            {
                log.Error("GetByPage Error:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        // GET: api/Role/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            DefaultResponse def = new DefaultResponse();
            //check role
            var identity = (ClaimsIdentity)User.Identity;
            string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.VIEW))
            {
                def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_VIEW_MESSAGE);
                return Ok(def);
            }

            try
            {
                Role data = await _context.Roles.FindAsync(id);

                if (data == null)
                {
                    def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_VIEW_MESSAGE);
                    return Ok(def);
                }

                RoleData res = _mapper.Map<RoleData>(data);
                res.functionRoles = _context.FunctionRoles.Where(e => e.TargetId == res.Id && e.Type == (int)AppEnums.TypeFunction.FUNCTION_ROLE && e.Status != AppEnums.EntityStatus.DELETED).ToList();

                def.meta = new Meta(200, ApiConstants.MessageResource.ACCTION_SUCCESS);
                def.data = res;
                return Ok(def);
            }
            catch(Exception ex)
            {
                log.Error("GetByPage Error:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        // POST: api/Role
        [HttpPost]
        public async Task<IActionResult> Post(RoleData input)
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
                if (!ModelState.IsValid)
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }

                if (input.functionRoles == null)
                {
                    def.meta = new Meta(400, "Danh sách chức năng đang trống!");
                    return Ok(def);
                }

                if (input.functionRoles.Count == 0)
                {
                    def.meta = new Meta(400, "Danh sách chức năng đang trống!");
                    return Ok(def);
                }

                Role codeExist = _context.Roles.Where(f => f.Code == input.Code && f.Status != AppEnums.EntityStatus.DELETED).FirstOrDefault();
                if (codeExist != null)
                {
                    def.meta = new Meta(211, "Mã đã tồn tại!");
                    return Ok(def);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    input.CreatedById = userId;
                    input.CreatedBy = fullName;
                    _context.Roles.Add(input);

                    try
                    {
                        await _context.SaveChangesAsync();

                        if(input.functionRoles != null)
                        {
                            foreach (FunctionRole functionRole in input.functionRoles)
                            {
                                functionRole.TargetId = input.Id;
                                functionRole.Type = (int)AppEnums.TypeFunction.FUNCTION_ROLE;
                                functionRole.CreatedBy = fullName;
                                functionRole.CreatedById = userId;

                                _context.FunctionRoles.Add(functionRole);
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
                        if (RoleExists(input.Id))
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

        // PUT: api/Role/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoleData input)
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
                if (!ModelState.IsValid)
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }

                if (input.functionRoles == null)
                {
                    def.meta = new Meta(400, "Danh sách chức năng đang trống!");
                    return Ok(def);
                }

                if (input.functionRoles.Count == 0)
                {
                    def.meta = new Meta(400, "Danh sách chức năng đang trống!");
                    return Ok(def);
                }

                if (id != input.Id)
                {
                    def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                    return Ok(def);
                }

                Role data = await _context.Roles.FindAsync(id);
                if(data == null)
                {
                    def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_UPDATE_MESSAGE);
                    return Ok(def);
                }

                Role codeExist = _context.Roles.Where(f => f.Code == input.Code && f.Status != AppEnums.EntityStatus.DELETED && f.Id != id).FirstOrDefault();
                if (codeExist != null)
                {
                    def.meta = new Meta(211, "Mã đã tồn tại!");
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
                        List<FunctionRole> functionRoles = _context.FunctionRoles.Where(e => e.TargetId == input.Id && e.Type == (int)AppEnums.TypeFunction.FUNCTION_ROLE && e.Status != AppEnums.EntityStatus.DELETED).ToList();
                        if(input.functionRoles != null)
                        {
                            foreach (FunctionRole functionRole in input.functionRoles)
                            {
                                FunctionRole functionRoleExist = functionRoles.Where(e => e.FunctionId == functionRole.FunctionId).FirstOrDefault();
                                if (functionRoleExist == null)
                                {
                                    functionRole.TargetId = input.Id;
                                    functionRole.Type = (int)AppEnums.TypeFunction.FUNCTION_ROLE;
                                    functionRole.CreatedBy = fullName;
                                    functionRole.CreatedById = userId;
                                    _context.FunctionRoles.Add(functionRole);
                                }
                                else
                                {
                                    functionRoles.Remove(functionRole);
                                }
                            }
                        }

                        functionRoles.ForEach(item => {
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedById = userId;
                            item.UpdatedBy = fullName;
                            item.Status = AppEnums.EntityStatus.DELETED;
                        });
                        _context.UpdateRange(functionRoles);

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
                        if (!RoleExists(data.Id))
                        {
                            def.meta = new Meta(212, "Không tồn tại Id trên hệ thống!");
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

        // DELETE: api/Role/1
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
                Role data = await _context.Roles.FindAsync(id);
                if(data == null)
                {
                    def.meta = new Meta(404, ApiConstants.MessageResource.NOT_FOUND_DELETE_MESSAGE);
                    return Ok(def);
                }

                //Kiểm tra xem có người dùng nào map với nhóm quyền này không
                UserRole userRole = _context.UserRoles.Where(e => e.RoleId == data.Id && e.Status != AppEnums.EntityStatus.DELETED).FirstOrDefault();
                if (userRole != null)
                {
                    def.meta = new Meta(213, "Không thể xóa nhóm quyền đã được map với người dùng!");
                    return Ok(def);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedById = userId;
                    data.UpdatedBy = fullName;
                    data.Status = AppEnums.EntityStatus.DELETED;
                    _context.Update(data);

                    List<FunctionRole> fr = _context.FunctionRoles.Where(e => e.FunctionId == data.Id && e.Type == (int)AppEnums.TypeFunction.FUNCTION_ROLE && e.Status != AppEnums.EntityStatus.DELETED).ToList();
                    fr.ForEach(item => {
                        item.UpdatedAt = DateTime.Now;
                        item.UpdatedById = userId;
                        item.UpdatedBy = fullName;
                        item.Status = AppEnums.EntityStatus.DELETED;
                    });
                    _context.UpdateRange(fr);

                    //List<UserRole> ur = _context.UserRoles.Where(e => e.RoleId == data.Id && e.Status != AppEnums.EntityStatus.DELETED).ToList();
                    //ur.ForEach(item => {
                    //    item.UpdatedAt = DateTime.Now;
                    //    item.UpdatedById = userId;
                    //    item.UpdatedBy = fullName;
                    //    item.Status = AppEnums.EntityStatus.DELETED;
                    //});
                    //_context.UpdateRange(ur);

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
                        if (!RoleExists(data.Id))
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

        private bool RoleExists(int id)
        {
            return _context.Roles.Count(e => e.Id == id) > 0;
        }
    }
}
