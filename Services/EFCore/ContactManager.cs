using AutoMapper;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CreateContact(Contact contact)
        {
            Contact cnt = _mapper.Map<Contact>(contact);
            _manager.Contact.Create(cnt);
            _manager.Save();
        }

        public IEnumerable<Contact> GetAllContacts(bool trackChanges)
        {
            return _manager.Contact.FindAll(trackChanges);
        }
    }
}
