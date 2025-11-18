using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontApp",
        policy => policy.WithOrigins(["https://localhost:44400", "http://localhost:55500"])
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});

builder.Services.AddSingleton<IDocumentStore>(provider =>
{
    var store = new DocumentStore()
    {
        Urls = new[] { Environment.GetEnvironmentVariable("ravenDbServer") },

        Database = Environment.GetEnvironmentVariable("ravenDbName"),
    }.Initialize();
    return store;
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDocumentSession>(provider =>
{
    var store = provider.GetRequiredService<IDocumentStore>();
    return store.OpenSession();

});
builder.Services.AddScoped<IRepositoryEquipamentoEletronico, RavenDbRepository>();
builder.Services.AddScoped<EquipamentoEletronicoCadastrarValidator>();
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

app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "api/equipamento-eletronico");

app.MapFallbackToFile("index.html");
app.MapControllers();

app.UseCors("AllowFrontApp");

app.Run();
