using CleanEx.Repositories.Extensions;
using CleanEx.Services;
using CleanEx.Services.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter=true);
var app = builder.Build();
//bu bir web projesinde olsaydý app.UseExceptionHandler(/erros/); gibi bir yapýyla sayfaya yönlendirilebilird.
app.UseExceptionHandler(x => { });
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
