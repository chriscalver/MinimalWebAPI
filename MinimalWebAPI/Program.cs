using MinimalAPITutorial;
using MinimalWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
async Task<List<Restaurant>> GetAllRestaurants(DataContext context) =>
    await context.Restaurants.ToListAsync();

app.MapGet("/", () => "Welcome to Restaurant API");



app.MapGet("/restaurant", async (DataContext context) =>
    await context.Restaurants.ToListAsync());


app.MapGet("/restaurant/{id}", async (DataContext context, int id) =>
    await context.Restaurants.FindAsync(id) is Restaurant hero ?
        Results.Ok(hero) :
        Results.NotFound("Sorry, hero not found. :/"));

app.MapPost("/restaurant", async (DataContext context, Restaurant hero) =>
{
    context.Restaurants.Add(hero);
    await context.SaveChangesAsync();
    return Results.Ok(await GetAllRestaurants(context));
});

app.MapPut("/restaurant/{id}", async (DataContext context, Restaurant hero, int id) =>
{
    var dbHero = await context.Restaurants.FindAsync(id);
    if (dbHero == null) return Results.NotFound("No hero found. :/");

    dbHero.Rating = hero.Rating;
    dbHero.RestaurantName = hero.RestaurantName;
    dbHero.Selection = hero.Selection;
    dbHero.Comment = hero.Comment;
    dbHero.PictureUrl = hero.PictureUrl;

    await context.SaveChangesAsync();

    return Results.Ok(await GetAllRestaurants(context));
});

app.MapDelete("/restaurant/{id}", async (DataContext context, int id) =>
{
    var dbHero = await context.Restaurants.FindAsync(id);
    if (dbHero == null) return Results.NotFound("Who's that?");

    context.Restaurants.Remove(dbHero);
    await context.SaveChangesAsync();

    return Results.Ok(await GetAllRestaurants(context));
});






app.Run();