using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repository
{
    public class HMORepository : IHMORepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<HMORepository> _logger;

        public HMORepository(DataContext context, IMapper mapper, ILogger<HMORepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<HMODTO> AddAsync(HMODTO hmo)
        {
            try
            {
                var newHMO = _mapper.Map<HMO>(hmo);
                newHMO.IsActive = true; 
                await _context.HMOs.AddAsync(newHMO);
                await _context.SaveChangesAsync();
                return hmo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddAsync-Repo");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var hmo = await _context.HMOs.FirstOrDefaultAsync(h => h.ID == id && h.IsActive);
                if (hmo == null)
                {
                    throw new Exception($"HMO with ID {id} does not exist or is already inactive.");
                }

                hmo.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func DeleteAsync-Repo");
                throw;
            }
        }



        public async Task<List<HMODTO>> GetAllAsync()
        {
            try
            {
                var activeHMOs = await _context.HMOs.Where(h => h.IsActive).ToListAsync();
                return _mapper.Map<List<HMODTO>>(activeHMOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllAsync-Repo");
                throw;
            }
        }

        public async Task<HMODTO> GetByIdAsync(int id)
        {
            try
            {
                var hmo = await _context.HMOs.FirstOrDefaultAsync(h => h.ID == id && h.IsActive);
                if (hmo == null)
                {
                    throw new Exception($"HMO with ID {id} does not exist or is inactive.");
                }
                return _mapper.Map<HMODTO>(hmo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdAsync-Repo");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(HMODTO hmoDTO)
        {
            try { 
           HMO hmo = _mapper.Map<HMO>(hmoDTO); 

            _context.HMOs.Update(hmo);
            await _context.SaveChangesAsync();
                return true;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateAsync-Repo");
                throw;
            }
        }
    }
}
