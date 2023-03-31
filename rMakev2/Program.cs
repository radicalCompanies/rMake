
using Blazorise.RichTextEdit;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using rMakev2.ViewModel;
using rMakev2.Services;
using Blazored.Toast;
using MudBlazor.Services;
using Doodle;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<RmakeViewModel>();
builder.Services.AddScoped<RobinVM>();
builder.Services.AddScoped<AIChat>();

builder.Services.AddBlazoredToast();
builder.Services
    .AddBlazorise()
    .AddBootstrapProviders()
    .AddFontAwesomeIcons()
    .UseDoodle((config) =>
    {
        config.DefaultStrokeColor = "#FF0000";
        config.DefaultStrokeSize = 2;
        config.ToolbarConfig.ShowToolbar = true;
        config.ToolbarConfig.ShowDrawActionsUndo = true;
        config.ToolbarConfig.ShowColorPicker = true;

        config.Theme = new Doodle.Themes.Default();

        config.BackgroundConfig.BackgroundSources = new List<Doodle.Abstractions.Models.BackgroundData>()
            {
                new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Back", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-back.png" },
                new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Front", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-front.png" },
                new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Side", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-side.png" },
                new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Top", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-top.png" },
           
        };

        config.CanvasConfig.ResizableImages = new List<Doodle.Abstractions.Models.ResizableImageSource>()
            {
                new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Signature", DataSource = "https://upload.wikimedia.org/wikipedia/commons/f/f8/Arnaldo_Tamayo_Signature.svg" },
                new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Inspection Passed", DataSource = "./_content/STDoodle.DemoComponents/img/inspection-passed.svg" },
                new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Inspection Failed", DataSource = "./_content/STDoodle.DemoComponents/img/inspection-failed.svg" }
            };

    });

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
