using AspNetMvcWizardSample.DataAccess;

//uncomment this to enable logging
//using Microsoft.AspNetCore.HttpLogging; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IOrdersDataAccess, InMemoryOrderDataAccess>();
builder.Services.AddSingleton<IGiftsDataAccess, HardcodedGiftsDataAcess>();

//emulate an actual authentication provider, by hardcoding a stub with the value "42"
builder.Services.AddSingleton<IAuthenticationProvider>((_)=>new AuthenticationProviderStub(42));

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
