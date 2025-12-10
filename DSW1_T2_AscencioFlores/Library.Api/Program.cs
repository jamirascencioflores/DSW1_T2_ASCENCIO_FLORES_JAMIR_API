using Library.Application.Interfaces;
using Library.Application.Mappings;
using Library.Application.Services;
using Library.Infrastructure; 

DotNetEnv.Env.Load(); 

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar CORS (Para que React pueda conectarse)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.AllowAnyOrigin() 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 2. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. INYECCIÓN DE DEPENDENCIAS (Arquitectura Hexagonal)

// A) Capa Infrastructure (Base de datos y Repositorios)
// Usamos el método de extensión que creamos antes
builder.Services.AddInfrastructure(builder.Configuration);

// B) Capa Application (Servicios y AutoMapper)
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>()); 
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Puedes comentarlo si te da problemas con certificados locales

// 5. Activar CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();