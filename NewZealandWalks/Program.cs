using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using NewZealandWalks.Data;
using NewZealandWalks.Mapping;
using NewZealandWalks.MiddleWares;
using NewZealandWalks.Repository;
using NewZealandWalks.Repository.Interfaces;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Nzwalks.txt",rollingInterval:RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(options =>{ 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("NZWalksConnectionString"),
        sqloptions=>sqloptions.CommandTimeout(60)
        );
});
builder.Services.AddDbContext<NzWalksAuthDbContext>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"),
        sqloptions=>sqloptions.CommandTimeout(60)
        );
});
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkReposotory,WalkRepository>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NzWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>
(
    options => 
    { 
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Uploads")),
    RequestPath= "/Uploads"
});

app.MapControllers();

app.Run();
