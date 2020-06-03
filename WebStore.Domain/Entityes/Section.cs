using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entityes.Base;

namespace WebStore.Domain.Entityes
{
    class SectionNamedEntity : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
    
}
