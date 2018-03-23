using System;

namespace MonopolyRetreat.Dto
{
    public class PropertyDto
    {
        public string Name { get; set; }
        public long Value { get; set; }
        public PropertyType PropertyType { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
    }
}
