using System;

namespace vtb.Core.DataAccessLayer
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
