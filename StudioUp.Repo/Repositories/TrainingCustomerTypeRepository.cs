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
    public class TrainingCustomerTypeRepository : ITrainingCustomerTypesRepository
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrainingCustomerTypeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<TrainingCustomerTypeDTO>> GetAllAsync()
        {
            try
            {
                //List<Training> lst = await _context.Trainings.Where(t => t.IsActive)
                //.Include(t => t.TrainingCustomerType.CustomerType)
                //.Include(t => t.TrainingCustomerType.TrainingType)
                //.Include(t => t.Trainer)
                //.ToListAsync();
                //return _mapper.Map<List<CalanderTrainingDTO>>(lst);
                var TrainingCustomerType = await _context.TrainingCustomersTypes.Where(t => t.IsActive)
                    .Include(t => t.TrainingType)
                    .Include(t => t.CustomerType)
                    .ToListAsync();
                return _mapper.Map<List<TrainingCustomerTypeDTO>>(TrainingCustomerType);
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Customer Type List.", ex);
            }
        }


        public async Task<TrainingCustomerTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                var c = await _context.TrainingCustomersTypes.Where(t => t.Id == id && t.IsActive)
                     .Include(t => t.TrainingType)
                    .Include(t => t.CustomerType).FirstOrDefaultAsync();
                return _mapper.Map<TrainingCustomerTypeDTO>(c);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Customer Type.", ex);
            }
        }



        //הוספת אימון כולל בדיקות האם זה אפשרי
        public async Task<TrainingCustomerTypePostComand> AddAsync(TrainingCustomerTypePostComand trainingCustomerTypedto)
        {
            try
            {
                if (trainingCustomerTypedto == null)
                {
                    throw new Exception("Training Customer Type cannot be null");
                }
                var trainingCustomerType = _mapper.Map<TrainingCustomerType>(trainingCustomerTypedto);

                TrainingType trainingType = _context.TrainingTypes.FirstOrDefault(t => t.ID == trainingCustomerType.TrainingTypeId);
                trainingCustomerType.TrainingType = trainingType;

                CustomerType customerType = _context.CustomerTypes.FirstOrDefault(t => t.ID == trainingCustomerType.CustomerTypeID);
                trainingCustomerType.CustomerType = customerType;

                //בדיקות בשביל לראות האם אפשר להוסיף כזה סוג אימון
                if (trainingCustomerType != null && trainingCustomerType.TrainingType != null && trainingCustomerType.CustomerType != null)
                {
                    // בדיקה האם סוג האימון בפעילות
                    if (trainingCustomerType.TrainingType.IsActive)
                    {
                        //האם יש אפשרות לכזה לקוח
                        if (trainingCustomerType.CustomerType.IsActive)
                        {
                            var allTrainingCustomerType = await GetAllAsync();
                            foreach (var item in allTrainingCustomerType)
                            {
                                if (item.TrainingTypeID == trainingCustomerType.TrainingTypeId && item.CustomerTypeID == trainingCustomerType.CustomerTypeID)
                                { throw new Exception("כבר קיים כזה סוג אימון!!");}
                            }
                            var TrainingCustomerType1 = await _context.TrainingCustomersTypes.AddAsync(_mapper.Map<TrainingCustomerType>(trainingCustomerType));
                            await _context.SaveChangesAsync();
                            return trainingCustomerTypedto;
                        }
                        else { throw new Exception("Condition not met: CustomerType not active"); }
                    }
                    else { throw new Exception("Condition not met: TrainingType not active"); }
                }
                else { throw new Exception("לא נמצא כזה סוג אימון או לקוח, אולי זה לא פעיל!!"); }
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add the Training Customer Type.", ex);
            }
        }


        public async Task UpdateAsync(TrainingCustomerTypePostComand trainingCustomerTypedto)
        {
            try
            {
                var trainingCustomerType = await _context.TrainingCustomersTypes.FindAsync(trainingCustomerTypedto.ID);
                if (trainingCustomerType == null)
                    throw new Exception("Training Customer Type not found.");
                _mapper.Map(trainingCustomerTypedto, trainingCustomerType);
                _context.Entry(trainingCustomerType).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to update the Training Customer Type.", ex);
            }
        }


        //הפונקציה הזו הופכת את ה isActive להיות false
        public async Task DeleteAsync(int id)
        {
            try
            {
                var thisTCT = await _context.TrainingCustomersTypes.FindAsync(id);
                if (thisTCT == null || !thisTCT.IsActive)
                    return;
                thisTCT.IsActive = false;
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to delete the Training Customer Type.", ex);
            }
        }
         
        public async Task<List<TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes()
        {
            var TrainingCustomerType = await _context.TrainingCustomersTypes.Include(a => a.TrainingType).Include(a => a.CustomerType).ToListAsync();


            return _mapper.Map<List<TrainingCustomerTypeDTO>>(TrainingCustomerType);
            }


        //public async Task<List<DTO.TrainingCustomerTypeDTO>> GetActiveTrainingCustomerTypes()
        //{
        //    var TrainingCustomerTypes = await context.TrainingCustomersTypes.Where(t => t.IsActive == true).ToListAsync();

        //    return mapper.Map<List<DTO.TrainingCustomerTypeDTO>>(TrainingCustomerTypes);
        //}




    }


}
