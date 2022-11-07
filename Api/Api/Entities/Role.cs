using Api.Common.Bases;

namespace Api.Entities
{
    public class Role : AbstractEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public byte? LevelRole { get; set; }
        public string Note { get; set; }
    }
}
