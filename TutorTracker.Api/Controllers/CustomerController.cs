
namespace TutorTracker.Api.Controllers;

using Managers;
using AutoMapper;
using M = Model;
using E = Entities;

public class CustomerController
{
    private readonly ICustomerManager _customerManager;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerManager customerManager, IMapper mapper)
    {
        _customerManager = customerManager;
        _mapper = mapper;
    }

    public async Task<IResult> GetCustomerAsync(Guid customerId, CancellationToken token)
    {
        try
        {
            var customer = await _customerManager.GetCustomerAsync(customerId, token);
            return customer is null ? Results.BadRequest() : Results.Ok(_mapper.Map<M.CustomerResult>(customer));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public async Task<IResult> GetCustomersAsync(string? firstName, string? lastName, CancellationToken token)
    {
        try
        {
            IEnumerable<E.Customer> customers;
            if (firstName is not null && lastName is not null)
            {
                customers = await _customerManager.GetCustomersAsync(x => 
                    string.Equals(x.FirstName.ToLower(), firstName.ToLower()) &&
                    string.Equals(x.LastName.ToLower(), lastName.ToLower()), token);
            }
            else if (firstName is null && lastName is not null)
            {
                customers = await _customerManager.GetCustomersAsync(x => 
                    string.Equals(x.LastName.ToLower(), lastName.ToLower()), token);
            }
            else if (firstName is not null && lastName is null)
            {
                customers = await _customerManager.GetCustomersAsync(x => 
                    string.Equals(x.FirstName.ToLower(), firstName.ToLower()), token);
            }
            else
            {
                customers = await _customerManager.GetCustomersAsync(token);
            }
            return Results.Ok(customers.Select(x => _mapper.Map<M.CustomerResult>(x)));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public async Task<IResult> GetLessonsAssociatedWithCustomer(Guid customerId, int? month, int? year,
        CancellationToken token)
    {
        try
        {
            var lessons = (await _customerManager.GetLessonsAssociatedWithCustomer(customerId, month, year, token))
                .Select(x => _mapper.Map<M.LessonResult>(x));
            return Results.Ok(lessons);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public async Task<IResult> CreateCustomerAsync(Model.CreateCustomer createCustomer, CancellationToken token)
    {
        try
        {
            var id = await _customerManager.CreateCustomerAsync(_mapper.Map<Entities.Customer>(createCustomer), token);
            return id is null ? Results.BadRequest() : Results.Ok(id);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}