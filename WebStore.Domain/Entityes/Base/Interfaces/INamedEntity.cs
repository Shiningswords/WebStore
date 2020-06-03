using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entityes.Base.Interfaces
{
    public interface INamedEntity: IBaseEntity
    {
        string Name { get; set; }
    }
}
