using AutoMapper;
using BuisnessLogicLayer;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

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
//builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddTransient<ITagService>(cs =>
            new TagService(cs.GetService<IUnitOfWork>(), cs.GetService<IMapper>())
            );
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPhotoRatingService, PhotoRatingService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoTagService, PhotoTagService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();


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
