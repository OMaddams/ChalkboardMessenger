using ChalkboardMessenger.App.Services;
using ChalkboardMessenger.Data.Database;
using ChalkboardMessenger.Data.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection");
var messagesConnectionString = builder.Configuration.GetConnectionString("MessagesConnection");

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(authConnectionString, b => b.MigrationsAssembly("ChalkboardMessenger.UI"));

});
builder.Services.AddDbContext<MessagesDbContext>(options => options.UseSqlServer(messagesConnectionString, b => b.MigrationsAssembly("ChalkboardMessenger.UI")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAuthorization(options => options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin")));

builder.Services.AddScoped<IMessagesRepo, MessagesRepo>();

builder.Services.AddScoped<MessagesManager>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Member");
    options.Conventions.AuthorizeFolder("/Admin", "AdminPolicy");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
