using System.Text;
using System.Text.Json.Serialization;
using HealthcareApi.Data;
using HealthcareApi.Interface;
using HealthcareApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IMedicine, MedicineRepository>();
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<ICart, CartRepository>();
builder.Services.AddTransient<IPayment, PaymentRepository>();   
builder.Services.AddTransient<IOrder, OrderRepository>();

builder.Services.AddControllers();
var _policyName = "CorsPolicy";
builder.Services.AddCors((c) => {
    c.AddPolicy(_policyName,(builder) =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
});


builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
builder.Services.AddDbContext<EHealthDbContext>(
    //options => options.UseSqlServer(builder.Configuration.GetConnectionString("MySQL")));
    options => options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion));
var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ehealthapi"));

app.UseCors(_policyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
