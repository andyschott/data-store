using data_store;
using DataStore.MongoDb;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

const string CorsPolicyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName,
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var mongoDbConfig = builder.Configuration.GetSection("MongoDb")
    .Get<MongoDbConfig>()
    ?? new MongoDbConfig();
builder.Services.AddMongoDb(mongoDbConfig);

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors(CorsPolicyName);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
