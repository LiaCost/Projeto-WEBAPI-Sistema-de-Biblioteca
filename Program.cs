using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WEB_API_LIA.ORM;
using WEB_API_LIA.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicione o contexto do banco de dados
builder.Services.AddDbContext<BdBibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione o repositório FuncionarioRepositorio
builder.Services.AddScoped<FuncionarioRepositorio>();

// Adicione o repositório CategoriaRepositorio
builder.Services.AddScoped<CategoriaRepositorio>();

// Adicione o repositório CategoriaRepositorio
builder.Services.AddScoped<LivroRepositorio>();

// Adicione o repositório MembroRepositorio
builder.Services.AddScoped<MembroRepositorio>();

// Adicione o repositório ReservaRepositorio
builder.Services.AddScoped<ReservaRepositorio>();

// Adicione o repositório EmprestimoRepositorio
builder.Services.AddScoped<EmprestimoRepositorio>();

// Adicione o repositório UsuarioRepositorio
builder.Services.AddScoped<UsuarioRepositorio>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MARVELS", Version = "v1" });

    // Configura o Swagger para usar o Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato **Bearer {seu_token}**",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configuração de autenticação JWT
var key = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6"; // 32 caracteres para 256 bits
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:7025", // O emissor deve corresponder ao que você definiu no token
        ValidAudience = "http://localhost:7025", // A audiência deve corresponder ao que você definiu no token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // Usando a chave
    };
});


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
