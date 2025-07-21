
using DesafioInventBackend.Data;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddSingleton<RavenDbContext>();

builder.Services.AddControllers();
builder.Services.AddScoped<IRepositoryEquipamentoEletronico<EquipamentoEletronico>, RavenDbRepository<EquipamentoEletronico>>();
builder.Services.AddScoped<EquipamentoEletronicoService>();

builder.Services.AddAutoMapper(cfg =>
    {
        cfg.CreateMap<EquipamentoEletronico, EquipamentoEletronicoDTO>().ReverseMap();
    }
);

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
