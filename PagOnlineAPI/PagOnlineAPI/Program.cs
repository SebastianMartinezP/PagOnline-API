using AutoMapper;
using PagOnlineAPI;

#region Services Container

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PagOnlineAPI.Models.ModelContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// SwaggerUI
builder.Services.AddSwaggerGen();

// AutoMapper 
MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper); 
builder.Services.AddMvc();

#endregion

#region Build WebApp

WebApplication app = builder.Build();

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

#endregion