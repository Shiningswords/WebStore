using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDb _db;

        public SqlEmployeesData(WebStoreDb db) => _db = db;

        public int Add(Employee Employee)
        {
            if (_db.Employees is null)
                throw new ArgumentNullException(nameof(Employee));

            if (_db.Employees.Contains(Employee))
                return Employee.Id;

            //Employee.Id = _db.Employees.Count() == 0 ? 1 : _db.Employees.Max(e => e.Id) + 1;
            _db.Employees.Add(Employee);
            return Employee.Id;
        }

        public bool Delete(int id)
        {
            var db_item = GetById(id);
            if (db_item is null)
                return false;

            _db.Employees.Remove(db_item);
            return true;
        }

        public void Edit(Employee Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            Employee db_item = GetById(Employee.Id);
            if (db_item is null) return;
            _db.Employees.Attach(db_item);
            
            db_item.FirstName = Employee.FirstName;
            db_item.Surname = Employee.Surname;
            db_item.Patronymic = Employee.Patronymic;
            db_item.Age = Employee.Age;
            _db.SaveChanges();
        }

        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.FirstOrDefault(x => x.Id == id);


        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
