using Email_Sender_API.MailRepository;
using Email_Sender_API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailServices, MailService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1" , new OpenApiInfo
                        {
                            Title = "Basic API",
                            Version = "v1" ,
                            Description = "An example of an email sender service using .NET webapi",
                            Contact = new OpenApiContact
                            {
                                Name = "Developer Contact",
                                Email = "Idoldev@gmail.com",

                            },
                        });
                    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
