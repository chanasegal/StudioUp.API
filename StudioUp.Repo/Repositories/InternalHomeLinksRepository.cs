using AutoMapper;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class InternalHomeLinksRepository:IInternalHomeLinksRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InternalHomeLinksRepository> _logger;

        public InternalHomeLinksRepository(DataContext context, IMapper mapper, ILogger<InternalHomeLinksRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<InternalHomeLinksDTO>> GetAllLinks()
        {
            try
            {
                return _mapper.Map<List<InternalHomeLinks>, List<InternalHomeLinksDTO>>(await this._context.InternalHomeLinks.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllLinks-Repo");
                throw;
            }
        }

        public async Task<InternalHomeLinksDTO> GetLinkById(int id)
        {
            try
            {
                var link = _mapper.Map<InternalHomeLinks, InternalHomeLinksDTO>(await _context.InternalHomeLinks.FirstOrDefaultAsync(x => x.ID == id));
                return link;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdAsync-Repo");
                throw;
            }
        }
        public async Task<InternalHomeLinksDTO> AddAsync(InternalHomeLinksDTO link)
        {
            try
            {
                var newLink = await this._context.InternalHomeLinks.AddAsync(_mapper.Map<InternalHomeLinks>(link));
                await _context.SaveChangesAsync();
                return link;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddAsync-Repo");
                throw;
            }
        }

        public async Task Update(InternalHomeLinksDTO link)
        {
            try
            {
                InternalHomeLinks l = _mapper.Map<InternalHomeLinks>(link);
                _context.InternalHomeLinks.Update(l);
                 _context.Entry(l).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func Update-Repo");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                InternalHomeLinks link = await _context.InternalHomeLinks.FindAsync(id);
                link.IsActive = false;
                _context.InternalHomeLinks.Update(_mapper.Map<InternalHomeLinks>(link));
                _context.Entry(link).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func Delete-Repo");
                throw;
            }

        }



    }
}
