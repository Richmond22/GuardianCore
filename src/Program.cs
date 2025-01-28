using GuardianCore.Data;
using GuardianCore.Handler;
using GuardianCore.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection(DatabaseOptions.ConfigurationSection));

var dbOptions = builder.Configuration.GetSection(DatabaseOptions.ConfigurationSection).Get<DatabaseOptions>();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(dbOptions.ConnectionString));
 
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<InternalServerExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/identity").MapIdentityApi<IdentityUser>();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await db.Database.MigrateAsync();


app.UseExceptionHandler();
    
await app.RunAsync();

