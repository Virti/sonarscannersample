using System;
using System.Collections.Generic;
using vtb.Core.DataAccessLayer;

namespace vtb.Warehouse.Data.Database.Model
{
    public class Rack : Entity
    {
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }

        public int Order { get; set; }

        public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}