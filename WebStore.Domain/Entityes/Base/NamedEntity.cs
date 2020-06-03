using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes.Base
{
    class NamedEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
