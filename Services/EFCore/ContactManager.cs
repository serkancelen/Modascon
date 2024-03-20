using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.EFCore
{
    public class ContactManager : IContactService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ContactManager(IRepositoryManager manager, IMapper mapper = null)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto contactDto)
        {
            Contact contact = _mapper.Map<Contact>(contactDto);
            _manager.Contact.Create(contact);
            _manager.Save();
        }

        public IEnumerable<Contact> GetAllContacts(bool trackChanges)
        {
            return _manager.Contact.FindAll(trackChanges);
        }
    }
}
