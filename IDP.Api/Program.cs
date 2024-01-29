using IDP.Application.Context;
using IDP.Application.LogRepository;
using IDP.Application.Users.Comand;
using IDP.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#nullable disable
// Add services to the container.

builder.Services.AddControllers();

#region IOC
builder.Services.AddScoped<IIDPContext, IDPContext>();
builder.Services.AddScoped<ILogRepository, LogRipository>();
builder.Services.AddScoped(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
#endregion

#region Context
builder.Services.AddDbContext<IDPContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("IDPConnectionString")));
#endregion

#region JWT Config
builder.Services.AddAuthentication(op =>
{
    op.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(ConfigureOptions =>
    {
        ConfigureOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["JwtConfig:issuer"],
            ValidAudience = builder.Configuration["JwtConfig:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:key"])),
            ValidateIssuerSigningKey = true, // ==> چک کردن امضا
            ValidateLifetime = true // ==> استفاده از توکن با زمان معتبر

        };
        ConfigureOptions.SaveToken = true;     // ==> HttpContext.GetTokenAsync(); => بدست آوردن توکن در کنترلر ها برای استفاده از توکن
    });
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MediatR Config Assembly
builder.Services.AddMediatR((typeof(AddUserComand).Assembly));
#endregion

#region Middlewares
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
