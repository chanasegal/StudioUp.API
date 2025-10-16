using AutoMapper;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.Repo.Repository;
namespace StudioUp.Repo.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public TrainerRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<TrainerDTO>> GetAllTrainersAsync()
        {
            try
            {
                var x = await context.Trainers.Where(y => y.IsActive).ToListAsync();
                return mapper.Map<List<TrainerDTO>>(x);


            }
            catch
            {
                throw;
            }
        }
        public async Task<TrainerDTO> GetTrainerByIdAsync(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                if (c.IsActive)
                    return mapper.Map<TrainerDTO>(c);
                else
                    throw new Exception($"cant find trainer  by ID {id}");
               
            }
            catch 
            {
                throw;
            }
        }
        public async Task<TrainerDTO> AddTrainerAsync(TrainerDTO t)
        {
            try
            {
                var x= await context.Trainers.FirstOrDefaultAsync(y => y.FirstName == t.FirstName&&y.LastName==t.LastName&&y.Mail==t.Mail);
                    if (x==null)
                {
                var trainer = await context.Trainers.AddAsync(mapper.Map<Trainer>(t));

                }
                else
                {
                    x.IsActive = true;
                    context.Trainers.Update(mapper.Map<Trainer>(x));
                }
                await this.context.SaveChangesAsync();
                return t;
            }
            catch 
            {
                throw;
            }
        }
        public async Task UpdateTrainerAsync(TrainerDTO t)
        {
            try
            {
                var trainer = await this.context.Trainers.FirstOrDefaultAsync(trainer => trainer.ID == t.ID);
                trainer.Address = t.Address;
                trainer.Tel = t.Tel;
                trainer.Mail = t.Mail;
                trainer.LastName = t.LastName;
                trainer.FirstName = t.FirstName;
                trainer.IsActive = t.IsActive;
                context.Trainers.Update(mapper.Map<Trainer>(trainer));
                await context.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }
        public async Task DeleteTrainerAsync(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null||c.IsActive==false)
                {
                    throw new($"Not found trainre with ID: {id}");
                }
                c.IsActive = false;
                await context.SaveChangesAsync();
            }
            catch 
            {
                throw;

            }
        }
        public async Task<List<TrainerDTO>> FilterTrainerAsync(TrainerFilterDto filter)
        {
            try
            {
                var query = context.Trainers.AsQueryable();

                if (!string.IsNullOrEmpty(filter.FirstName) || !string.IsNullOrEmpty(filter.LastName))
                {
                    var firstName = filter.FirstName?.Trim().Replace(" ", "").ToLower();
                    var lastName = filter.LastName?.Trim().Replace(" ", "").ToLower();

                    query = query.Where(t =>
                        (string.IsNullOrEmpty(firstName) || t.FirstName.ToLower().Replace(" ", "").Contains(firstName)) &&
                        (string.IsNullOrEmpty(lastName) || t.LastName.ToLower().Replace(" ", "").Contains(lastName)));
                }

                if (!string.IsNullOrEmpty(filter.mail))
                {
                    var email = filter.mail.Trim().Replace(" ", "").ToLower();
                    query = query.Where(t => t.Mail.ToLower().Replace(" ", "").Contains(email));
                }

                return await query.Select(t => new TrainerDTO
                {
                    ID = t.ID,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Address = t.Address,
                    Mail = t.Mail,
                    Tel = t.Tel,
                    IsActive = t.IsActive
                }).Where(ct => ct.IsActive).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}

