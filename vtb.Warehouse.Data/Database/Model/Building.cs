using System.Collections.Generic;
using vtb.Core.DataAccessLayer;

namespace vtb.Warehouse.Data.Database.Model
{

    public class Building : Entity
    {
        public string Label { get; set; }

        public ICollection<Rack> Racks { get; set; } = new List<Rack>();
    }
}
