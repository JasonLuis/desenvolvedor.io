using BlazorWebApp.Client.Pages;
using BlazorWebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// configuração que indica que o Blazor Server e WebAssembly serão usados
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging(); //debugar o webassembly no Visual Studio
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // redirecionar requisições HTTP para HTTPS

app.UseStaticFiles(); // serve arquivos estáticos, como CSS, JS e imagens
app.UseAntiforgery(); // gera tokens antifalsificação para proteger contra ataques CSRF

app.MapRazorComponents<App>() // mapear os componentes Razor para o aplicativo
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebApp.Client._Imports).Assembly);

app.Run();
