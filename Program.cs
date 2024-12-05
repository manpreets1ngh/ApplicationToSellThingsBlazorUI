using ApplicationToSellThings.BlazorUI.Helper;
using ApplicationToSellThings.BlazorUI.Middleware;
using ApplicationToSellThings.BlazorUI.Services;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using Fluxor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("ApplicationToSellthingsAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5258");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IResetForgotPasswordService, ResetForgotPasswordService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<UserDetailHelper>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools(); // Enable Redux DevTools
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JWT:Secret"]))
        };
    });
    /*.AddCookie("Cookies", options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "AuthToken";
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth/accessdenied";

    });*/

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
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

app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Use(async (context, next) =>
{
    if (context.Response.StatusCode == 403) // Forbidden
    {
        context.Response.Redirect("/accessdenied");
    }
    else if (context.Response.StatusCode == 401) // Unauthorized
    {
        context.Response.Redirect("/auth/login"); // Or another login page
    }
    else
    {
        await next();
    }
});
app.Run();
