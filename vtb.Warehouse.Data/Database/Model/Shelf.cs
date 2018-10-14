using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vtb.Core.DataAccessLayer;

namespace vtb.Warehouse.Data.Database.Model
{
    public class Shelf : Entity
    {
        public Guid RackId { get; set; }
        public Rack Rack { get; set; }

        public long Width { get; set; }
        public long Height { get; set; }
        public long Depth { get; set; }

        public ICollection<StorageUnit> StorageUnits { get; set; } = new List<StorageUnit>();

        public long Area { get => Width * Height; }
        public long Volume { get => Area * Depth; }
    }
}