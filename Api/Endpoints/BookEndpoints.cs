using Api.Contracts;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/books").WithTags("Books");
        
        group.MapPost("/", async (CreateBookRequest request, BooksContext context) =>
            {
                var book = new Book()
                {
                    Id = Guid.CreateVersion7(),
                    Title = request.Title,
                    Isbn = request.Isbn
                };
        
                context.Books.Add(book);
                await context.SaveChangesAsync();
        
                return Results.CreatedAtRoute("GetBook", new { book.Id }, book);
            })
            .WithName("CreateBook");
        
        group.MapGet("/{id:guid}", async (Guid id, BooksContext context) =>
            {
                var book = await context.Books.FindAsync(id);
        
                return book is not null ? Results.Ok(book) : Results.NotFound();
            })
            .WithName("GetBook");
        
        group.MapGet("", async (BooksContext context) =>
            {
                var books = await context.Books.ToListAsync();
        
                return Results.Ok(books);
            })
            .WithName("GetBooks");
        
        group.MapPut("/{id:guid}", async (Guid id, UpdateBookRequest request , BooksContext context) =>
            {
                var book = await context.Books.FindAsync(id);
        
                if (book is null) return Results.NotFound();
        
                book.Title = request.Title;
                await context.SaveChangesAsync();
        
                return Results.Ok(book);
            })
            .WithName("UpdateBook");
        
        group.MapDelete("/{id:guid}", async (Guid id, BooksContext context) =>
            {
                var book = await context.Books.FindAsync(id);
        
                if (book is null) return Results.NotFound();
        
                context.Books.Remove(book);
                await context.SaveChangesAsync();
        
                return Results.NoContent();
            })
            .WithName("DeleteBook");
        
        return app;
    }
}