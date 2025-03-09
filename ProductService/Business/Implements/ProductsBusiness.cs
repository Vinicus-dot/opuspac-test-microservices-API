using ProductService.Business.Interfaces;
using ProductService.Model.Request;
using ProductService.Repository.Interfaces;
using ServiceStack.Host;
using ProductService.Helper;
using System.Text.Json;
using ProductService.Model.DTO;
using ProductService.Model.Response;

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

            RabbitMQFactory _rabbitMQ = new (Util.RabbitConnection, Util.QueueProduct);

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
