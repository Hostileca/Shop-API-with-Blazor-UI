using WebBlazor.Components;
using WebBlazor.Components.Pages.Forms;
using WebBlazor.Service;

namespace WebBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<FormService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<MessagesService>();


            var app = builder.Build();
            app.UseExceptionHandler("/Error");

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
