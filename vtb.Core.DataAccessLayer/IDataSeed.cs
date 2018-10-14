using Microsoft.EntityFrameworkCore;
using System;

namespace vtb.Core.DataAccessLayer
{
    public interface IDataSeed : IDisposable
    {
        void Seed(ModelBuilder modelBuilder);
    }
}
