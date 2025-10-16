using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ISubscriptionTypeRepository
    {
            public Task<IEnumerable<SubscriptionTypeDTO>> GetAllSubscriptions();
            public Task<SubscriptionTypeDTO> GetSubscriptionById(int id);
            public Task<SubscriptionTypeDTO> AddSubscription(SubscriptionTypeDTO subscriptionDto);
            public Task<SubscriptionTypeDTO> UpdateSubscription(SubscriptionTypeDTO subscriptionDto, int id);
            public Task<SubscriptionTypeDTO> DeleteSubscription(int id);

    }
}
