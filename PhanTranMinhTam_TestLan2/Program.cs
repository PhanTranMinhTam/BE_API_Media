using Microsoft.EntityFrameworkCore;
using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Mappings;
using PhanTranMinhTam_TestLan2.Repository;
using PhanTranMinhTam_TestLan2.Services;
//using PhanTranMinhTam_TestLan2.Services;

namespace PhanTranMinhTam_TestLan2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Thêm dịch vụ Controllers
            builder.Services.AddControllers();

            // Thêm dịch vụ phân quyền
            builder.Services.AddAuthorization();

            // Thêm dịch vụ xác thực
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //            ValidAudience = builder.Configuration["Jwt:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //        };
            //    });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingMusic));

            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
            builder.Services.AddScoped<IMusicServices, MusicServices>();
            //builder.Services.AddScoped<IDateRangesServices, DateRangeServices>();
            builder.Services.AddScoped<IPlayScheduleServices, PlayScheduleServices>();
            builder.Services.AddScoped<IRecurrenceRuleServices, RecurrenceRuleServices>();
            builder.Services.AddScoped<ITimeSlotServices, TimeSlotServices>();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();  // Ánh xạ các controller

            app.Run();
        }
    }
}
