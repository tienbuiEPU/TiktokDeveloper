using Api.Entities;

namespace Api.Models.Data
{
    public class FunctionData : Function
    {
    }

    public partial class FunctionTreeData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Level { get; set; }
    }
}
