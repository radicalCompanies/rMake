
using Blazorise.RichTextEdit;
using Blazorise.RichTextEdit;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using rMakev2.ViewModel;
using rMakev2.Services;
using Blazored.Toast;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<RmakeViewModel>();

builder.Services.AddBlazoredToast();
builder.Services
    .AddBlazorise()
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services
    .AddBlazoriseRichTextEdit(options => { options.UseBubbleTheme = true; });

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
