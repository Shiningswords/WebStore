using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>Информация о сотруднике</summary>
    [Table("Employees")]
    public class Employee : BaseEntity, IOrderedEntity
    {
        public int Order { get; set; }
        /// <summary>Имя</summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>Фамилия</summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>Отчество</summary>
        public string Patronymic { get; set; }

        /// <summary>Возраст</summary>
        public int Age { get; set; }
    }
}
