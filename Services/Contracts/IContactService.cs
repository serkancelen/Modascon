using Entities.Dtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContacts(bool trackChanges);
        Task CreateContactAsync(CreateContactDto contactDto);
    }
}
