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
}
