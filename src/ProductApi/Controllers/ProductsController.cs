using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products =
    [
        new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 1500m,
            Quantity = 10
        },
        new Product
        {
            Id = 2,
            Name = "Smartphone",
            Price = 800m,
            Quantity = 20
        },
        new Product
        {
            Id = 3,
            Name = "Headphones",
            Price = 120m,
            Quantity = 30
        }
    ];

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAll(
        [FromQuery] string? search,
        [FromQuery] int page = 1)
    {
        if (page < 1)
        {
            return BadRequest(new
            {
                message = "Page must be greater than or equal to 1."
            });
        }

        const int pageSize = 2;

        IEnumerable<Product> query = Products;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(product =>
                product.Name.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase));
        }

        var filteredProducts = query.ToList();
        var items = filteredProducts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new
        {
            items,
            pagination = new
            {
                page,
                pageSize,
                totalItems = filteredProducts.Count,
                totalPages = (int)Math.Ceiling(
                    filteredProducts.Count / (double)pageSize)
            }
        };

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] int id)
    {
        var product = Products.FirstOrDefault(product => product.Id == id);

        if (product is null)
        {
            return NotFound(new
            {
                message = $"Product with id {id} was not found."
            });
        }

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    public ActionResult<Product> Create([FromBody] Product product)
    {
        var nextId = Products.Count == 0
            ? 1
            : Products.Max(existingProduct => existingProduct.Id) + 1;

        product.Id = nextId;
        Products.Add(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }
}