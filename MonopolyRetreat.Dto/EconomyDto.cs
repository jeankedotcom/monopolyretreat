using System.Collections.Generic;

namespace MonopolyRetreat.Dto
{
    public class EconomyDto
    {
        public IEnumerable<PointAndValueInTimeDto> BusinessHistory { get; set; }
        public IEnumerable<PointAndValueInTimeDto> HousingHistory { get; set; }
    }

    public class PointAndValueInTimeDto
    {
        public int PointInTime { get; set; }
        public long ValueInTime { get; set; }
    }
}