using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{


    public class TrainingCustomerRepository : ITrainingCustomerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public TrainingCustomerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrainingCustomerDTO>> GetAllTrainingCustomers()
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(t => t.IsActive).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Customers List.", ex);
            }
        }
        public async Task<List<CalanderAvailableTrainingDTO>> GetAllRegisteredTrainingsDetailsAsync()
        {
            try
            {
                var trainings = await _context.TrainingCustomers.Where(x => x.IsActive)      
                      .Include(tc => tc.Customer)
                            .ThenInclude(c => c.CustomerType)
                       .Include(tc => tc.Training)
                             .ThenInclude(at => at.Training)
                                .ThenInclude(t => t.Trainer)
                        .Include(tc => tc.Training)
                            .ThenInclude(at => at.Training)
                                .ThenInclude(t => t.TrainingCustomerType)
                                    .ThenInclude(tct => tct.TrainingType)
                        .ToListAsync();
                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(trainings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TrainingCustomerDTO> GetTrainingCustomerById(int id)
        {
            try
            {
                var c = await _context.TrainingCustomers.Where(t => t.ID == id && t.IsActive).FirstOrDefaultAsync();
                return _mapper.Map<TrainingCustomerDTO>(c);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Type.\", ex);\r\n.", ex);
            }
        }


        public async Task<List<TrainingCustomerDTO>> GetTrainingCustomerByTrainingId(int id)
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(tc => tc.TrainingID == id).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<TrainingCustomerDTO>> GetTrainingCustomerByCustomerId(int id)
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(tc => tc.CustomerID == id).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TrainingCustomerDTO> AddTrainingCustomer(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                if (trainingCustomer == null)
                {
                    throw new Exception("Training Customer cannot be null");
                }
                var mapCast = _mapper.Map<TrainingCustomer>(trainingCustomer);
                mapCast.IsActive = true;
                var newTrainingCustomer = await _context.TrainingCustomers.AddAsync(mapCast);
                await _context.SaveChangesAsync();
                trainingCustomer.ID = newTrainingCustomer.Entity.ID;
                return trainingCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add the Training Customer.", ex);
            }
        }

        public async Task UpdateTrainingCustomer(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                var trainingCustomerToUpdate = await _context.TrainingCustomers.FirstOrDefaultAsync(customerToUpdate => customerToUpdate.ID == trainingCustomer.ID);

                if (trainingCustomerToUpdate == null)
                {
                    throw new Exception("Training Customer not found.");
                }
                _mapper.Map(trainingCustomer, trainingCustomerToUpdate);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to update the Training Customer.", ex);
            }
        }

        public async Task DeleteTrainingCustomer(int id)
        {
            try
            {
                var c = await _context.TrainingCustomers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null || !c.IsActive)
                {
                    return;
                }
                c.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to delete the Training Customer.", ex);
            }
        }
        public async Task<List<CalanderAvailableTrainingDTO>> FilterAsync(CalanderAvailableTrainingFilterDTO filter)
        {
            try
            {
                var query = _context.TrainingCustomers
                    .Include(x => x.Customer)
                        .ThenInclude(c => c.CustomerType)
                    .Include(x => x.Training)
                        .ThenInclude(t => t.Training)
                        .ThenInclude(tr => tr.Trainer)
                    .Include(x => x.Training)
                        .ThenInclude(t => t.Training)
                        .ThenInclude(t => t.TrainingCustomerType)
                        .ThenInclude(tct => tct.TrainingType)
                    .AsQueryable();

                var queryTraininigs = _context.Trainings.AsQueryable();

                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
                //  DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
                DateTime todayDateTime = DateTime.Now;

                if (filter.StratDate.HasValue && filter.EndDate.HasValue && filter.StratDate.Value > filter.EndDate.Value)
                {
                    throw new ArgumentException("StartDate cannot be greater than EndDate.");
                }

                if (filter.Future.HasValue && filter.Past.HasValue)
                {
                    // כל האימונים (עבר ועתיד), ללא תאריכים מוגדרים
                    if (filter.Past.Value == filter.Future.Value && !filter.StratDate.HasValue && !filter.EndDate.HasValue)
                    {
                        // ללא פילטרים נוספים
                    }
                    // כל האימונים בטווח תאריכים מוגדר
                    else if (filter.Past.Value == filter.Future.Value && filter.StratDate.HasValue && filter.EndDate.HasValue)
                    {
                        ApplyDateRangeFilter(ref query, filter.StratDate, filter.EndDate);
                    }
                    // כל האימונים מהתחלה מוגדרת (עד תאריך סיום כלשהו)
                    else if (filter.Past.Value == filter.Future.Value && filter.StratDate.HasValue)
                    {
                        ApplyDateRangeFilter(ref query, filter.StratDate, null);
                    }
                    // כל האימונים עד תאריך סיום מוגדר
                    else if (filter.Past.Value == filter.Future.Value && filter.EndDate.HasValue)
                    {
                        ApplyDateRangeFilter(ref query, null, filter.EndDate);
                    }
                    // אימונים בעבר בלבד, ללא תאריכים מוגדרים
                    else if (filter.Past.Value && !filter.Future.Value && !filter.StratDate.HasValue && !filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date <= todayDateTime);
                    }
                    // אימונים בעבר בלבד בטווח תאריכים מוגדר
                    else if (filter.Past.Value && !filter.Future.Value && filter.StratDate.HasValue && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) >= filter.StratDate.Value && DateOnly.FromDateTime(x.Training.Date) <= filter.EndDate.Value && x.Training.Date <= todayDateTime);
                    }
                    // אימונים בעבר בלבד מהתחלה מוגדרת
                    else if (filter.Past.Value && !filter.Future.Value && filter.StratDate.HasValue)
                    {
                        query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) >= filter.StratDate.Value && x.Training.Date <= todayDateTime);
                    }
                    // אימונים בעבר בלבד עד תאריך סיום מוגדר
                    else if (filter.Past.Value && !filter.Future.Value && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) <= filter.EndDate.Value && x.Training.Date <= todayDateTime);
                    }
                    // אימונים בעתיד בלבד, ללא תאריכים מוגדרים
                    else if (filter.Future.Value && !filter.Past.Value && !filter.StratDate.HasValue && !filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date >= todayDateTime);
                    }
                    // אימונים בעתיד בלבד בטווח תאריכים מוגדר
                    else if (filter.Future.Value && !filter.Past.Value && filter.StratDate.HasValue && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) >= filter.StratDate.Value && DateOnly.FromDateTime(x.Training.Date) <= filter.EndDate.Value && x.Training.Date >= todayDateTime);
                    }
                    // אימונים בעתיד בלבד מהתחלה מוגדרת
                    else if (filter.Future.Value && !filter.Past.Value && filter.StratDate.HasValue)
                    {
                        query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) >= filter.StratDate.Value && x.Training.Date >= todayDateTime);
                    }
                    // אימונים בעתיד בלבד עד תאריך סיום מוגדר
                    else if (filter.Future.Value && !filter.Past.Value && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) <= filter.EndDate.Value && x.Training.Date >= todayDateTime);
                    }



                }

                if (filter.Past.Value && !filter.Future.Value)
                {
                    query = query.Where(x =>
                        x.Training.Date <= todayDateTime // אימונים היום ולפני היום
                    );
                }
                else if (filter.Future.Value && !filter.Past.Value)
                {
                    query = query.Where(x =>
                        x.Training.Date >= todayDateTime // אימונים היום ואחרי היום
                    );
                }

                // שלב 2: הבאה של כל הנתונים לזיכרון וסינון לפי שעה ודקה
                var results = await query.ToListAsync();

                if (filter.Past.Value && !filter.Future.Value)
                {
                    results = results.Where(x =>
                        x.Training.Date < todayDateTime || // אימונים לפני היום
                        (x.Training.Date == todayDateTime &&
                        (x.Training.Training.Hour < todayDateTime.Hour || // שעות קטנות מהשעה הנוכחית
                        (x.Training.Training.Hour == todayDateTime.Hour && x.Training.Training.Minute <= todayDateTime.Minute)) // שעות שוות ודקות קטנות או שוות
                    )).ToList();
                }
                else if (filter.Future.Value && !filter.Past.Value)
                {
                    results = results.Where(x =>
                        x.Training.Date > todayDateTime || // אימונים אחרי היום
                        (x.Training.Date == todayDateTime &&
                        (x.Training.Training.Hour > todayDateTime.Hour || // שעות גדולות מהשעה הנוכחית
                        (x.Training.Training.Hour == todayDateTime.Hour && x.Training.Training.Minute > todayDateTime.Minute)) // שעות שוות ודקות גדולות
                    )).ToList();
                }

                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(results);

            }
            catch
            {
                throw;
            }
        }


        private void ApplyDateRangeFilter(ref IQueryable<TrainingCustomer> query, DateOnly? startDate, DateOnly? endDate, bool isPast = false)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) >= startDate.Value && DateOnly.FromDateTime(x.Training.Date) <= endDate.Value);
                if (isPast) query = query.Where(x => x.Training.Date <= DateTime.Now);
            }
            else if (startDate.HasValue)
            {
                query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) >= startDate.Value);
                if (isPast) query = query.Where(x => x.Training.Date <= DateTime.Now);
            }
            else if (endDate.HasValue)
            {
                query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) <= endDate.Value);
                if (isPast) query = query.Where(x => DateOnly.FromDateTime(x.Training.Date) <= DateOnly.FromDateTime(DateTime.Now));
            }
        }
        public async Task AddTrainingForCustomer(int TrainingId, int CustomerId)
        {
            try
            {
                var training = await _context.AvailableTraining
                .Include(tc => tc.Training)
                .Include(at => at.Training.TrainingCustomerType)
                .FirstOrDefaultAsync(x => x.TrainingId == TrainingId);
                if (training == null)
                    throw new Exception($"cant find training by ID {TrainingId}");

                var customer = await _context.Customers
               .Include(c => c.SubscriptionType)
               .FirstOrDefaultAsync(x => x.Id == CustomerId);
                if (customer == null)
                    throw new Exception($"cant find customer by ID {CustomerId}");

                if (await numOfParticipants(training) && await trainingQuota(customer, training.Date)
                    && await checkType(customer, training))
                {
                    training.ParticipantsCount = training.ParticipantsCount + 1;
                    TrainingCustomerDTO trainingCustomer = new TrainingCustomerDTO();
                    trainingCustomer.TrainingID = TrainingId;
                    trainingCustomer.CustomerID = CustomerId;
                    trainingCustomer.Attended = false;
                    trainingCustomer.IsActive = true;
                    AddTrainingCustomer(trainingCustomer);
                    await _context.SaveChangesAsync();
                    throw new Exception($"You have successfully registered for this training");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> trainingQuota(Customer customer, DateTime date)
        {
            try
            {
                var maxOfTrainingPerWeek = customer.SubscriptionType.NumberOfTrainingPerWeek; //מס' ימי סוג האימון הנוכחי לשבוע
                int currentDayOfWeek = (int)date.DayOfWeek;

                // טווח שבוע האימון הנוכחי
                DateTime startDate = date.AddDays(-currentDayOfWeek);
                DateTime endDate = startDate.AddDays(7);

                // מס' פעמים ששהה באימון בשבוע של אימון זה
                var numOfTrainingPerWeek = await _context.TrainingCustomers
                .Include(tc => tc.Training)
                .Where(x => x.CustomerID == customer.Id && x.Attended &&
                x.Training.Date >= startDate && x.Training.Date < endDate)
                .CountAsync();

                if (numOfTrainingPerWeek < maxOfTrainingPerWeek)
                {
                    return true;
                }
                throw new Exception($"You have exceeded the amount of training this week");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> numOfParticipants(AvailableTraining training)
        {
            try
            {
                var currentNumOfParticipants = training.ParticipantsCount; //מס' משתתפים נוכחי
                var MaxNumOfParticipants = training.Training.ParticipantsCount; //מס' מקסימלי של משתתפים
                if (currentNumOfParticipants < MaxNumOfParticipants)
                {
                    return true;
                }
                throw new Exception($"The quota of participants for this training is full");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> checkType(Customer customer, AvailableTraining training)
        {
            try
            {
                if (training.Training.TrainingCustomerType.CustomerTypeID == customer.CustomerTypeId)
                {
                    return true;
                }
                throw new Exception($"Your client type is not compatible with this training");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
