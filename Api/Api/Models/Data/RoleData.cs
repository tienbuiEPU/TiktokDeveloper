using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Data
{
    public class RoleData : Role
    {
        public List<FunctionRole> functionRoles { get; set; }
    }
}
