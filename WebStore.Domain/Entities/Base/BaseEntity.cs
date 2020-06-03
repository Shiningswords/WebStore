using System;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
