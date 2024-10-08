using Mapster;
using RestaurantApp.Application.DTOs.WebDTOs.ContactDTOs;
using RestaurantApp.Domain.Contracts.WebsitesContracts.ContactUsRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.WebServices.ContactServices;

public class ContactService : IContactService
{
    private readonly IContactUsRepository _contactRepository;

    public ContactService(IContactUsRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IDataResult<ContactDTO>> AddAsync(ContactCreateDTO contactCreateDTO)
    {
      var newContact = contactCreateDTO.Adapt<ContactUs>();
        await _contactRepository.AddAsync(newContact);
        await _contactRepository.SaveChangesAsync();
        return new SuccessDataResult<ContactDTO>(newContact.Adapt<ContactDTO>(), "Contact Add success!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact is null)
        {
            return new ErrorResult("Contact not found");
        }
        await _contactRepository.DeleteAsync(contact);
        await _contactRepository.SaveChangesAsync();
        return new SuccessResult("Contact delete success!");
    }

    public async Task<IDataResult<List<ContactListDTO>>> GetAllAsync()
    {
        var contact = await _contactRepository.GetAllAsync();
        if (contact.Count() <= 0)
        {
            return new ErrorDataResult<List<ContactListDTO>>(contact.Adapt<List<ContactListDTO>>(), "Contact Sistemde Kayıtlı");
        }
        return new SuccessDataResult<List<ContactListDTO>>(contact.Adapt<List<ContactListDTO>>(), "Contact List success!");

    }

    public async Task<IDataResult<ContactDTO>> GetByIdAsync(Guid id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact is null)
        {
            return new ErrorDataResult<ContactDTO>("Contact not found");
        }
        return new SuccessDataResult<ContactDTO>(contact.Adapt<ContactDTO>(), "Contact get success!");
          
    }

    public async Task<IDataResult<ContactDTO>> UpdateAsync(ContactUpdateDTO contactUpdateDTO)
    {
        var contact = await _contactRepository.GetByIdAsync(contactUpdateDTO.Id);
        if (contact is null)
        {
            return new ErrorDataResult<ContactDTO>("Contact not found");
        }
        contact.ContactLocation = contactUpdateDTO.ContactLocation;
        contact.ContactNumber = contactUpdateDTO.ContactNumber;
        contact.ContactMail = contactUpdateDTO.ContactMail;
        contact.ContactFooterDescription = contactUpdateDTO.ContactFooterDescription;
        await _contactRepository.UpdateAsync(contact);
        await _contactRepository.SaveChangesAsync();
        return new SuccessDataResult<ContactDTO>(contact.Adapt<ContactDTO>(), "Contact update success!");

    }
}
