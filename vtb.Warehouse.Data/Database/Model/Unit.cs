using vtb.Core.DataAccessLayer;

namespace vtb.Warehouse.Data.Database.Model
{
    public class Unit : Entity
    {
        public string Label { get; set; }

        public long Width { get; set; }
        public long Height { get; set; }
        public long Depth { get; set; }

        public long Weight { get; set; }

        public long Area { get => Width * Height; }
        public long Volume { get => Area * Depth; }
    }
}
