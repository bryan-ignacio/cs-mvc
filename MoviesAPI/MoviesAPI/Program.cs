using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.MoviesMapper;
using MoviesAPI.Repository;
using MoviesAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// agregamos los repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// agregamos el AutoMapper
builder.Services.AddAutoMapper(typeof(MoviesMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
