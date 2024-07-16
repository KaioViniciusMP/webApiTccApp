using Microsoft.EntityFrameworkCore;
using webApiTcc.Application.IServices;
using webApiTcc.Application.Services;
using webApiTcc.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<webApiTccContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddCors();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
builder.Services.AddScoped<ITransacoesService, TransacoesService>();
builder.Services.AddScoped<ICartaoService, CartaoService>();
builder.Services.AddScoped<IAutenticacoesServices, AutenticacoesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
