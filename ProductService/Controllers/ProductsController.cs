using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Business.Interfaces;
using ProductService.Model.Entity;
using ProductService.Model.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Controllers
{
    public class ProductsController : GenericController
    {
        private readonly IProductsBusiness _productsBusiness;
        public ProductsController(IProductsBusiness productsBusiness)
        {
            _productsBusiness = productsBusiness;
        }
        /// <summary>
        /// List all products
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves a list of all available products.
        /// </remarks>
        /// <response code="200">Returns the list of products</response>
        /// <response code="500">Internal server error</response>
        [SwaggerResponse(200, "Returns the list of products.", typeof(List<Product>))]
        [SwaggerResponse(500, "Internal server error.")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productsBusiness.GetAllProducts());
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <remarks>
        /// This endpoint creates a new product with a name, description, and price.
        /// </remarks>
        /// <param name="request">Product creation data</param>
        /// <response code="201">Product successfully created</response>
        /// <response code="400">Malformed request</response>
        /// <response code="500">Internal server error</response>
        [SwaggerResponse(201, "Product successfully created.")]
        [SwaggerResponse(400, "Malformed request.")]
        [SwaggerResponse(500, "Internal server error.")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            return Ok(await _productsBusiness.CreateProduct(request));
        }

    }
}
