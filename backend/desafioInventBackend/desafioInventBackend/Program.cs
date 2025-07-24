
using DesafioInventBackend.Data;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddSingleton<RavenDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryEquipamentoEletronico, RavenDbRepository>();
builder.Services.AddScoped<EquipamentoEletronicoValidator>();
builder.Services.AddScoped<EquipamentoEletronicoAlterarValidator>();
builder.Services.AddScoped<EquipamentoEletronicoDeleteValidator>();
builder.Services.AddScoped<EquipamentoEletronicoService>();

builder.Services.AddAutoMapper(cfg =>
    {
        cfg.CreateMap<EquipamentoEletronico, EquipamentoEletronicoDTO>().ReverseMap();
    }
);

builder.Services.AddSwaggerGen();

var app = builder.Build();

var env = app.Environment;

if (env.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "api/equipamento-eletronico");

app.MapFallbackToFile("index.html");
app.MapControllers();

app.Run();
