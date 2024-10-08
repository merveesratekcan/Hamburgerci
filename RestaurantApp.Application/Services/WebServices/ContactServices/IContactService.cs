using RestaurantApp.Application.DTOs.WebDTOs.AboutDTOs;
using RestaurantApp.Application.DTOs.WebDTOs.ContactDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.WebServices.ContactServices;

public interface IContactService
{
    Task<IDataResult<List<ContactListDTO>>> GetAllAsync();
    Task<IDataResult<ContactDTO>> AddAsync(ContactCreateDTO contactCreateDTO);
    Task<IDataResult<ContactDTO>> UpdateAsync(ContactUpdateDTO contactUpdateDTO);
    Task<IDataResult<ContactDTO>> GetByIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);
}
