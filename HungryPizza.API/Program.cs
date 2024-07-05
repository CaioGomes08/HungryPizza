using HungryPizza.Application.Services;
using HungryPizza.Domain.Repositories;
using HungryPizza.Infraestructure.DataContext;
using HungryPizza.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<HungryPizzaDbContext>(
		options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

// Repositories
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddTransient<ISaborRepository, SaborRepository>();
builder.Services.AddTransient<PedidoService>();

var app = builder.Build();

// Inicializa e aplica as migrations no DB
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<HungryPizzaDbContext>();
	db.Database.Migrate();
	InitializeDataBase.Init(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
