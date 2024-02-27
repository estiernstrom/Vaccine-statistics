using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.HeadModel;
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Utför Api-anrop som lagras i Jsonfiler 

//await ApiClient.GetDesoInfo();
//await ApiClient.GetVaccinationFromDesoCode();
//await ApiClient.GetGenderAgeTotalFromSCBApi();
//await ApiClient.GetAgeGenderByDesoFromSCBApi();
//await ApiClient.GetBatchesInfo();
//await ApiClient.GetSiteInfo();
//await ApiClient.GetBackgroundByDesoFromSCBApi();
//await ApiClient.VaccinationCount();


Singleton singleton = Singleton.GetSingleton();

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
