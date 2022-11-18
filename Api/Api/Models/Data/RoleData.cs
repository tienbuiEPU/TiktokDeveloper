using Api.Entities;
using System.Collections.Generic;

namespace Api.Models.Data
{
    public class RoleData : Role
    {
        public List<FunctionRole> functionRoles { get; set; }
    }
}
