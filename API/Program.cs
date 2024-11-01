using Microsoft.EntityFrameworkCore;
using CryptoTrade.Business;
using CryptoTrade.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserEFRepository>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ICryptoRepository, CryptoEFRepository>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockRepository, StockEFRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionEFRepository>();

var connectionString = builder.Configuration.GetConnectionString("ServerDB_localhost");

builder.Services.AddDbContext<CryptoTradeContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

// app.Use(async (context, next) =>
// {
//     if (context.Request.Path == "/")
//     {
//         context.Response.Redirect("/swagger");
//         return;
//     }
//     await next();
// });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
