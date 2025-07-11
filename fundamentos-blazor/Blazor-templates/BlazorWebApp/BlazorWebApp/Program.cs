using BlazorWebApp.Client.Pages;
using BlazorWebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// configura��o que indica que o Blazor Server e WebAssembly ser�o usados
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

app.UseHttpsRedirection(); // redirecionar requisi��es HTTP para HTTPS

app.UseStaticFiles(); // serve arquivos est�ticos, como CSS, JS e imagens
app.UseAntiforgery(); // gera tokens antifalsifica��o para proteger contra ataques CSRF

app.MapRazorComponents<App>() // mapear os componentes Razor para o aplicativo
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebApp.Client._Imports).Assembly);

app.Run();
