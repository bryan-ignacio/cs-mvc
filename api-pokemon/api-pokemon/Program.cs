using api_pokemon.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPokemonService, PokemonService>();

builder.Services.AddHttpClient<IPokemonService, PokemonService>(
    c =>
    {
        c.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// este es el punto inicial de la App.
// el controlador es InicioController y el metodo a accionar es Index()
app.MapControllerRoute(name: "default", pattern: "{controller=Inicio}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
