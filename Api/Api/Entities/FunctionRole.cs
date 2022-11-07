using Api.Common.Bases;

namespace Api.Entities
{
    public class FunctionRole : AbstractEntity<int>
    {
        public long TargetId { get; set; }
        public int FunctionId { get; set; }
        public string ActiveKey { get; set; }
        public byte? Type { get; set; }
    }
}
