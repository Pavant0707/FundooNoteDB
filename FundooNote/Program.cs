using BusinessLayer.Interface;
using BusinessLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logpath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logpath);
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisCacheUrl"]; });

builder.Services.AddDistributedMemoryCache();

// Add services to the container.
builder.Services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(builder.Configuration["ConnectionString:FundooDBB"]));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddTransient<IUserRL,UserRl>();
builder.Services.AddTransient<IUserBL, UserBL>();

builder.Services.AddTransient<INotesRL, NotesRL>();
builder.Services.AddTransient<INotesBL, NotesBL>();

builder.Services.AddTransient<IProductRL, ProductRL>();
builder.Services.AddTransient<IProductBL, ProductBL>();

builder.Services.AddTransient<ILabel, LabelRL>();
builder.Services.AddTransient<ILabelBL, LabelBL>();

builder.Services.AddTransient<ICollaboratorBL, CollaboratorBL>();
builder.Services.AddTransient<ICollaboratorRL, CollaboratorRL>();



builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {   
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.UseHealthCheck(provider);
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    options.Cookie.SameSite = SameSiteMode.None; // or SameSiteMode.Lax or SameSiteMode.Strict
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//});
builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("http://localhost:4200/", "https://localhost:7299/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
}));

builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();  

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseCors("ApiCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
