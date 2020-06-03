using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entityes.Base.Interfaces
{
    interface IOrderedEntity: IBaseEntity
    {
        int Order { get; set; }
    }
}
