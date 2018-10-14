using System;
using vtb.Core.DataAccessLayer;

namespace vtb.Warehouse.Data.Database.Model
{
    public class StorageUnit : Entity
    {
        public Guid ShelfId { get; set; }
        public Shelf Shelf { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public int Quantity { get; set; }
    }
}
