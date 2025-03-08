using ProductService.Business.Interfaces;
using ProductService.Model.Request;
using ProductService.Repository.Interfaces;
using ServiceStack.Host;
using ProductService.Helper;
using System.Text.Json.Serialization;
using System.Text.Json;
using ProductService.Model.Entity;

namespace ProductService.Business.Implements
{
    public class ProductsBusiness : IProductsBusiness
    {
        private readonly string Queue = Util.GetEnvironmentVariable("PRODUCT_QUEUE");
        private readonly string RabbitConnection = Util.GetEnvironmentVariable("RABBIT_CONNECTION");
        private readonly IProductsRepository _productsRepository;
        public ProductsBusiness(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<object> CreateProduct(CreateProductRequest createProductRequest)
        {
            if(createProductRequest.Price < 0)
                throw new HttpException(StatusCodes.Status400BadRequest, "O preço tem que ser maior que zero!");

            var product = await _productsRepository.GetProduct(createProductRequest.Name);

            if(product != null)
                throw new HttpException(StatusCodes.Status400BadRequest, "Já exite um produto com esse nome!");

            await _productsRepository.InsertProduct(new() 
            { 
                Price = createProductRequest.Price,
                Name = createProductRequest.Name,
                Description = createProductRequest.Description,
            });

            RabbitMQFactory _rabbitMQ = new (RabbitConnection,Queue);

            product = await _productsRepository.GetProduct(createProductRequest.Name);

            await _rabbitMQ.PublishMessageAsync(JsonSerializer.Serialize(product));

            throw new HttpException(StatusCodes.Status201Created, "Sucesso");
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productsRepository.GetAllProducts();
        }
    }
}
