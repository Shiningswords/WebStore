﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDb _db;

        public SqlProductData(WebStoreDb db) => _db = db;

        public IEnumerable<Section> GetSections() => _db.Sections;

        public Section GetSection(int Id) => _db.Sections.Include(s => s.ParentSection).FirstOrDefault(s => s.Id == Id);

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public Brand GetBrand(int Id) => _db.Brands.Find(Id);

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.BrandId != null)
                    query = query.Where(product => product.BrandId == Filter.BrandId);

                if (Filter?.SectionId != null)
                    query = query.Where(product => product.SectionId == Filter.SectionId);
            }

            return query.Select(p => p.ToDTO());
        }

        public ProductDTO GetProductById(int id) => _db.Products
           .Include(p => p.Section)
           .Include(p => p.Brand)
           .FirstOrDefault(p => p.Id == id)
            .ToDTO();
    }
}
