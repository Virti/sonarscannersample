using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vtb.Core.DataAccessLayer
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
