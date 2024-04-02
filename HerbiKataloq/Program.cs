using HerbiKataloq.DatabaseConnection;
using HerbiKataloq.Services.TankServices;
using HerbiKataloq.Services.TeyyareServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoDbServerTenzimlemeleri>(
        builder.Configuration.GetSection("MongoDB")
    );

builder.Services.AddSingleton<HerbiKataloqDbConnection>();
builder.Services.AddScoped<ITeyyareService, TeyyareService>();
builder.Services.AddScoped<ITankService, TankService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=teyyareler}/{action=index}/{id?}"
    );

app.Run();
