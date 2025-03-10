using ProductService.Business.Interfaces;
using ServiceStack.Host;
using System.Text.Json;
using Helper;
using Model;
using Repository.Interfaces;
using Model.Product.Request;
using Microsoft.AspNetCore.Http;
using Model.Product.DTO;

namespace ProductService.Business.Implements
{
    public class ProductsBusiness : IProductsBusiness
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsBusiness(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<object?> CreateProduct(CreateProductRequest createProductRequest)
        {
            if(createProductRequest.Price <= 0)
                throw new HttpException(StatusCodes.Status400BadRequest, "O preço tem que ser maior que zero!");

            if (string.IsNullOrEmpty(createProductRequest.Name))
                throw new HttpException(StatusCodes.Status400BadRequest, "Name deve ser preenchido!");

            var product = await _productsRepository.GetProduct(createProductRequest.Name);

            if(product != null)
                throw new HttpException(StatusCodes.Status400BadRequest, "Já exite um produto com esse nome!");

            await _productsRepository.InsertProduct(new() 
            { 
                Price = createProductRequest.Price,
                Name = createProductRequest.Name,
                Description = createProductRequest.Description,
            });

            RabbitMQFactory _rabbitMQ = new (Util.GetEnvironmentVariable("RABBIT_CONNECTION"), Util.GetEnvironmentVariable("PRODUCT_QUEUE"));

            product = await _productsRepository.GetProduct(createProductRequest.Name);

            await _rabbitMQ.PublishMessageAsync(JsonSerializer.Serialize(product));

            return default;
        }

        public async Task<ListResponse<ProductDTO>> GetAllProducts(int pageNumber, int pageSize)
        {
            var products = await _productsRepository.GetAllProducts(pageNumber, pageSize); 
            return new ListResponse<ProductDTO>
            {
                Data = new ProductDTO().ToListDto(products.Data),
                Total = products.Total,
                PageNumber = products.PageNumber,
                PageSize = products.PageSize
            };
        }
    }
}
