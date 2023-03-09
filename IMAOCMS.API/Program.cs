using Autofac;
using Autofac.Extensions.DependencyInjection;
using IMAOCMS.API.Business;
using IMAOCMS.API.Filters;
using IMAOCMS.API.Modules;
using IMAOCRM.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container => container.RegisterModule(new RepoServiceModule()));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));


builder.Services.AddSingleton<RFIDWorker>();
//builder.Services.AddHostedService(op => op.GetRequiredService<RFIDWorker>());

var app = builder.Build();
using (var context = new AppDbContext()) { context.Database.Migrate(); }
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("api/stopTimer", async (ILoggerFactory loggerFactory, IServiceProvider serviceProvider) =>
{
    var timer2Service = serviceProvider.GetRequiredService<RFIDWorker>();
    await timer2Service.StopAsync(CancellationToken.None);
    return "success";
});

app.MapGet("api/startTimer", async (ILoggerFactory loggerFactory, IServiceProvider serviceProvider) =>
{
    var timer2Service = serviceProvider.GetRequiredService<RFIDWorker>();
    await timer2Service.StartAsync(CancellationToken.None);
    return "success";
});
app.Run();
