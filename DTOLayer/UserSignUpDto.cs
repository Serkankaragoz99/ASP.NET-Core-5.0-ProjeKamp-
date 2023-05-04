using CoreLayer.Entities;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class UserSignUpDto: AppUser, IDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsAcceptTheContract { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
