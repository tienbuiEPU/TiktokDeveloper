using Api.Common.Bases;

namespace Api.Entities
{
    public class TypeAttribute : AbstractEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
