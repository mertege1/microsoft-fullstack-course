using InventoryHub.Api.Data;
using InventoryHub.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string AllowAllCorsPolicy = "AllowAll";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=inventory.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(AllowAllCorsPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync())
    .WithName("GetProducts");

app.MapPost("/api/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/api/products/{product.Id}", product);
})
.WithName("CreateProduct");

// 1. Ürün Güncelleme (PUT)
app.MapPut("/api/products/{id}", async (int id, Product updatedProduct, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = updatedProduct.Name;
    product.Category = updatedProduct.Category;
    product.Price = updatedProduct.Price;
    product.Quantity = updatedProduct.Quantity;

    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UpdateProduct");

// 2. Ürün Silme (DELETE)
app.MapDelete("/api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteProduct");

app.Run();
