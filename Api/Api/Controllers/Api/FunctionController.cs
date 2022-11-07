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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Api.Controllers.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private static readonly ILog log = LogMaster.GetLogger("function", "function");
        private static string functionCode = "FUNCTION_MANAGEMENT";
        private readonly ApiDbContext _context;
        private IMapper _mapper;

        public FunctionController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetByPage")]
        public IActionResult GetByPage([FromQuery] FilteredPagination paging)
        {
            DefaultResponse def = new DefaultResponse();
            //check role
            //var identity = (ClaimsIdentity)User.Identity;
            //string access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault();
            //if (!CheckRole.CheckRoleByCode(access_key, functionCode, (int)AppEnums.Action.VIEW))
            //{
            //    def.meta = new Meta(222, ApiConstants.MessageResource.NOPERMISION_VIEW_MESSAGE);
            //    return Ok(def);
            //}

            if (paging != null)
            {
                def.meta = new Meta(200, ApiConstants.MessageResource.ACCTION_SUCCESS);
                IQueryable<Function> data = _context.Functions.Where(c => c.Status != AppEnums.EntityStatus.DELETED);
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
                    def.data = _mapper.Map<List<FunctionData>>(data.ToList());
                }

                return Ok(def);
            }
            else
            {
                def.meta = new Meta(400, ApiConstants.MessageResource.BAD_REQUEST_MESSAGE);
                return Ok(def);
            }
        }
    }
}
