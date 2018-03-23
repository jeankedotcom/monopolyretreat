using System.Collections.Generic;

namespace MonopolyRetreat.Dto
{
    public class LeaderboardUserDto
    {
        public string Name { get; set; }
        public long AvailableMoney { get; set; }
        public long NetWorth { get; set; }
        public IEnumerable<PropertyPosition> Properties { get; set; }
    }
}