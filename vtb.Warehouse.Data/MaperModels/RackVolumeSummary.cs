using System;
using System.Collections.Generic;
using System.Text;

namespace vtb.Warehouse.Data.MaperModels
{
    public class BuildingVolumeSummary
    {
        public Guid BuildingId { get; set; }
        public long TotalVolume { get; set; }

        public long VolumeTaken { get; set; }
        public long VolumeLeft { get => TotalVolume - VolumeTaken; }
    }
}
