using HostingEnvironmentExtensions = Microsoft.Extensions.Hosting.HostingEnvironmentExtensions;

namespace TutorTracker.Api.Managers;

using System.Linq.Expressions;
using Repositories;
using Entities;
using Parsing;

public class CustomerManager : ICustomerManager
{
    private readonly IRepository _repository;
    private readonly ILogger<CustomerManager> _logger;
    private readonly IDateParser _dateParser;

    public CustomerManager(IRepository repository, ILogger<CustomerManager> logger, IDateParser dateParser)
    {
        _repository = repository;
        _logger = logger;
        _dateParser = dateParser;
    }

    public async Task<Guid?> CreateCustomerAsync(Customer customer, CancellationToken token)
    {
        try
        {
            return await _repository.SaveCustomerAsync(customer, token) ? customer.Id : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create customer");
            throw;
        }
    }

    public async Task<Customer?> GetCustomerAsync(Guid customerId, CancellationToken token)
    {
        try
        {
            return await _repository.GetCustomerAsync(customerId, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not find customer with id {id}", customerId);
            throw;
        }
    }

    public Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken token)
    {
        try
        {
            return _repository.GetCustomersAsync(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get customers");
            throw;
        }
    }

    public Task<IEnumerable<Customer>> GetCustomersAsync(Expression<Func<Customer, bool>> predicate, CancellationToken token)
    {
        try
        {
            return _repository.GetCustomersAsync(predicate, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get customers");
            throw;
        }
    }

    public async Task<IEnumerable<Lesson>> GetLessonsAssociatedWithCustomer(Guid customerId, int? month, int? year,
        CancellationToken token)
    {
        try
        {
            var period = _dateParser.GetPeriod(month, year);
            return await _repository.GetLessonsAssociatedWithCustomerAsync(customerId, period.Start, period.End, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get lessons associated with customer with customer id {customerId}",
                customerId);
            throw;
        }
    }
}