using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public Brand GetBrand(int Id) => TestData.Brands.FirstOrDefault(b => b.Id == Id);

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public Section GetSection(int Id) => TestData.Sections.FirstOrDefault(s => s.Id == Id);

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query.Select(p => p.ToDTO());
        }

        public ProductDTO GetProductById(int id)
        {
            return TestData.Products.FirstOrDefault(product => product.Id == id).ToDTO();
        }
    }
}
