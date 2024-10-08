using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.WebDTOs.ContactDTOs;

public class ContactDTO
{
    public Guid Id { get; set; }
    public string ContactLocation { get; set; }
    public string ContactNumber { get; set; }
    public string ContactMail { get; set; }
    public string ContactFooterDescription { get; set; }
}
