namespace TutorTracker.Api.Managers;

using Repositories;
using Entities;
using Parsing;
using M = Model;

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
    
    public Task<IEnumerable<Customer>> GetCustomersAsync(string? firstName, string? lastName, CancellationToken token)
    {
        try
        {
            return _repository.GetCustomersAsync(firstName, lastName, token);
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

    public async Task<Customer?> UpdateCustomerAsync(M.UpdateCustomer customer, CancellationToken token)
    {
        try
        {
            return await _repository.UpdateCustomerAsync(customer, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not update customer with customer id {customerId}",
                customer.Id);
            throw;
        }
    }
}