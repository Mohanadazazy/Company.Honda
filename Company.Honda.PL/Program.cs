using Company.Honda.BLL;
using Company.Honda.BLL.Interfaces;
using Company.Honda.BLL.Repositories;
using Company.Honda.DAL.Data.Contexts;
using Company.Honda.PL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Company.Honda.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Life Time
            // builder.Service.AddScoped();     // Create Object Life Time Per Request - UnReachable Object
            // builder.Service.AddTransient();  // Create Object Life Time Per Operation
            // builder.Service.AddSigelton();   // Create Object Life Time Per Application

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            
        }
    }
}
