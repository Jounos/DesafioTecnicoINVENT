using DesafioInventBackend.Context;
using DesafioInventBackend.Data;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
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
builder.Services.AddAutoMapper(cfg => cfg.CreateMap<EquipamentoEletronico, RetornoEquipamentoEletronicoDto>());

builder.Services.AddDbContext<DataContext>(
    opt =>
    {
        opt.UseInMemoryDatabase("EquipamentoEletronicoDB");
        opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);

builder.Services.AddSingleton<RavenDbContext>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
