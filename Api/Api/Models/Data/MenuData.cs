using System.Collections.Generic;

namespace Api.Models.Data
{
    public class MenuData
    {
        public int MenuId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MenuParent { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string ActiveKey { get; set; }
        public int? Status { get; set; }
        public bool? IsSpecialFunc { get; set; }
        public List<MenuData> listMenus { get; set; }
    }
}
