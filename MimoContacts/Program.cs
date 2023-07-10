using Microsoft.EntityFrameworkCore;
using MimoContacts.Data;
using MimoContacts.Services.Contacts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<MimoContactsContext>(
            options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("MimoContactsContext"))
                .UseSnakeCaseNamingConvention()
    );
    builder.Services.AddScoped<IContactService, ContactService>();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/error");
    app.UseStaticFiles();
    app.UseRouting();
    app.MapControllers();
    app.MapFallbackToFile("index.html");
    app.Run();
}