using MakeYourHomework.HomeworkService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<HomeworkDataContext>(opt =>
{
    //if (builder.Environment.IsDevelopment())
    //{
    //    opt.UseInMemoryDatabase("HomeworkInMemoryDataBase");
    //}
    //else
    //{
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
    //}
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();