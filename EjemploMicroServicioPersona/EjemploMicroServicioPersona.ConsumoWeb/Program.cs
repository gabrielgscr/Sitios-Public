var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// HttpClient para consumir el microservicio de Persona
builder.Services.AddHttpClient<EjemploMicroServicioPersona.ConsumoWeb.Services.IPersonaApiClient, EjemploMicroServicioPersona.ConsumoWeb.Services.PersonaApiClient>(client =>
{
    var baseUrl = builder.Configuration["PersonaApi:BaseUrl"]
                  ?? throw new InvalidOperationException("PersonaApi:BaseUrl no configurado");
    client.BaseAddress = new Uri(baseUrl);
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
