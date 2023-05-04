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
    public class UserDto: AppUser, IDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public IFormFile ProfileImageFile { get; set; }
    }
}
