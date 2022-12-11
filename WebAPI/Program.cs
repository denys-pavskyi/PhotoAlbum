using AutoMapper;
using BuisnessLogicLayer;
using BuisnessLogicLayer.Helpers;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Handlers;
using WebAPI.Requirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = TokenHelper.Issuer,
        ValidAudience = TokenHelper.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret))

    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyNonBannedUser", policy => {

        policy.Requirements.Add(new UserStatusRequirement(UserStatus.Active));

    });
});
builder.Services.AddSingleton<IAuthorizationHandler, UserBannedStatusHandler>();

var connectionString = builder.Configuration.GetConnectionString("InternetPhotoAlbumDB");
builder.Services.AddDbContext<InternetPhotoAlbumDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperProfile());
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPhotoRatingService, PhotoRatingService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoTagService, PhotoTagService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ILoginService, LoginService>();


var app = builder.Build();


app.UseCors("AllowOrigin");
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
