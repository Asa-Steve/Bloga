using System.Text.Json;
using Bloga.Data;
using Bloga.DTOs;
using Bloga.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloga.Endpoints;


class CategoryEndPoints
{
    // get all categories
    public static async Task<IResult> HandleGetAllCategories(BlogaDbContext ctx)
    {
        return Results.Ok(await ctx.Categories.ToListAsync());
    }

    public static async Task<IResult> HandleGetCategoryById(int id, BlogaDbContext ctx)
    {
        var foundCategory = await ctx.Categories.FindAsync(id);
        return foundCategory is null ? Results.NotFound() : Results.Ok(foundCategory);
    }

    public static async Task<IResult> HandleCreateCategory(CategoryCreateDto request, BlogaDbContext ctx)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name)) return Results.BadRequest(new { Message = "Invalid category name" });
            Category newCat = new() { Name = request.Name };
            await ctx.Categories.AddAsync(newCat);
            var res = await ctx.SaveChangesAsync();

            return res == 1 ? Results.Created() : Results.InternalServerError();

        }
        catch (Exception ex)
        {
            return Results.InternalServerError(new { Message = ex?.InnerException?.Message ?? ex?.Message });
        }
    }
}

/*

Method	Endpoint
GET	/api/categories
GET	/api/categories/{id}
POST	/api/categories
PUT	/api/categories/{id}
DELETE	/api/categories/{id}

*/