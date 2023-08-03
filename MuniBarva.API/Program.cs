using MuniBarva.DAO;
using MuniBarva.DAO.Interfaces;
using MuniBarva.SERVICES;
using MuniBarva.SERVICES.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro de todas las dependencias
builder.Services.AddScoped<ILoginDAO, LoginDAO>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddSingleton<MasterDAO>();

var app = builder.Build();

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
