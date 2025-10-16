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
    public class ContactRepository : IContactRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContactDTO> AddAsync(ContactDTO contactDTO)
        {
            try
            {
                var newContact = await _context.Contacts.AddAsync(_mapper.Map<Contact>(contactDTO));
                await _context.SaveChangesAsync();
                return contactDTO;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to add a contact");
            }
        }

        public async Task<List<ContactDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<Contact>, List<ContactDTO>>(await _context.Contacts.ToListAsync());
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to fetch any Contacts");
            }
        }

        public async Task<ContactDTO> GetByIdAsync(int id)
        {
            try
            {
                var contact = _mapper.Map<Contact, ContactDTO>(await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id));
                return contact;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to fetch this contact");
            }
        }

    }
}
