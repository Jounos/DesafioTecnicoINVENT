using DesafioInventBackend.Context;
using DesafioInventBackend.Model.Mapper;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Repository.Contract;
using DesafioInventBackend.Service;
using DesafioInventBackend.Service.Contract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


builder.Services.AddControllers();
builder.Services.AddScoped<IEquipamentoEletronicoService, EquipamentoEletronicoService>();
builder.Services.AddScoped<IEquipamentoEletronicoRepository, EquipamentoEletronicoRepository>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(
    opt =>
    {
        opt.UseInMemoryDatabase("EquipamentoEletronicoDB");
        opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);


builder.Services.AddAutoMapper(typeof(DtoMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}

app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
