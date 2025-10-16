using StudioUp.DTO;
using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerSubscriptionRepository
    {
        Task<IEnumerable<CustomerSubscription>> GetAllCustomerSubscriptionsAsync();
        Task<CustomerSubscriptionDTO> GetCustomerSubscriptionByIdAsync(int id);
        Task<CustomerSubscriptionDTO> AddCustomerSubscriptionAsync(CustomerSubscriptionDTO subscription);
        Task UpdateCustomerSubscriptionAsync(CustomerSubscriptionDTO subscription);
        Task DeleteCustomerSubscriptionAsync(int id);
        Task<List<CustomerSubscriptionDTO>> GetCustomerSubscriptionsByCustomerIdAsync(int customerId);
    }

}
