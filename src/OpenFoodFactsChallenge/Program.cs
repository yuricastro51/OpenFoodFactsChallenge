using Microsoft.EntityFrameworkCore;
using OpenFoodFactsChallenge.Domain.Repositories;
using OpenFoodFactsChallenge.Domain.Services;
using OpenFoodFactsChallenge.Infrastructure.Contexts;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGetProductByCodeRepository, ProductRepository>();
builder.Services.AddScoped<IAddProductRepository, ProductRepository>();
builder.Services.AddScoped<IGetProductListRepository, ProductRepository>();
builder.Services.AddScoped<IGetTotalProductsRepository, ProductRepository>();
builder.Services.AddScoped<IWebScrapingService, WebScrapingService>();
builder.Services.AddScoped<IInsertScrapedProductService, InsertScrapedProductsService>();

builder.Services.AddMediatR((cfg => cfg.RegisterServicesFromAssemblyContaining<Program>()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMongoDB(
        connectionString!,
        "OpenFoodFactsChallenge"));

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("ScheduleService");

    q.AddJob<ScheduleService>(options => options.WithIdentity(jobKey));

    q.AddTrigger(options => options
           .ForJob(jobKey)
           .WithIdentity("ScheduleService-trigger")
           .WithCronSchedule("0 11 10 1/1 * ? *", 
               scheduleBuilder => scheduleBuilder.InTimeZone(TimeZoneInfo.Local)));
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
