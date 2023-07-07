using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface InterfaceTokenService 
    {
        string CreateToken(AppUser user);
    }
}