using m01_labMedicine.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = "Server=DESKTOP-6HE46OL\\SQLEXPRESS;Database=labmedicinebd;Trusted_Connection=True;TrustServerCertificate=True;";
//Injeção de Dependencia do Context
builder.Services.AddDbContext<AtendimentoMedicoContext>(o => o.UseSqlServer(connectionString));

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