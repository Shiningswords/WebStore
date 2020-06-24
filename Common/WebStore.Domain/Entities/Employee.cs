using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("Employees")]
    public class Employee : BaseEntity, IOrderedEntity
    {
        public int Order { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }
    }
}
