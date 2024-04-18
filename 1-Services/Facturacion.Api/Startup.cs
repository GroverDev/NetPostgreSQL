using Facturacion.Domain;

namespace Facturacion.Api;

public class Startup
{
    const string MisCORS = "MisCors";
    public Startup(IConfiguration configuration)
    {
        _Configuration = configuration;
    }
    public IConfiguration _Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: MisCORS,
                builder =>
                {
                    builder.WithOrigins("*");
                    builder.WithHeaders("*");
                    builder.WithMethods("*");
                });
        });


        var appSettingsSection = _Configuration.GetSection("ConfigSiat");
        services.Configure<ConfigSiat>(appSettingsSection);
    }

}
// public class startup
// {

//     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//         if (env.IsDevelopment())
//         {
//             app.UseDeveloperExceptionPage();
//             app.UseSwagger();
//             app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicios.Api v1"));
//         }
//         // Define la culura de la aplicación
//         #region
//         var culture = CultureInfo.CreateSpecificCulture("es-BO");
//         var dateformat = new DateTimeFormatInfo
//         {
//             ShortDatePattern = "dd/MM/yyyy",
//             LongDatePattern = "dd/MM/yyyy HH:mm:ss"
//         };
//         culture.DateTimeFormat = dateformat;

//         var supportedCultures = new[]
//         {
//                 culture
//             };

//         app.UseRequestLocalization(new RequestLocalizationOptions
//         {
//             DefaultRequestCulture = new RequestCulture(culture),
//             SupportedCultures = supportedCultures,
//             SupportedUICultures = supportedCultures
//         });
//         #endregion
//         app.UseRouting();

//         app.UseAuthentication();
//         app.UseAuthorization();

//         app.UseCors(MisCORS);

//         app.UseEndpoints(endpoints =>
//         {
//             endpoints.MapGet("/", async context =>
//             {
//                 await context.Response.WriteAsync("App ok");
//             });
//             endpoints.MapControllers();
//         });
//     }
// }

