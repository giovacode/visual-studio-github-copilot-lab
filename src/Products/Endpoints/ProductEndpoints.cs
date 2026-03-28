using DataEntities;
using Microsoft.EntityFrameworkCore;
using Products.Data;

namespace Products.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product");

        group.MapGet("/", async (ProductDataContext db) =>
        {
            return await db.Product.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, ProductDataContext db) =>
        {
            return await db.Product.FindAsync(id)
                is Product product
                    ? Results.Ok(product)
                    : Results.NotFound();
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (Product product, ProductDataContext db) =>
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return Results.BadRequest("Product name is required");

            db.Product.Add(product);
            await db.SaveChangesAsync();

            return Results.Created($"/api/Product/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="inputProduct">The updated product data.</param>
        /// <param name="db">The database context.</param>
        /// <returns>The updated product or a not found result.</returns>
        group.MapPut("/{id}", async (int productId, Product inputProduct, ProductDataContext db) =>
        {
            var product = await db.Product.FindAsync(productId);

            if (product is null)
                return Results.NotFound();

            if (string.IsNullOrWhiteSpace(inputProduct.Name))
                return Results.BadRequest("Product name is required");

            product.Name = inputProduct.Name;
            product.Description = inputProduct.Description;
            product.Price = inputProduct.Price;
            product.ImageUrl = inputProduct.ImageUrl;

            db.Product.Update(product);
            await db.SaveChangesAsync();

            return Results.Ok(product);
        })
        .WithName("UpdateProduct")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id}", async (int id, ProductDataContext db) =>
        {
            var product = await db.Product.FindAsync(id);

            if (product is null)
                return Results.NotFound();

            db.Product.Remove(product);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
