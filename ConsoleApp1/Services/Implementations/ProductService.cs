using ConsoleApp1.entities;
using ConsoleApp1.Models.DTOs.Requests;
using ConsoleApp1.Models.DTOs.Responses;
using ConsoleApp1.Repositories.Interfaces;
using ConsoleApp1.Services.Interfaces;

namespace ConsoleApp1.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            return _repository.GetAllProducts()
                .Select(p => new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
                return null;

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public ProductForReadDto? CreateProduct(ProductForCreateDto dto)
        {
            var existingProduct = _repository
                .GetAllProducts()
                .FirstOrDefault(p =>
                    p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));

            if (existingProduct != null)
            {
                return null;
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(product);

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = _repository.GetProductById(id);

            if (product != null)
            {
                product.Name = dto.Name;
                product.Price = dto.Price;

                _repository.UpdateProduct(product);
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetProductById(id);

            if (product != null)
            {
                _repository.DeleteProduct(product);
            }
        }

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            return _repository.SearchProductsByName(name)
                .Select(p => new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();
        }

        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();

            if (!products.Any())
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = string.Empty
                };
            }

            return new ProductStatsDto
            {
                Total = products.Count,
                AveragePrice = products.Average(p => p.Price),
                MostExpensiveName = products
                    .OrderByDescending(p => p.Price)
                    .First()
                    .Name
            };
        }
    }
}