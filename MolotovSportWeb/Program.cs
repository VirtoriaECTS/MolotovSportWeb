using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Components;
using MolotovSportWeb.Components.Classes.Servers;
using MolotovSportWeb.Models;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Добавляем IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddScoped<UserStateService>();

builder.Services.AddScoped<YandexGeocoderService>();

builder.Services.AddMudServices();

//Подключение бд
builder.Services.AddDbContext<MolotovSportWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
